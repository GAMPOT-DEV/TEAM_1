using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlaceManager : MonoBehaviour
{
    [SerializeField] public List<PlaceObject> m_listAllPlace;

    private void Start()
    {

        
    }

   


    public PlaceObject GivePlaceValue(int idx)
    {
        return m_listAllPlace[idx];
    }



}
