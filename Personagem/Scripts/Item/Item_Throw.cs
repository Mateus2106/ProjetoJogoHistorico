using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Throw : MonoBehaviour
{
    private Item_Master itemMaster;
    private Transform myTransform;
    private Rigidbody myRigidBody;
    private Vector3 throwDirection;
    private Gun_Active gunActive;

    public string throwButtonName;
    public float throwForce;

    void Start()
    {
        SetInitialReferences();
    }

    void Update()
    {
        CheckForThrowInput();
    }

    void SetInitialReferences()
    {
        itemMaster = GetComponent<Item_Master>();
        myTransform = transform;
        myRigidBody = GetComponent<Rigidbody>();
        gunActive = GetComponent<Gun_Active>();
    }

    void CheckForThrowInput()
    {
        
        if (throwButtonName != null)
        {
            if(Input.GetButtonDown(throwButtonName) && Time.timeScale > 0 && itemMaster.canBeThrow)
            {
                CarryOutThrowActions();
            }
        }
    }

    void CarryOutThrowActions()
    {
        throwDirection = myTransform.up;
        myTransform.parent = null;
        itemMaster.CallEventObjectThrow();
        itemMaster.canBeThrow = false;
        HurlItem();
    }

    void HurlItem()
    {
        myRigidBody.AddForce(throwDirection * throwForce, ForceMode.Impulse);
    }
}
