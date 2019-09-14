using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_SetRotation : MonoBehaviour
{
    private Item_Master itemMaster;
    public GameObject lamp;
    public float x;
    public float z;
    public float y;

    void OnEnable()
    {
        SetInitialReferences();
        itemMaster.EventObjectPickup += SetRotationOnPlayer;
    }

    void OnDisable()
    {
        itemMaster.EventObjectPickup -= SetRotationOnPlayer;
    }


    void SetInitialReferences()
    {
        itemMaster = GetComponent<Item_Master>();
    }

    void SetRotationOnPlayer()
    {
        lamp.transform.localRotation = Quaternion.Euler(x, y, z);
    }
}
