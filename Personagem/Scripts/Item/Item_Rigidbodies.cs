using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Rigidbodies : MonoBehaviour
{
    private Item_Master itemMaster;
    public Rigidbody[] rigidBodies;

    void OnEnable()
    {
        SetInitialReferences();
        itemMaster.EventObjectThrow += SetIsKimematicToFalse;
        itemMaster.EventObjectPickup += SetIsKimematicToTrue;
    }

    void OnDisable()
    {
        itemMaster.EventObjectThrow -= SetIsKimematicToFalse;
        itemMaster.EventObjectPickup -= SetIsKimematicToTrue;
    }

    void Start()
    {
        CheckIfStartsInInventory();
    }

    void SetInitialReferences()
    {
        itemMaster = GetComponent<Item_Master>();
    }

    void CheckIfStartsInInventory()
    {
        if(transform.root.CompareTag(GameManager_References._playerTag))
        {
            SetIsKimematicToTrue();
        }
    }

    void SetIsKimematicToTrue()
    {
        if(rigidBodies.Length > 0)
        {
            foreach(Rigidbody rBody in rigidBodies)
            {
                rBody.isKinematic = true;
            }
        }
    }

    void SetIsKimematicToFalse()
    {
        if (rigidBodies.Length > 0)
        {
            foreach (Rigidbody rBody in rigidBodies)
            {
                rBody.isKinematic = false;
            }
        }
    }
}
