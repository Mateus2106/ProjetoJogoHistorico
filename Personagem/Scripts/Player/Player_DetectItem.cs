using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_DetectItem : MonoBehaviour
{
    public LayerMask layerToDetect;
    public Transform rayTransformPivot;
    public string buttonPickup;

    private Transform itemAvaliableForPickup;
    private RaycastHit hit;
    private float detectRange = 3;
    private float detectRadius = 0.7f;
    private bool itemInRange;

    private float labelWidth = 200;
    private float labelHeight = 50;

    void Update()
    {
        CastRayForDetectingItens();
        CheckForItemPickupAttempt();
    }

    void CastRayForDetectingItens()
    {
        if(Physics.SphereCast(rayTransformPivot.position, detectRadius, rayTransformPivot.forward, out hit, detectRange, layerToDetect))
        {
            itemAvaliableForPickup = hit.transform;
            itemInRange = true;
        }

        else
        {
            itemInRange = false;
        }
    }

    void CheckForItemPickupAttempt()
    {
        if(Input.GetButtonDown(buttonPickup) && Time.timeScale > 0 && itemInRange)
        {
            itemAvaliableForPickup.GetComponent<Item_Master>().CallEventPickupAction(rayTransformPivot);
        }
    }

    void OnGUI()
    {
        if(itemInRange && itemAvaliableForPickup != null)
        {
            GUI.Label(new Rect(Screen.width / 2 - labelWidth / 2, Screen.height / 2, labelWidth, labelHeight), itemAvaliableForPickup.name);
        }
    }
}
