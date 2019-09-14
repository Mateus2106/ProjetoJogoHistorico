using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_GameOver : MonoBehaviour
{
    private GameManager_Master gameManagerMaster;
    public GameObject panelGameOver;
    private int flag = 0;

    void Update()
    {
        if (gameManagerMaster.isRestart)
        {
            flag = 0;
        }
        CheckForGameOverEvent();  
    }

    void OnEnable()
    {
        SetInitialReferences();
        gameManagerMaster.GameOverEvent += TurnOnGameOverPainel;
    }

    void OnDisable()
    {
        gameManagerMaster.GameOverEvent -= TurnOnGameOverPainel;
    }

    void SetInitialReferences()
    {
        gameManagerMaster = GetComponent<GameManager_Master>();
    }

    void CheckForGameOverEvent()
    {
        if(gameManagerMaster.isGameOver && flag == 0)
        {
            TurnOnGameOverPainel();
        }
    }

    void TurnOnGameOverPainel()
    {
        if(panelGameOver != null)
        {
            panelGameOver.SetActive(!panelGameOver.activeSelf);
            flag = 1;
        }
    }
}
