using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_ToggleInventoryUI : MonoBehaviour
{
    public bool hasInventory;
    public GameObject inventoryUI;
    public string toggleInventoryButton;
    private GameManager_Master gameManagerMaster;

    void Start()
    {
        SetInitialReferences();
    }

    void Update()
    {
        CheckForInventoryUIRequest();
    }

    void SetInitialReferences()
    {
        gameManagerMaster = GetComponent<GameManager_Master>();
    }

    void CheckForInventoryUIRequest()
    {
        if(Input.GetButtonUp(toggleInventoryButton) && !gameManagerMaster.isMenuOn && !gameManagerMaster.isGameOver && hasInventory)
        {
            ToggleInventoryUI();
        }
    }

    public void ToggleInventoryUI()
    {
        if(inventoryUI != null)
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
            gameManagerMaster.isInventoryUIOn = !gameManagerMaster.isInventoryUIOn;
            gameManagerMaster.CallEventInventoryIUToggle();
        }
    }
}
