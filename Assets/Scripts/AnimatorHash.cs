using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TPSSample
{
    public static class AnimatorHash 
    {
        public static readonly int X = Animator.StringToHash("X");
        public static readonly int Z = Animator.StringToHash("Z");
        public static readonly int Jump = Animator.StringToHash("Jump");
        public static readonly int Weapon = Animator.StringToHash("Weapon");
        public static readonly int IsGrounded = Animator.StringToHash("IsGrounded");
        public static readonly int Idle = Animator.StringToHash("Idle");
        public static readonly int Walk = Animator.StringToHash("Walk");
        public static readonly int Attack = Animator.StringToHash("Attack");
        public static readonly int Death = Animator.StringToHash("Death");
    }
}
