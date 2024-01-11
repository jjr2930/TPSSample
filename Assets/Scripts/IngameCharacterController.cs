using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TPSSample
{
    [RequireComponent(typeof(CharacterController))]
    public class IngameCharacterController : MonoBehaviour
    {
        [SerializeField] CharacterController characterController;
        [SerializeField] Vector2 movingInput;
        [SerializeField] Vector2 lookingInput;

        [Header("Camera")]
        [SerializeField] Transform virtualCameraTrasnform;
        [SerializeField] MinMaxFloat elevationMinMax;
        [SerializeField] float distance;
        [SerializeField] Vector3 lookingOffset;
        [SerializeField] LayerMask raycastingLayerMask;

        [Header("Charecter")]
        [SerializeField] float jumpPower;
        [SerializeField] float maxSpeed;
        [SerializeField] float accel;
        [SerializeField] float friction;

        [Header("Epsilons")]
        [SerializeField] float movingInputEpsilon;


        float sqrMovingInputEpsilon;

        public Vector2 MovingInput 
        { 
            get => movingInput; 
            set => movingInput = value; 
        }
        
        public Vector2 LookingInput 
        {
            get => lookingInput; 
            set => lookingInput = value; 
        }

        public void Reset()
        {
            characterController = GetComponent<CharacterController>();
        }


        public void Awake()
        {
            if(null == virtualCameraTrasnform)
            {
                var go = new GameObject("virtual camera transform");
                virtualCameraTrasnform = go.transform;
            }

            CalculateSqrEpsilons();
        }
        public void Update()
        {
            CalculateSqrEpsilons();
            UpdateMovement();   
        }

        void CalculateSqrEpsilons()
        {
            sqrMovingInputEpsilon = movingInputEpsilon * movingInputEpsilon;
        }

        public void UpdateMovement()
        {            
            var cameraForward = virtualCameraTrasnform.transform.forward;
            var cameraRight = virtualCameraTrasnform.transform.right;
            var cameraForwardOnPlanar = MathUtility.ProjectToPlane(cameraForward, Vector3.up);
            var cameraRightOnPalar = MathUtility.ProjectToPlane(cameraRight, Vector3.up);
            var deltaTime = Time.deltaTime;

            Vector3 velocity = characterController.velocity;
        

            Vector3 accelDirection = Vector3.zero;
            Debug.Log($"cc velocity : {characterController.velocity}");
            if(movingInput.sqrMagnitude > sqrMovingInputEpsilon)
            {
                accelDirection += cameraForwardOnPlanar * movingInput.y;
                accelDirection += cameraRightOnPalar * movingInput.x;
            }

            accelDirection.Normalize();
            accelDirection *= accel;
            
            if (velocity.sqrMagnitude >= 0f && accelDirection.sqrMagnitude <= 0f)
            {
                var dir = velocity.normalized;
                velocity -= dir * friction;
            }

            velocity += accelDirection;
            // Debug.Log($"accel : {accelDirection} velocity : {velocity}");
            velocity = MathUtility.ClmapVectorLength(velocity, 0, maxSpeed);
            //Debug.Log($"accel : {accelDirection} velocity : {velocity}");
            characterController.Move(velocity * deltaTime);
        }

        public void UpdateCamera()
        {
        }
    }
}
