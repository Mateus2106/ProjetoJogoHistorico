using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee_StandardInput : MonoBehaviour
{
    private Melee_Master meleeMaster;
	private Transform myTransform;
	private float nextSwing;
	public string attackButtonName;

    void Start()
    {
        SetInicialReferences();
    }

	void SetInicialReferences(){
		myTransform = transform;
		meleeMaster = GetComponent<Melee_Master>();
	}

	void CheckWeaponShouldAttack(){
		if(Time.timeScale > 0 && myTransform.root.CompareTag(GameManager_References._playerTag) && !meleeMaster.isInUse){
			if(Input.GetButton(attackButtonName) && Time.time > nextSwing){
				nextSwing = Time.time + meleeMaster.swingRate;
				meleeMaster.isInUse = true;
			}
		}
	}
}
