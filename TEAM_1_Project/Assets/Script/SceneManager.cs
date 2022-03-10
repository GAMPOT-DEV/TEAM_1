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
        UI_Parent = new GameObject(name = "UI_Parent"); // UI�� ��� ���̶�Űâ�� ����
        Unit_Parent = new GameObject(name = "Unit_Parent"); // Unit�� ��� ���̶�Űâ�� ����

        Instantiate(GameManager.GetInstance().resourceManager.LoadUI("UI_Turn_End_Button")).transform.SetParent(UI_Parent.transform); // �� ���� UI ��ư ����
        GameManager.GetInstance().unitManager.CreateUnit(0,0);
        //Instantiate(GameManager.GetInstance().resourceManager.LoadUnit("Unit_Tmp")).transform.parent = Unit_Parent.transform; // �ӽ� ���� ���� ( ����â Ȯ�ο�) 
    }
    
    //UI_Turn_End_Button
    void Update()
    {
        
    }
}
