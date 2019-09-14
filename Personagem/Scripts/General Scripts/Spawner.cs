using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject objectToSpawn;
    public int numberOfEnemies;
    private float spawnRadius = 5;
    private Vector3 spawnPosition;
    private GameManager_EventMaster eventMasterScript;

    /*void OnEnable()
    {
        setInitialReferences();
        eventMasterScript.myGeneralEvent += spawnObject;
    }

    void OnDisable()
    {
        eventMasterScript.myGeneralEvent -= spawnObject;
    }
    void Start()
    {
        spawnObject();
    }*/

    void Update()
    {
        
    }

    void spawnObject()
    {
        for(int i = 0; i <= numberOfEnemies; i++)
        {
            spawnPosition = transform.position + Random.insideUnitSphere * spawnRadius;
            Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);
        }
    }

    void setInitialReferences()
    {
        eventMasterScript = GameObject.Find("GameManager").GetComponent<GameManager_EventMaster>();
    }
}
