using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible_Degenerate : MonoBehaviour
{
    private Destructible_Master destructiveMaster;
    private bool isHealthLow = false;
    public float degenRate = 1;
    private float nextDegenTime;
    public int healthLoss = 5;

    void OnEnable()
    {
        SetInitialReferences();
        destructiveMaster.EventHealthLow += HealthLow;
    }

    void OnDisable()
    {
        destructiveMaster.EventHealthLow -= HealthLow;
    }

    void Update()
    {
        CheckIfHealthShouldDegenerate();
    }

    void SetInitialReferences()
    {
        destructiveMaster = GetComponent<Destructible_Master>();
    }

    void HealthLow()
    {
        isHealthLow = true;
    }

    void CheckIfHealthShouldDegenerate()
    {
        if(isHealthLow)
        {
            if(Time.time > nextDegenTime)
            {
                nextDegenTime = Time.time + degenRate;
                destructiveMaster.CallEventDeductHealth(healthLoss);
            }
        }
    }
}
