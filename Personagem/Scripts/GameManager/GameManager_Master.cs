using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_Master : MonoBehaviour
{
    public delegate void GameManagerEventHandler();
	public event GameManagerEventHandler MenuToggleEvent;
	public event GameManagerEventHandler InventoryIUToggleEvent;
	public event GameManagerEventHandler RestartLevelEvent;
	public event GameManagerEventHandler GoToMenuSceneEvent;
	public event GameManagerEventHandler GameOverEvent;
	
	public bool isGameOver;
	public bool isInventoryUIOn;
	public bool isMenuOn;
    public bool isRestart;
    public bool isGoToMenuScene;

    public Action IsGameOverEvent { get; internal set; }

    public void CallEventMenuToggle(){
		if(MenuToggleEvent != null){
			MenuToggleEvent();
		}	
	}
	
	public void CallEventInventoryIUToggle(){
		if(InventoryIUToggleEvent != null){
			InventoryIUToggleEvent();
		}	
	}
	
	public void CallEventRestartLevel(){
		if(RestartLevelEvent != null){
            isRestart = true;
			RestartLevelEvent();         
		}	
	}
	
	public void CallEventGoToMenuScene(){
		if(GoToMenuSceneEvent != null){
            isGoToMenuScene = true;
			GoToMenuSceneEvent();
		}	
	}
	
	public void CallEventGameOver(){
		if(GameOverEvent != null){
			isGameOver = true;
            isRestart = false;
			GameOverEvent();
		}	
	}
}
