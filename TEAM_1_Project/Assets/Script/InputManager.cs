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
        if (EventSystem.current.IsPointerOverGameObject()) return; // UI Ŭ���ϸ� ����
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero); // ī�޶󿡼� ȭ������ ������ ���� �´� ��ü �Ǻ�
        if (GameManager.GetInstance().uiManager._popupUIs.Count > 0) Destroy(GameManager.GetInstance().uiManager._popupUIs.Pop()); // �˾� UI (����� ���� ����) �� �������� ����
        if (hit.collider == null)
        {
            return;
        }
        Debug.Log($"{hit.collider.name} Ŭ��");

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
        if(hit.collider.gameObject.GetComponent<Unit>() != null) // ������ ���� ��ü�� Unit ������Ʈ�� ������ ������
        {
            GameObject _uiInfo = Instantiate(GameManager.GetInstance().resourceManager.LoadUI("UI_Unit_Info"));
            _uiInfo.transform.SetParent(GameManager.GetInstance().sceneManager.UI_Parent.transform);
            GameManager.GetInstance().uiManager._popupUIs.Push(_uiInfo);
            _uiInfo.GetComponentInChildren<Image>().gameObject.transform.position = Input.mousePosition + Vector3.right * GameManager.GetInstance().uiManager._uiInfoOffset;
        }
    }
}
