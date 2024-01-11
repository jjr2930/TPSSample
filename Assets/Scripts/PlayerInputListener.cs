using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TPSSample
{
    public class PlayerInputListener : MonoBehaviour
    {
        [SerializeField]
        IngameCharacterController cc;

        public void Reset()
        {
            cc = GetComponent<IngameCharacterController>();
        }

        public void OnMoving(InputValue value)
        {
            cc.MovingInput = value.Get<Vector2>();
        }

        public void OnLooking(InputValue value)
        {
            cc.LookingInput = value.Get<Vector2>();
        }

        public void OnAim(InputValue value)
        {
            cc.AimPressed = value.isPressed;
        }
    }
}
