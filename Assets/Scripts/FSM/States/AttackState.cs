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
        ZombieAnimatorListener animatorListener;
        CharacterData characterData;
        public override void OnEntered(StateMachineRunner owner)
        {
            base.OnEntered(owner);
            Debug.Log("Attack state entered");
            agent = owner.GetComponent<NavMeshAgent>();
            animator = owner.GetComponentInChildren<Animator>();
            animatorListener = owner.GetComponentInChildren<ZombieAnimatorListener>();
            characterData = owner.GetComponentInChildren<CharacterData>();

            var target = owner.GetStateMachineValue<Object>(StateMachineValueNames.Zombie.TargetObject) as GameObject;
            targetTransform = target.transform;

            animatorListener.onAttacked.AddListener(OnAttacked);

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

        public override void OnExit(StateMachineRunner owner)
        {
            base.OnExit(owner);

            animatorListener.onAttacked.RemoveListener(OnAttacked);
        }

        bool IsEnemyOutOfRange()
        {
            var sqrDistance = MathUtility.GetSqrDistancePlanar(agent.transform.position, targetTransform.position);
            return sqrDistance >= outOfRangeDistance;
        }

        void OnAttacked()
        {
            var targetCharacterData = targetTransform.GetComponent<CharacterData>();
            targetCharacterData.OnDamaged(characterData.Data.attack);
        }
    }
}
