using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace TPSSample
{
    public class PlayerInputListener : MonoBehaviour
    {
        /// <summary>
        /// first : jump pressed, second : jump pressed this frame?
        /// </summary>
        public UnityEvent<bool> onFirePressed;
        public UnityEvent<Vector2> onMoving;
        public UnityEvent<Vector2> onLooking;
        public UnityEvent<bool> onAimPressed;
        public UnityEvent<bool> onPrimaryPresed;
        public UnityEvent<bool> onSecondaryPressed;
        public UnityEvent<bool> onNeutralPresed;
        public UnityEvent<bool> onJumpPreseed;
        
        private void Fire_performed(InputAction.CallbackContext obj)
        {
            Debug.Log("forformed");
        }

        public void OnMoving(InputValue value)
        {
            var moving = value.Get<Vector2>();
            onMoving?.Invoke(moving);
        }

        public void OnLooking(InputValue value)
        {
            var looking = value.Get<Vector2>();
            onLooking?.Invoke(looking);
        }

        public void OnAim(InputValue value)
        {
            onAimPressed?.Invoke(value.isPressed);
        }

        public void OnPrimaryWeapon(InputValue value)
        {
            onPrimaryPresed?.Invoke(value.isPressed);
        }

        public void OnSecondaryWeapon(InputValue value)
        {
            onSecondaryPressed?.Invoke(value.isPressed);
        }

        public void OnNeutral(InputValue value)
        {
            onNeutralPresed?.Invoke(value.isPressed);
        }

        public void OnJump(InputValue value)
        {
            onJumpPreseed?.Invoke(value.isPressed);
        }

        public void OnFire(InputValue value)
        {
            onFirePressed?.Invoke(value.isPressed);
        }
    }
}
