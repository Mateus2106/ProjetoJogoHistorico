using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun_Active : MonoBehaviour
{
    public GameObject pistol;
    public GameObject characterPistol;
    public GameObject characterIdle;
    public GameObject ammo;

    void CheckIfPistolActive()
    {
        if(pistol.activeInHierarchy)
        {
            characterPistol.SetActive(true);
            characterIdle.SetActive(false);
            ammo.SetActive(true);
        }
        else
        {
            characterPistol.SetActive(false);
            characterIdle.SetActive(true);
            ammo.SetActive(false);
        }
    }

    void Update()
    {
        CheckIfPistolActive();
    }
}
