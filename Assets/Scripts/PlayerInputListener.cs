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
        GameInput input;
        public UnityEvent<bool> onFirePressed;
        public UnityEvent<Vector2> onMoving;
        public UnityEvent<Vector2> onLooking;
        public UnityEvent<bool> onAimPressed;
        public UnityEvent<bool> onPrimaryPresed;
        public UnityEvent<bool> onSecondaryPressed;
        public UnityEvent<bool> onNeutralPresed;
        public UnityEvent<bool> onJumpPreseed;
        public UnityEvent<bool> onInteracted;


        public void Awake()
        {
            if (null == input)
            {
                input = new GameInput();
                input.Enable();
                input.Human.Enable();
            }
        }

        public void Update()
        {
            onFirePressed?.Invoke(input.Human.Fire.IsPressed());
            onMoving?.Invoke(input.Human.Moving.ReadValue<Vector2>());
            onLooking?.Invoke(input.Human.Looking.ReadValue<Vector2>());
            onAimPressed?.Invoke(input.Human.Aim.IsPressed());

            if (input.Human.PrimaryWeapon.WasPressedThisFrame())
                onPrimaryPresed?.Invoke(true);

            if (input.Human.SecondaryWeapon.WasPressedThisFrame())
                onSecondaryPressed?.Invoke(true);

            if (input.Human.Neutral.WasPressedThisFrame())
                onNeutralPresed?.Invoke(true);

            if (input.Human.Jump.WasPressedThisFrame())
                onJumpPreseed?.Invoke(input.Human.Jump.WasPressedThisFrame());

            onInteracted?.Invoke(input.Human.Interaction.IsPressed());
        }
    }
}
