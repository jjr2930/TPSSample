using JLib.FSM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.XR.Haptics;

namespace TPSSample
{
    public class DeathState : State
    {
        Animator animator;
        NavMeshAgent navAgent;
        public override void OnEntered(StateMachineRunner owner)
        {
            base.OnEntered(owner);
            animator = owner.GetComponentInChildren<Animator>();
            navAgent = owner.GetComponentInChildren<NavMeshAgent>();

            animator.SetTrigger(AnimatorHash.Death);
            navAgent.isStopped = true;
        }
    }
}
