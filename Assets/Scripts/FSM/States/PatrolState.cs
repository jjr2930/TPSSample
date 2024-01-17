using JLib.FSM;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

namespace TPSSample
{
    public class PatrolState : State
    {
        public enum Phase
        {
            Idle,
            Moving
        }

        const string TARGET_TAG_KEY = "TargetTag";
        const string TARGET_OBJECT_KEY = "TargetObject";

        [SerializeField] Vector3 spawnedPosition;
        [SerializeField] LayerMask layerMask;
        [SerializeField] NavMeshAgent navMeshAgent;
        [SerializeField] Animator animator;
        [SerializeField] Phase phase;
        [SerializeField] float idleDuration;
        [SerializeField] float idleElapsedTime;
        [SerializeField] float searchingRadius;
        [SerializeField] LayerMask searchingMask;
        [SerializeField] MinMaxFloat patrolRadiusMinMax;
        public override void OnEntered(StateMachineRunner owner)
        { 
            base.OnEntered(owner);
            spawnedPosition = owner.transform.position;
            navMeshAgent = owner.GetComponent<NavMeshAgent>();
            animator = owner.GetComponentInChildren<Animator>();
            phase = Phase.Idle;
        }
        public override void OnUpdate(StateMachineRunner owner)
        {
            base.OnUpdate(owner);
            var deltaTime = Time.deltaTime;

            var searchingPosition = navMeshAgent.transform.position;
            var colliders = Physics.OverlapSphere(searchingPosition, searchingRadius, searchingMask.value);
            if(colliders.Length > 0)
            {
                float minDistance = float.MaxValue;
                int minIndex = -1;
                //find shortestDistance
                for(int i = 0; i < colliders.Length; ++i)
                {
                    if (colliders[i].transform.tag != owner.GetStateMachineValue<string>(TARGET_TAG_KEY))
                        continue;

                    var sqrDistance = Vector3.SqrMagnitude(colliders[i].transform.position - searchingPosition);
                    if(minDistance > sqrDistance)
                    {
                        minDistance = sqrDistance;
                        minIndex = i;
                    }
                }

                if(minIndex > 0)
                {
                    owner.SetStateMachineValue(TARGET_OBJECT_KEY, colliders[minIndex].gameObject as UnityEngine.Object);
                    owner.PushEvent(FSMEventNames.Zombie.OnEnemyFound);
                }
            }


            switch (phase)
            {
                case Phase.Idle:
                    {
                        idleElapsedTime += deltaTime;
                        if(idleElapsedTime >= idleDuration)
                        {
                            var randomUnitCircle = Random.insideUnitCircle;
                            var randomRadius = patrolRadiusMinMax.GetRandom();
                            var planarPosition = new Vector3(randomUnitCircle.x, 0, randomUnitCircle.y);
                            planarPosition *= randomRadius;

                            Ray ray = new Ray(spawnedPosition + planarPosition + Vector3.up * 10f, Vector3.down);
                            RaycastHit hit;
                            if (Physics.Raycast(ray, out hit, 100, layerMask))
                            {
                                navMeshAgent.SetDestination(hit.point);
                                phase = Phase.Moving;
                                animator.SetTrigger(AnimatorHash.Walk);
                            }
                        }
                    }
                    break;
                case Phase.Moving:
                    {
                        float sqrStopDistance = navMeshAgent.stoppingDistance * navMeshAgent.stoppingDistance;
                        var sqrDistance = MathUtility.GetSqrDistancePlanar(navMeshAgent.transform.position, navMeshAgent.destination);
                        if (sqrDistance <= sqrStopDistance)
                        {
                            phase = Phase.Idle;
                            idleElapsedTime = 0f;
                            animator.SetTrigger(AnimatorHash.Idle);
                        }
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
