using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public GameObject LoadUI(string uiName)
    {
        GameObject go = Resources.Load<GameObject>($"Prefabs/UI/{uiName}");
        if (go == null)
        {
            Debug.Log($"Failed to Load : {uiName}");
            return null;
        }
        return go;
    }

    public GameObject LoadUnit(string unitName)
    {
        GameObject go = Resources.Load<GameObject>($"Prefabs/Units/{unitName}");
        if (go == null)
        {
            Debug.Log($"Failed to Load : {unitName}");
            return null;
        }
        return go;
    }
}
