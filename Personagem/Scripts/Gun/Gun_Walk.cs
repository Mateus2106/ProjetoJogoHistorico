using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun_Walk : MonoBehaviour
{
    private Player_Master playerMaster;
    private Gun_Master gunMaster;

    void Start()
    {
        SetInitialReferences();
    }
    
    void SetInitialReferences()
    {
        playerMaster = GetComponent<Player_Master>();
        gunMaster = GetComponent<Gun_Master>();
    }

    void Update()
    {
        playerMaster.playerAmmo = true;
        playerMaster.playerPistol = true;
        gunMaster.isGunLoaded = true;
    }
}
