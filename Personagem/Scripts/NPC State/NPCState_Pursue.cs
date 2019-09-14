using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCState_Pursue : NPCState_Interface
{
    private readonly NPC_StatePattern npc;
    private float capturedDistance;

    public NPCState_Pursue(NPC_StatePattern npcStatePattern)
    {
        npc = npcStatePattern;
    }

    public void UpdateState()
    {
        Look();
        Pursue();
    }
    public void ToPatrolState()
    {
        KeepWalking();
        npc.currentState = npc.patrolState;
    }
    public void ToAlertState()
    {
        KeepWalking();
        npc.currentState = npc.alertState;
    }
    public void ToPursueState() { }
    public void ToMeleeAttackState()
    {
        npc.currentState = npc.meleeAttackState;
    }
    public void ToRangeAttackState()
    {
        npc.currentState = npc.rangeAttackState;
    }

    void Look()
    {
        if(npc.pursueTarget == null)
        {
            ToPatrolState();
            return;
        }

        Collider[] colliders = Physics.OverlapSphere(npc.transform.position, npc.sightRange, npc.myEnemyLayers);

        if(colliders.Length == 0)
        {
            npc.pursueState = null;
            ToPatrolState();
            return;
        }

        capturedDistance = npc.sightRange * 2;

        foreach(Collider col in colliders)
        {
            float distanceToTarg = Vector3.Distance(npc.transform.position, col.transform.position);

            if(distanceToTarg < capturedDistance)
            {
                capturedDistance = distanceToTarg;
                npc.pursueTarget = col.transform.root;
            }
        }
    }

    void Pursue()
    {
        if(npc.myNavMeshAgent.enabled && npc.pursueTarget != null)
        {
            npc.myNavMeshAgent.SetDestination(npc.pursueTarget.position);
            npc.locationOfInterest = npc.pursueTarget.position;
            KeepWalking();

            float distanceToTarget = Vector3.Distance(npc.transform.position, npc.pursueTarget.position);

            if(distanceToTarget <= npc.rangeAttackRange && distanceToTarget > npc.meleeAttackRange)
            {
                if(npc.hasRangeAttack)
                {
                    ToRangeAttackState();
                }
            }

            else if(distanceToTarget <= npc.meleeAttackRange)
            {
                if(npc.hasMeleeAttack)
                {
                    Debug.Log("teste2");
                    ToMeleeAttackState();
                }
                else if(npc.hasRangeAttack)
                {
                    ToRangeAttackState();
                }
            }
        }

        else
        {
            ToAlertState();
        }
    }

    void KeepWalking()
    {
        npc.myNavMeshAgent.GetComponent<NavMeshAgent>().isStopped = false;
        npc.npcMaster.CallEventNpcWalkAnim();
    }
}
