using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Health : MonoBehaviour
{
    private Enemy_Master enemyMaster;
    public int enemyHealth = 100;
    public float healthLow = 25;

    void OnEnable()
    {
        SetInitialReferences();
        enemyMaster.EventEnemyDeductionHealth += DeductionHealth;
        enemyMaster.EventEnemyIncreaseHealth += IncreaseHealth;
    }

    void OnDisable()
    {
        enemyMaster.EventEnemyDeductionHealth -= DeductionHealth;
        enemyMaster.EventEnemyIncreaseHealth -= IncreaseHealth;
    }

    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Period))
        {
            enemyMaster.CallEventEnemyIncreaseHealth(70);
        }
    }

    void SetInitialReferences()
    {
        enemyMaster = GetComponent<Enemy_Master>();
    }

    void DeductionHealth(int healthChange)
    {
        enemyHealth -= healthChange;
        if(enemyHealth <= 0)
        {
            enemyHealth = 0;
            enemyMaster.CallEventEnemyDie();
            Destroy(gameObject, Random.Range(10, 20));
        }

        CheckHealthFraction();
    }

    void CheckHealthFraction()
    {
        if(enemyHealth <= healthLow && enemyHealth > 0)
        {
            enemyMaster.CallEventEnemyHealthLow();
        }

        else if(enemyHealth > healthLow)
        {
            enemyMaster.CallEventEnemyHealthRecovered();
        }
    }

    void IncreaseHealth(int healthChange)
    {
        enemyHealth += healthChange;
        if(enemyHealth > 100)
        {
            enemyHealth = 100;
        }

        CheckHealthFraction();
    }
}
