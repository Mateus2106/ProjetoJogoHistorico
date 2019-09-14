using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class GameManager_TogglePlayer : MonoBehaviour
{
    public RigidbodyFirstPersonController playerController;
    private GameManager_Master gameManagerMaster;

    void OnEnable()
    {
        SetInitialReferences();
        gameManagerMaster.MenuToggleEvent += TogglePlayerController;
        gameManagerMaster.InventoryIUToggleEvent += TogglePlayerController;
    }

    void OnDisable()
    {
        gameManagerMaster.MenuToggleEvent -= TogglePlayerController;
        gameManagerMaster.InventoryIUToggleEvent -= TogglePlayerController;
    }

    void SetInitialReferences()
    {
        gameManagerMaster = GetComponent<GameManager_Master>();
    }

    void TogglePlayerController()
    {   
        if(playerController != null)
        {
            playerController.enabled = !playerController.enabled;
        }     
    }
}
