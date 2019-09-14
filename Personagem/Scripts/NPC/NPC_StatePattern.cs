﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC_StatePattern : MonoBehaviour
{
    private float checkRate = 0.1f;
    private float nextCheck;
    public float sightRange = 40;
    public float detectBehindRange = 5;
    public float meleeAttackRange = 4;
    public float meleeAttackDamage = 20;
    public float rangeAttackRange = 35;
    public float rangeAttackDamage = 10;
    public float rangeAttackSpread = 0.5f;
    public float attackRate = 0.4f;
    public float nextAttack;
    public float fleeRange = 25;
    public float offset = 0.4f;
    public int requiredDetectionCount = 15;

    public Transform myFollowTarget;
    [HideInInspector]
    public Transform pursueTarget;
    [HideInInspector]
    public Vector3 locationOfInterest;
    [HideInInspector]
    public Vector3 wanderTarget;
    [HideInInspector]
    public Transform myAttacker;

    public bool hasRangeAttack;
    public bool hasMeleeAttack;
    public bool isMeleeAttacking;

    public LayerMask sightLayers;
    public LayerMask myEnemyLayers;
    public LayerMask myFriendlyLayers;
    public string[] myEnemyTags;
    public string[] myFriendlyTags;

    public Transform[] waypoints;
    public Transform head;
    public MeshRenderer meshRendererFlag;
    public GameObject rangeWeapon;
    public NPC_Master npcMaster;
    [HideInInspector]
    public NavMeshAgent myNavMeshAgent;

    public NPCState_Interface currentState;
    public NPCState_Interface capturedState;
    public NPCState_Patrol patrolState;
    public NPCState_Alert alertState;
    public NPCState_Pursue pursueState;
    public NPCState_MeleeAttack meleeAttackState;
    public NPCState_RangeAttack rangeAttackState;
    public NPCState_Flee fleeState;
    public NPCState_Struck struckState;
    public NPCState_InvestigateHarm investigateHarmState;
    public NPCState_Follow followState;

    void Awake()
    {
        SetupUpStateReferences();
        SetInitialReferences();
        npcMaster.EventNpcLowHealth += ActivateFleeState;
        npcMaster.EventNpcHealthRecovered += ActivatePatrolState;
        npcMaster.EventNpcDeductHealth += ActivateStruckState;
    }

    void Start()
    {
        SetInitialReferences();
    }

    void Update()
    {
        CarryOutUpdateState();
    }

    void OnDisable()
    {
        npcMaster.EventNpcLowHealth -= ActivateFleeState;
        npcMaster.EventNpcHealthRecovered -= ActivatePatrolState;
        npcMaster.EventNpcDeductHealth -= ActivateStruckState;
        StopAllCoroutines();
    }

    void SetupUpStateReferences()
    {
        patrolState = new NPCState_Patrol(this);
        alertState = new NPCState_Alert(this);
        pursueState = new NPCState_Pursue(this);
        fleeState = new NPCState_Flee(this);
        meleeAttackState = new NPCState_MeleeAttack(this);
        rangeAttackState = new NPCState_RangeAttack(this);
        struckState = new NPCState_Struck(this);
        investigateHarmState = new NPCState_InvestigateHarm(this);
    }

    void SetInitialReferences()
    {
        myNavMeshAgent = GetComponent<NavMeshAgent>();
        ActivatePatrolState();
    }

    void CarryOutUpdateState()
    {
        if(Time.time > nextCheck)
        {
            nextCheck = Time.time + checkRate;
            currentState.UpdateState();
        }
    }

    void ActivatePatrolState()
    {
        currentState = patrolState;
    }

    void ActivateFleeState()
    {
        if(currentState == struckState)
        {
            capturedState = fleeState;
            return;
        }

        currentState = fleeState;
    }

    void ActivateStruckState(int dummy)
    {
        StopAllCoroutines();

        if(currentState != struckState)
        {
            capturedState = currentState;
        }

        if(rangeWeapon != null)
        {
            rangeWeapon.SetActive(false);
        }

        if(myNavMeshAgent.enabled)
        {
            myNavMeshAgent.GetComponent<NavMeshAgent>().isStopped = true;
        }

        currentState = struckState;

        npcMaster.CallEventNpcStruckAnim();
        StartCoroutine(RecoverFromStruckState());
    }

    IEnumerator RecoverFromStruckState()
    {
        yield return new WaitForSeconds(1.5f);

        npcMaster.CallEventRecoveredAnim();

        if(rangeWeapon != null)
        {
            rangeWeapon.SetActive(true);
        }

        if(myNavMeshAgent.enabled)
        {
            myNavMeshAgent.GetComponent<NavMeshAgent>().isStopped = false;
        }

        currentState = capturedState;
    }

    public void OnEnemyAttack()
    {
        if(pursueTarget != null)
        {
            if(Vector3.Distance(transform.position, pursueTarget.position) <= meleeAttackRange)
            {
                Vector3 toOther = pursueTarget.position - transform.position;
                if(Vector3.Dot(toOther, transform.forward) > 0.5f)
                {
                    pursueTarget.SendMessage("CallEventPlayerHealthDeduction", meleeAttackDamage, SendMessageOptions.DontRequireReceiver);
                    pursueTarget.SendMessage("ProcessDamage", meleeAttackDamage, SendMessageOptions.DontRequireReceiver);
                }
            }
        }

        isMeleeAttacking = false;
    }

    public void SetMyAttacker(Transform attacker)
    {
        myAttacker = attacker;
    }

    public void Distract(Vector3 DistractionPos)
    {
        locationOfInterest = DistractionPos;

        if(currentState == patrolState)
        {
            currentState = alertState;
        } 
    }
}