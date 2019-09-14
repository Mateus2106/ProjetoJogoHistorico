using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun_HitEffects : MonoBehaviour
{
    private Gun_Master gunMaster;
    public GameObject defaultHitEffect;
    public GameObject enemyHitEffect;

    void OnEnable()
    {
        SetInitialReferences();
        gunMaster.EventShotDefault += SpawnDefaultHitEffects;
        gunMaster.EventShotEnemy += SpawnEnemyHitEffect;
    }

    void OnDisable()
    {
        gunMaster.EventShotDefault -= SpawnDefaultHitEffects;
        gunMaster.EventShotEnemy -= SpawnEnemyHitEffect;
    }

    void SetInitialReferences()
    {
        gunMaster = GetComponent<Gun_Master>();
    }

    void SpawnDefaultHitEffects(Vector3 hitPosition, Transform hitTransform)
    {
        if(defaultHitEffect != null)
        {
            Instantiate(defaultHitEffect, hitPosition, Quaternion.identity);
        }
    }

    void SpawnEnemyHitEffect(Vector3 hitPosition, Transform hitTransform)
    {
        if(enemyHitEffect != null)
        {
            Instantiate(enemyHitEffect, hitPosition, Quaternion.identity);
        }
    }
}
