using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stealer : Unit
{
    [SerializeField] public int attackpower;
    [SerializeField] int stealCoast;

    [SerializeField] public bool isSteal;
    public bool isPlayer;
    GameObject target_place;

    float _attackTime = 1f;
    float _stealTime = 1.5f;
    public Define.StealerState currState = Define.StealerState.nothing;

    public Unit target;
    public Vector3 targetPos;
    GameObject target_unit = null;
    GameObject target_unit2 = null;
    private void Start()
    {
        base.Init();
        //character.sprite = GameManager.resourceManager.LoadSprite("squirrel");
        level = 1;
        skill.unit = this;
        Level();
    }
    private void Update()
    {
        UpdateState();
    }
    void UpdateState()
    {
        switch (currState)
        {
            case Define.StealerState.nothing:
                UpdateNothing();
                break;
            case Define.StealerState.attack:
                UpdateAttack();
                break;
            case Define.StealerState.stealSeed:
                UpdateStealSead();
                break;
            case Define.StealerState.stealBoom:
                UpdateStealBoom();
                break;
        }
    }
    void BackCheck()
    {
        target_place = GameManager.placeManager.getPlaceObject(!_currPlace.isPlayerPlace, this._currPlace.x, 1);
        target_unit = GameManager.unitManager.GetUnit(target_place.GetComponent<PlaceObject>());
        if (target_unit != null)
        {
            if (target_unit.GetComponent<Stealer>() == null)
            {
                if (target_unit.GetComponent<Unit>().valid)
                {
                    target_unit2 = target_unit;
                }
            }
        }
        else if (target_unit == null)
        {
            target_place = GameManager.placeManager.getPlaceObject(!_currPlace.isPlayerPlace, this._currPlace.x, 0);
            target_unit = GameManager.unitManager.GetUnit(target_place.GetComponent<PlaceObject>());
            if (target_unit == null)
                target_unit2 = target_unit;
            else
            {
                if (target_unit.GetComponent<Stealer>() == null)
                {
                    if (target_unit.GetComponent<   Unit>().valid)
                    {
                        target_unit2 = target_unit;
                    }
                }
            }
        }
    }

    void isCheck()
    {
        for (int i = 0; i < 2; i++)
        {
            target_place = GameManager.placeManager.getPlaceObject(!_currPlace.isPlayerPlace, this._currPlace.x, i);
            target_unit = GameManager.unitManager.GetUnit(target_place.GetComponent<PlaceObject>());
            if (target_unit != null)
            {
                if (target_unit.GetComponent<Stealer>() == null)
                {
                    if (target_unit.GetComponent<Unit>().valid)
                    {
                        target_unit2 = target_unit;
                        break;
                    }
                }
            }
        }
    }
    public override float Ability()
    {
        Debug.Log("Dd");
        float ret = 0;
        if (skill != null && _name != "Stealer1")
        {
            skill.Skiil();
        }
            if (isBackCheck)
            {
                BackCheck();
            }
            else
            {
                //Debug.Log("플레이어");
                isCheck();
            }
            if (stealCount > 0)
            {
                Debug.Log("Ddd");
                if (target_unit2 == null || target_unit2.GetComponent<Stealer>() != null)
                {
                    ret = _attackTime;
                    GameManager.effectManager.UseSkill(Define.Effect.stealer, this);

                    GameManager.sceneManager.getEnemy(_currPlace)._currHP -= attackpower;
                }
                else if (target_unit2.GetComponent<Seed>() != null)
                {
                    ret = _stealTime;
                    Seed seed = target_unit2.GetComponent<Seed>();

                    GameManager.effectManager.UseSkill(Define.Effect.stealerToSeed, this, seed);

                    StartCoroutine(CoAttackedOrUsed(seed, ret));
                    GameManager.sceneManager.getPlayer(_currPlace)._currResource += Rip_Seed(seed);
                    if (isSeedStealDamage)
                        GameManager.sceneManager.getEnemy(_currPlace)._currHP -= 2;
                    stealCount++;
                    GameManager.unitManager.isSteal = true;
                }
                else if (target_unit2.GetComponent<Boom>() != null)
                {
                    ret = _stealTime;
                    Boom boom = target_unit2.GetComponent<Boom>();

                    GameManager.effectManager.UseSkill(Define.Effect.stealerToBoom, this, boom);

                    StartCoroutine(CoAttackedOrUsed(boom, ret));
                    if (isBoomDamageMiss)
                    {
                        StartCoroutine(CoAttackedOrUsed(this, ret));
                        GameManager.sceneManager.getPlayer(_currPlace)._currHP -= Rip_Boom(boom);
                    }
                    GameManager.unitManager.isSteal = true;
                }
                stealCount--;
            }
        
        return ret;
    }

    //if (target_unit.layer == LayerMask.NameToLayer("Seed"))
    public void Level()
    {
        switch (level)
        {
            case 1:
                attackpower = 30;
                stealCoast = 1;
                break;
            case 2:
                attackpower = 3;
                stealCoast = 3;
                break;
            case 3:
                attackpower = 5;
                stealCoast = 5;
                break;
        }
    }

    public int Rip_Seed(Seed seed)
    {
        int result;
        result = seed.myresource;
        return result;
    }
    public int Rip_Boom(Boom boom)
    {
        int result;
        result = boom.damage;
        return result;
    }
    void UpdateNothing()
    {

    }
    void UpdateAttack()
    {
        if (_currTime > _attackTime)
        {
            currState = Define.StealerState.nothing;
            _currTime = 0;
            return;
        }
        _currTime += Time.deltaTime;
        if (_currTime <= _attackTime / 2)
        {
            gameObject.transform.localScale += Vector3.one * _effectSize * Time.deltaTime;
        }
        else
        {
            gameObject.transform.localScale -= Vector3.one * _effectSize * Time.deltaTime;
        }
    }
    void UpdateStealSead()
    {
        if (_currTime > _stealTime)
        {
            currState = Define.StealerState.nothing;
            _currTime = 0;
            return;
        }
        _currTime += Time.deltaTime;
        Vector3 dirVec = (targetPos - transform.position).normalized;
        float dist = (targetPos - transform.position).magnitude;
        if (_currTime <= _stealTime / 2)
        {
            transform.position += dirVec * dist * Time.deltaTime * 1.5f;
        }
        else
        {
            if (_unitCamp == Define.UnitCamp.playerUnit)
            {
                target.transform.position = transform.position + Vector3.right;
            }
            else
            {
                target.transform.position = transform.position + Vector3.left;
            }
            transform.position -= dirVec * dist * Time.deltaTime * 1.5f;
        }
    }
    void UpdateStealBoom()
    {
        if (_currTime > _stealTime)
        {
            currState = Define.StealerState.nothing;
            _currTime = 0;
            return;
        }
        _currTime += Time.deltaTime;
        Vector3 dirVec = (targetPos - transform.position).normalized;
        float dist = (targetPos - transform.position).magnitude;
        if (_currTime <= _stealTime / 2)
        {
            transform.position += dirVec * dist * Time.deltaTime * 1.5f;
        }
        else
        {
            if (_unitCamp == Define.UnitCamp.playerUnit)
            {
                target.transform.position = transform.position + Vector3.right;
            }
            else
            {
                target.transform.position = transform.position + Vector3.left;
            }
            transform.position -= dirVec * dist * Time.deltaTime * 1.5f;
        }
    }
}
