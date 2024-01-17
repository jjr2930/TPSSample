using JLib.FSM;
using UnityEngine;
using UnityEngine.AI;

namespace TPSSample
{
    public class MoveToTargetState : State
    {
        [SerializeField] float setTimeDelay;

        NavMeshAgent navMeshAgent;
        Animator animator;
        Transform targetTransform;
        Vector3 lastDestination;
        float lastSetTime;
        public override void OnEntered(StateMachineRunner owner)
        {
            base.OnEntered(owner);
            navMeshAgent = owner.GetComponent<NavMeshAgent>();
            animator = owner.GetComponentInChildren<Animator>();
            var target = owner.StateMachine.GetValue<Object>(StateMachineValueNames.Zombie.TargetObject) as GameObject;
            targetTransform = target.transform;
            SetAgentDestination();
            //Debug.Log($"destination : {navMeshAgent.destination}, transformPosition : {targetTransform.position}" );

            if (animator.GetCurrentAnimatorStateInfo(0).shortNameHash != AnimatorHash.Walk
                || animator.GetNextAnimatorStateInfo(0).shortNameHash != AnimatorHash.Walk)
            {
                animator.SetTrigger(AnimatorHash.Walk);
            }

            lastSetTime = 0f;
        }
        public override void OnUpdate(StateMachineRunner owner)
        {
            //Debug.Log($"destination : {navMeshAgent.destination}, transformPosition : {targetTransform.position}");
            base.OnUpdate(owner);
            if(null == targetTransform)
            {
                owner.PushEvent(FSMEventNames.Zombie.OnEnemyLost);
                return;
            }

            if(IsEnteredLostRange(owner))
            {
                owner.PushEvent(FSMEventNames.Zombie.OnEnemyLost);
                return;
            }

            if(IsTargetMoved())
            {
                SetAgentDestination();
            }

            if (GetPlanarSqrDistance(owner.transform) <= navMeshAgent.stoppingDistance) 
            {
                Debug.Log("Arrived!");
                owner.PushEvent(FSMEventNames.Zombie.OnArrived);
            }
        }

        bool IsEnteredLostRange(StateMachineRunner owner)
        {
            var lostDistance = owner.GetStateMachineValue<float>(StateMachineValueNames.Zombie.TargetLostDistance);

            return MathUtility.GetSqrDistancePlanar(owner.transform.position, targetTransform.position) >= lostDistance * lostDistance;            
        }

        bool IsTargetMoved()
        {
            return MathUtility.GetSqrDistancePlanar(navMeshAgent.destination, targetTransform.position) > 0.000001f;
        }

        float GetPlanarSqrDistance(Transform ownerTransform)
        {
            var toTarget = targetTransform.position - ownerTransform.position;
            toTarget.y = 0;

            return toTarget.sqrMagnitude;
        }

        void SetAgentDestination()
        {
            var currentTime = Time.time;
            if (currentTime - lastSetTime >= setTimeDelay)
            {
                lastDestination = targetTransform.position;
                navMeshAgent.SetDestination(lastDestination);
                lastSetTime = currentTime;
            }
        }
    }
}
