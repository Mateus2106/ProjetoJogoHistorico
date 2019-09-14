using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee_Swing : MonoBehaviour
{
	private Melee_Master meleeMaster;
	public Collider myCollider;
	public Rigidbody myRigidBody;
	public Animator myAnimator;

    void OnEnable(){
		SetInitialReferences();
		meleeMaster.EventPlayerInput += MeleeAttackActions;
	}

	void OnDisable(){
		meleeMaster.EventPlayerInput -= MeleeAttackActions;
	}
	
	void SetInitialReferences(){
		meleeMaster = GetComponent<Melee_Master>();
	}

	void MeleeAttackActions(){
		myCollider.enabled = true;
		myRigidBody.isKinematic = false;
		myAnimator.SetTrigger("Attack");
	}

	void MeleeAttackComplete(){
		myCollider.enabled = false;
		myRigidBody.isKinematic = true;
		meleeMaster.isInUse = false;
	}
}
