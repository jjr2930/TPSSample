using JLib.FSM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR.Haptics;

namespace TPSSample
{
    public class DeathState : State
    {
        [SerializeField] Animator animator;
        public override void OnEntered(StateMachineRunner owner)
        {
            base.OnEntered(owner);
            if (null == animator)
            {
                animator = owner.GetComponent<Animator>();
            }
            animator.SetTrigger(AnimatorHash.Death);
        }
    }
}
