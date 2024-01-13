using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TPSSample
{
    [RequireComponent(typeof(CharacterController))]
    public class MonsterCharacterController : MonoBehaviour
    {
        [SerializeField] CharacterController characterController;
        [SerializeField] Transform movingDirection;
        [SerializeField] float movingSpeed;
        [SerializeField] float rotationSpeed;
        [SerializeField] bool isGrounded;

        public void Reset()
        {
            characterController = GetComponent<CharacterController>();
        }

        public void Update()
        {
            UpdateJumpAndGravity();
            UpdateIsGround();
            UpdateMove();
        }

        private void UpdateMove()
        {
            throw new NotImplementedException();
        }

        private void UpdateIsGround()
        {
            throw new NotImplementedException();
        }

        private void UpdateJumpAndGravity()
        {
            throw new NotImplementedException();
        }
    }
}
