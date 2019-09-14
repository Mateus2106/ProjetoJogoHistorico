using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_EventMaster : MonoBehaviour
{
    public delegate void GeneralEvent();
    public event GeneralEvent myGeneralEvent;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void CallMyGeneralEvent()
    {
        if(myGeneralEvent != null)
        {
            myGeneralEvent();
        }
    }
}
