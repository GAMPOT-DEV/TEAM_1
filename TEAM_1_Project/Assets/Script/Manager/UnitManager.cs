using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UnitManager : MonoBehaviour
{
    [SerializeField] GameObject UnitPrefab;
    private List<GameObject> UnitList = new List<GameObject>();
    public void UnitMoveFunc(int x1, int y1, int x2, int y2)
    {
        var unit = UnitList.Find(a => a.GetComponent<UnitInterface>().checkPos(x1,x2) == true);
        unit.GetComponent<UnitInterface>().Movefuc(x2,y2);
    }//유닛 이동 명령

    public void CreateUnit(int x,int y) {
        var unit = UnitFactory.getUnit("Unit",x,y,UnitPrefab);
        UnitList.Add(unit);
    }
    public void CreateMoveFunc()
    {
    }

}
