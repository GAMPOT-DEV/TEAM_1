using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        LeftClick();
        RightClick();
    }

    public void LeftClick()
    {
        if (!Input.GetMouseButtonDown(0)) return;
        if (EventSystem.current.IsPointerOverGameObject()) return; // UI 클릭하면 종료
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero); // 카메라에서 화면으로 광선을 쏴서 맞는 물체 판별
        if (GameManager.GetInstance().uiManager._popupUIs.Count > 0) Destroy(GameManager.GetInstance().uiManager._popupUIs.Pop()); // 팝업 UI (현재는 유닛 정보) 가 떠있으면 삭제
        if (hit.collider == null)
        {
            return;
        }
        Debug.Log($"{hit.collider.name} 클릭");

    }
    public void RightClick()
    {
        if (!Input.GetMouseButtonDown(1)) return;
        if (EventSystem.current.IsPointerOverGameObject()) return;
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (GameManager.GetInstance().uiManager._popupUIs.Count > 0) Destroy(GameManager.GetInstance().uiManager._popupUIs.Pop());
        if (hit.collider == null)
        {
            return;
        }
        if(hit.collider.gameObject.GetComponent<Unit>() != null) // 광선을 맞은 물체가 Unit 컴포넌트를 가지고 있으면
        {
            GameObject _uiInfo = Instantiate(GameManager.GetInstance().resourceManager.LoadUI("UI_Unit_Info"));
            _uiInfo.transform.SetParent(GameManager.GetInstance().sceneManager.UI_Parent.transform);
            GameManager.GetInstance().uiManager._popupUIs.Push(_uiInfo);
            _uiInfo.GetComponentInChildren<Image>().gameObject.transform.position = Input.mousePosition + Vector3.right * GameManager.GetInstance().uiManager._uiInfoOffset;
        }
    }
}
