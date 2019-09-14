using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetectSpawn : MonoBehaviour
{
    public GameObject boss;
    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject enemy3;
    public GameObject enemy4;
    public float proximity;
    private float checkRate;
    private float nextCheck;
    private Transform myTransform;
    public Transform playerTransform;
    private Enemy_Master enemyMaster;

    void Start()
    {
        SetInitialReferences();
    }

    void Update()
    {
        CheckDistance();
    }

    void SetInitialReferences()
    {
        myTransform = transform;
        checkRate = Random.Range(0.8f, 1.2f);
        enemyMaster = GetComponent<Enemy_Master>();
    }

    void CheckDistance()
    {
        if (Time.time > nextCheck)
        {
            nextCheck = Time.time + checkRate;
            if (Vector3.Distance(myTransform.position, playerTransform.position) < proximity)
            {
                boss.GetComponent<Enemy_Master>().isNavPaused = false;
                enemy1.GetComponent<Enemy_Master>().isNavPaused = false;
                enemy2.GetComponent<Enemy_Master>().isNavPaused = false;
                enemy3.GetComponent<Enemy_Master>().isNavPaused = false;
                enemy4.GetComponent<Enemy_Master>().isNavPaused = false;
            }
        }
    }
}
