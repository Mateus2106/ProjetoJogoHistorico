using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCState_Patrol : NPCState_Interface
{
    private readonly NPC_StatePattern npc;
    private int nextWayPoint;
    private Collider[] colliders;
    private Vector3 lookAtPoint;
    private Vector3 heading;
    private float dotProd;

    public NPCState_Patrol(NPC_StatePattern npcStatePattern)
    {
        npc = npcStatePattern;
    }

    public void UpdateState()
    {
        Look();
        Patrol();
    }

    public void ToPatrolState() { }
    public void ToAlertState()
    {
        npc.currentState = npc.alertState;
    }
    public void ToPursueState() { }
    public void ToMeleeAttackState() { }
    public void ToRangeAttackState() { }

    void Look()
    {
        colliders = Physics.OverlapSphere(npc.transform.position, npc.sightRange / 3, npc.myEnemyLayers);

        if(colliders.Length > 0)
        {
            VisibilityCalculations(colliders[0].transform);

            if(dotProd > 0)
            {
                AlertStateActions(colliders[0].transform);
                return;
            }
        }
        colliders = Physics.OverlapSphere(npc.transform.position, npc.sightRange, npc.myEnemyLayers);

        foreach(Collider col in colliders)
        {
            RaycastHit hit;

            VisibilityCalculations(col.transform);

            if(Physics.Linecast(npc.head.position, lookAtPoint, out hit, npc.sightLayers))
            {
                foreach(string tags in npc.myEnemyTags)
                {
                    if(hit.transform.CompareTag(tags))
                    {
                        if(dotProd > 0)
                        {
                            AlertStateActions(col.transform);
                            return;
                        }
                    }
                }
            }
        }

    }

    void Patrol()
    {
        npc.meshRendererFlag.material.color = Color.green;

        if(npc.myFollowTarget != null)
        {
            npc.currentState = npc.followState;
        }

        if(!npc.myNavMeshAgent.enabled)
        {
            return;
        }

        if(npc.waypoints.Length > 0)
        {
            MoveTo(npc.waypoints[nextWayPoint].position);
            if(HavelReachedDestination())
            {
                nextWayPoint = (nextWayPoint + 1) % npc.waypoints.Length;
            }
        }

        else
        {
            if(HavelReachedDestination())
            {
                StopWalking();

                if(RandomWanderTarget(npc.transform.position, npc.sightRange, out npc.wanderTarget))
                {
                    MoveTo(npc.wanderTarget);
                }
            }
        }
    }

    void AlertStateActions(Transform target)
    {
        npc.locationOfInterest = target.position;
        ToAlertState();
    }

    void VisibilityCalculations(Transform target)
    {
        lookAtPoint = new Vector3(target.position.x, target.position.y + npc.offset, target.position.z);
        heading = lookAtPoint - npc.transform.position;
        dotProd = Vector3.Dot(heading, npc.transform.position);
    }

    bool RandomWanderTarget(Vector3 centre, float range, out Vector3 result)
    {
        NavMeshHit navHit;

        Vector3 randomPoint = centre + Random.insideUnitSphere * npc.sightRange;
        if(NavMesh.SamplePosition(randomPoint, out navHit, 3.0f, NavMesh.AllAreas))
        {
            result = navHit.position;
            return true;
        }
        else
        {
            result = centre;
            return false;
        }
    }

    bool HavelReachedDestination()
    {
        if(npc.myNavMeshAgent.remainingDistance <= npc.myNavMeshAgent.stoppingDistance && !npc.myNavMeshAgent.pathPending)
        {
            StopWalking();
            return true;
        }
        else
        {
            KeepWalking();
            return false;
        }
    }

    void MoveTo(Vector3 targetPos)
    {
        if(Vector3.Distance(npc.transform.position, targetPos) > npc.myNavMeshAgent.stoppingDistance + 1)
        {
            npc.myNavMeshAgent.SetDestination(targetPos);
            KeepWalking();
        }
    }

    void StopWalking()
    {
        npc.myNavMeshAgent.GetComponent<NavMeshAgent>().isStopped = true;
        npc.npcMaster.CallEventNpcWalkAnim();
    }

    void KeepWalking()
    {
        npc.myNavMeshAgent.GetComponent<NavMeshAgent>().isStopped = false;
        npc.npcMaster.CallEventNpcIdleAnim();
    }
}
