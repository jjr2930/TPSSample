using JLib.FSM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TPSSample
{
    public class MoveToTargetState : State
    {
        public override void OnUpdate(StateMachineRunner owner)
        {
            base.OnUpdate(owner);
            Debug.Log("move to target");
        }
    }
}
