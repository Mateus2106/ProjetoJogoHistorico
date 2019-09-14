using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour
{
    public GameObject creditos;
    public GameObject boss;
    public Animator myAnimator;

    void CheckIfBossDead()
    {
        if(boss == null)
        {
            creditos.SetActive(true);
            ActiveCredits();
        }
    }

    void ActiveCredits()
    {
        if (myAnimator != null)
        {
            myAnimator.SetTrigger("Creditos");
        }
    }

    void Start()
    {
        SetInitialReferences();
    }

    void Update()
    {
        CheckIfBossDead();
    }

    void SetInitialReferences()
    {
        if (GetComponent<Animator>() != null)
        {
            myAnimator = GetComponent<Animator>();
        }
    }
}
