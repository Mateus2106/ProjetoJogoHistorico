using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_CursorToggle : MonoBehaviour
{
    private GameManager_Master gameManagerMaster;
    private bool isCursorLocked = true;
    private int flag = 0;

    void OnEnable()
    {
        SetInitialReferences();
        gameManagerMaster.MenuToggleEvent += ToggleCursorState;
        gameManagerMaster.InventoryIUToggleEvent += ToggleCursorState;
        gameManagerMaster.GoToMenuSceneEvent -= ToggleCursorState;
    }

    void OnDisable()
    {
        gameManagerMaster.MenuToggleEvent -= ToggleCursorState;
        gameManagerMaster.InventoryIUToggleEvent -= ToggleCursorState;
        gameManagerMaster.GoToMenuSceneEvent += ToggleCursorState;
    }

    void Update()
    {
        CheckCursorShouldBeLocked();
        if (gameManagerMaster.isGameOver && flag == 0)
        {
            ToggleCursorState();
            flag = 1;
        }
        if(!gameManagerMaster.isGameOver)
        {
            flag = 0;
        }      
    }

    void SetInitialReferences()
    {
        gameManagerMaster = GetComponent<GameManager_Master>();
    }

    void ToggleCursorState()
    {
        isCursorLocked = !isCursorLocked;
    }

    void CheckCursorShouldBeLocked()
    {
        if(isCursorLocked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }


}
