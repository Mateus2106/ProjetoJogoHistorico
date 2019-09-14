using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Master : MonoBehaviour
{
    private Player_Master playerMaster;

    public delegate void GeneralEventHandler();
    public event GeneralEventHandler EventObjectThrow;
    public event GeneralEventHandler EventObjectPickup;

    public delegate void PickupActionEventHandler(Transform item);
    public event PickupActionEventHandler EventPickupAction;

    private bool isOnPlayer;
    public bool canBeThrow;

    void Start()
    {
        SetInitialReferences();
        CheckIfOnPlayer();
    }

    public void CallEventObjectThrow()
    {
        if(EventObjectThrow != null)
        {
            EventObjectThrow();
        }
        
    }

    public void CallEventObjectPickup()
    {
        if(EventObjectPickup != null)
        {
            EventObjectPickup();
            playerMaster.CallEventInventoryChanged();
        }
    }

    public void CallEventPickupAction(Transform item)
    {
        if(EventPickupAction != null)
        {
            EventPickupAction(item);
        }
    }

    void SetInitialReferences()
    {
        playerMaster = GameManager_References._assetPlayer.GetComponent<Player_Master>();
    }

    void CheckIfOnPlayer()
    {
        if(transform.root == GameManager_References._player.transform)
        {
            isOnPlayer = true;
        }
        else
        {
            isOnPlayer = false;
        }
    }
}
