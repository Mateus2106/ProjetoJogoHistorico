using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_TogglePause : MonoBehaviour
{
    private GameManager_Master gameManagerMaster;
    private bool isPaused;

    void OnEnable()
    {
        SetInitialReferences();
        gameManagerMaster.MenuToggleEvent += TogglePause;
        gameManagerMaster.InventoryIUToggleEvent += TogglePause;
        gameManagerMaster.GameOverEvent += TogglePause;
    }

    void Update()
    {
        if(gameManagerMaster.isRestart)
        {
            TogglePause();
        }
        if(gameManagerMaster.isGoToMenuScene)
        {
            TogglePause();
        }
    }

    void OnDisable()
    {
        gameManagerMaster.MenuToggleEvent -= TogglePause;
        gameManagerMaster.InventoryIUToggleEvent -= TogglePause;
        gameManagerMaster.GameOverEvent -= TogglePause;
    }

    void SetInitialReferences()
    {
        gameManagerMaster = GetComponent<GameManager_Master>();
    }

    void TogglePause()
    {
        if(isPaused)
        {
            Time.timeScale = 1;
            isPaused = false;
        }
        else
        {
            Time.timeScale = 0;
            isPaused = true;
        }
    }
}
