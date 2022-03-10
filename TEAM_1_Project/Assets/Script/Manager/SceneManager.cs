using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public int currTurn;

    public GameObject UI_Parent;
    public GameObject Unit_Parent;
    void Start()
    {
        UI_Parent = new GameObject(name = "UI_Parent"); // UI들 묶어서 하이라키창에 저장
        Unit_Parent = new GameObject(name = "Unit_Parent"); // Unit들 묶어서 하이라키창에 저장

        Instantiate(GameManager.GetInstance().resourceManager.LoadUI("UI_Turn_End_Button")).transform.SetParent(UI_Parent.transform); // 턴 종료 UI 버튼 생성
        GameManager.GetInstance().unitManager.CreateUnit(0,0);
    }
    
    //UI_Turn_End_Button
    void Update()
    {
        
    }
}
