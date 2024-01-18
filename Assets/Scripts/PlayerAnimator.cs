using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace TPSSample
{
    public class PlayerAnimator : MonoBehaviour
    {
        [SerializeField] Animator animator;
        [SerializeField] Vector2 movingInput;
        [SerializeField] Vector2 lookingInput;
        [SerializeField] bool aimPressed;
        [SerializeField] float changeSpeed;

        public Vector2 MovingInput { get => movingInput; set => movingInput = value; }
        public Vector2 LookingInput { get => lookingInput; set => lookingInput = value; }
        public bool AimPressed { get => aimPressed; set => aimPressed = value; }

        public void Awake()
        {
            if(null == animator)
            {
                animator = GetComponentInChildren<Animator>();
                if(null == animator)
                {
                    Debug.LogError("not found animator");
                    return;
                }
            }
        }

        public void Update()
        {
            var currentX = animator.GetFloat(AnimatorHash.X);
            var currentZ = animator.GetFloat(AnimatorHash.Z);
            var deltaTime = Time.deltaTime;

            if (aimPressed)
            {
                var nextX = movingInput.x;
                var nextZ = movingInput.y;

                animator.SetFloat(AnimatorHash.X, Mathf.MoveTowards(currentX, nextX, changeSpeed * deltaTime));
                animator.SetFloat(AnimatorHash.Z, Mathf.MoveTowards(currentZ, nextZ, changeSpeed * deltaTime));
            }
            else
            {
                var nextZ = movingInput.magnitude;
                animator.SetFloat(AnimatorHash.Z, Mathf.MoveTowards(currentZ, nextZ, changeSpeed * deltaTime));
            }
        }

        public void OnModeChanged(CombatMode mode)
        {
            switch (mode)
            {
                case CombatMode.Neutral:
                    animator.SetInteger(AnimatorHash.Weapon, (int)CombatMode.Neutral);
                    break;

                case CombatMode.Primary:
                    animator.SetInteger(AnimatorHash.Weapon, (int)CombatMode.Primary);
                    break;

                case CombatMode.Secondary:
                    animator.SetInteger(AnimatorHash.Weapon, (int)CombatMode.Secondary);
                    break;

                default:
                    break;
            }
        }


        public void IsGroundChanged(bool value)
        {
            animator.SetBool(AnimatorHash.IsGrounded, value);
        }

        public void OnJumpPressed(bool value)
        {
            if (value)
            {
                animator.SetTrigger(AnimatorHash.Jump);                
            }
        }
    }
}
