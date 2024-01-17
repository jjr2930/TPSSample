using JLib.FSM;
using UnityEngine;
using UnityEngine.AI;

namespace TPSSample
{
    public class AttackState : State
    {
        [SerializeField] float outOfRangeDistance = 0.5f;
        [SerializeField] float attackDelay = 5f;
        float lastAttackTime = 0f;

        NavMeshAgent agent;
        Animator animator;
        Transform targetTransform;
        public override void OnEntered(StateMachineRunner owner)
        {
            base.OnEntered(owner);
            Debug.Log("Attack state entered");
            agent = owner.GetComponent<NavMeshAgent>();
            animator = owner.GetComponentInChildren<Animator>();

            var target = owner.GetStateMachineValue<Object>(StateMachineValueNames.Zombie.TargetObject) as GameObject;
            targetTransform = target.transform;

            lastAttackTime = 0f;
        }

        public override void OnUpdate(StateMachineRunner owner)
        {
            base.OnUpdate(owner);

            if(null == targetTransform)
            {
                owner.PushEvent(FSMEventNames.Zombie.OnEnemyLost);
                return;
            }

            if (IsEnemyOutOfRange())
            {
                owner.PushEvent(FSMEventNames.Zombie.OnOutOfRange);
                return;
            }
            
            //TODO implement enemy dead situation;

            if (Time.time - lastAttackTime >= attackDelay)
            {
                lastAttackTime = Time.time;
                animator.SetTrigger(AnimatorHash.Attack);
                Debug.Log("Attack!");
            }
        }

        bool IsEnemyOutOfRange()
        {
            var sqrDistance = MathUtility.GetSqrDistancePlanar(agent.transform.position, targetTransform.position);
            return sqrDistance >= outOfRangeDistance;
        }
    }
}
