using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public int _uiInfoOffset = 50;
    public Stack<GameObject> _popupUIs = new Stack<GameObject>();
}
