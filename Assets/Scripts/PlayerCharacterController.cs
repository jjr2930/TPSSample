using System.Collections;
using System.Collections.Generic;
using UnityEditor.AddressableAssets.BuildReportVisualizer;
using UnityEngine;
using UnityEngine.Events;
using UnityEngineInternal;

namespace TPSSample
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerCharacterController : MonoBehaviour
    {
        [Header("Input from others")]
        [SerializeField] CharacterController characterController;
        [SerializeField] Vector2 movingInput;
        [SerializeField] Vector2 lookingInput;
        [SerializeField] bool aimPressed;
        [SerializeField] bool jumpPresed;

        [Header("Camera")]
        [SerializeField] Transform virtualCameraTrasnform;
        [SerializeField] MinMaxFloat elevationMinMax;
        [SerializeField] float distance;
        [SerializeField] Vector3 lookingOffset;
        [SerializeField] Vector3 positionOffset;
        [SerializeField] LayerMask raycastingLayerMask;
        [SerializeField] float rotationSpeed;
        [SerializeField] float aimDistance;
        [SerializeField] float normalDistance;
        [SerializeField] float elevation;
        [SerializeField] float polar;

        [Header("Charecter")]
        [SerializeField] float jumpHeight;
        [SerializeField] float maxSpeed;
        [SerializeField] float accel;
        [SerializeField] float friction;
        [SerializeField] bool isGrounded;
        [SerializeField] float groundRadius;
        [SerializeField] float groundSphereOffset;
        [SerializeField] LayerMask groundedCheckingLayer;
        [SerializeField] Transform animatorTransform;

        [Header("Epsilons")]
        [SerializeField] float movingInputEpsilon;

        float sqrMovingInputEpsilon;

        [SerializeField] UnityEvent<bool> IsGroundChanged;
        [SerializeField] UnityEvent<Transform> onVirtualCameraTransformChanged;
        RaycastHit hit = new RaycastHit();
        
        float verticalVelcity;

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
        public bool AimPressed 
        {
            get => aimPressed; 
            set => aimPressed = value; 
        }
        public bool JumpPresed
        { 
            get => jumpPresed; 
            set => jumpPresed = value; 
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
            UpdateJumpAndGravity();
            UpdateGrounded();
            UpdateMovement();
            UpdateCamera();

            //isGrounded = characterController.isGrounded;
        }

        void CalculateSqrEpsilons()
        {
            sqrMovingInputEpsilon = movingInputEpsilon * movingInputEpsilon;
        }
        
        public void UpdateGrounded()
        {
            // set sphere position, with offset
            Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - groundSphereOffset,
                transform.position.z);

            var previous = isGrounded;
            isGrounded = Physics.CheckSphere(spherePosition, groundRadius, groundedCheckingLayer.value,
                QueryTriggerInteraction.Ignore);

            if (previous != isGrounded)
            {
                IsGroundChanged.Invoke(isGrounded);
            }
        }

        public void UpdateJumpAndGravity()
        {
            if (isGrounded)
            {
                if (verticalVelcity < 0f)
                    verticalVelcity = 0f;
                //// reset the fall timeout timer
                //_fallTimeoutDelta = FallTimeout;

                //// update animator if using character
                //if (_hasAnimator)
                //{
                //    _animator.SetBool(_animIDJump, false);
                //    _animator.SetBool(_animIDFreeFall, false);
                //}

                //// stop our velocity dropping infinitely when grounded
                //if (_verticalVelocity < 0.0f)
                //{
                //    _verticalVelocity = -2f;
                //}
                if(jumpPresed)
                {
                    jumpPresed = false;
                    verticalVelcity = Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y);
                }
                //// Jump
                //if (_input.jump && _jumpTimeoutDelta <= 0.0f)
                //{
                //    // the square root of H * -2 * G = how much velocity needed to reach desired height
                //    _verticalVelocity = Mathf.Sqrt(JumpHeight * -2f * Gravity);

                //    // update animator if using character
                //    if (_hasAnimator)
                //    {
                //        _animator.SetBool(_animIDJump, true);
                //    }
                //}

                //// jump timeout
                //if (_jumpTimeoutDelta >= 0.0f)
                //{
                //    _jumpTimeoutDelta -= Time.deltaTime;
                //}
            }
            else
            {
                // reset the jump timeout timer
                //    _jumpTimeoutDelta = JumpTimeout;

                //    // fall timeout
                //    if (_fallTimeoutDelta >= 0.0f)
                //    {
                //        _fallTimeoutDelta -= Time.deltaTime;
                //    }
                //    else
                //    {
                //        // update animator if using character
                //        if (_hasAnimator)
                //        {
                //            _animator.SetBool(_animIDFreeFall, true);
                //        }
                //    }

                //    // if we are not grounded, do not jump
                //    _input.jump = false;
                //}

                //// apply gravity over time if under terminal (multiply by delta time twice to linearly speed up over time)
                //if (_verticalVelocity < _terminalVelocity)
                //{
                //    _verticalVelocity += Gravity * Time.deltaTime;

                verticalVelcity += Physics.gravity.y * Time.deltaTime;
            }
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
            
            velocity = MathUtility.ClmapVectorLength(velocity, 0, maxSpeed);
            
            velocity.y = verticalVelcity;

            characterController.Move(velocity * deltaTime);
            
            var cameraForwardPlanar = MathUtility.ProjectToPlane(virtualCameraTrasnform.forward, Vector3.up);
            var lookPosition = virtualCameraTrasnform.position + cameraForwardPlanar * 1000f;
            var characterToLook = lookPosition - characterController.transform.position;
            characterController.transform.rotation = Quaternion.LookRotation(characterToLook, Vector3.up);

            if (aimPressed)
            {
                animatorTransform.forward = characterController.transform.forward;
            }
            else
            {
                if (accelDirection.sqrMagnitude > 0f)
                {
                    var nextRotation = Quaternion.LookRotation(accelDirection, Vector3.up);
                    nextRotation = Quaternion.RotateTowards(animatorTransform.rotation, nextRotation, 720f * deltaTime);
                    animatorTransform.rotation = nextRotation;
                }
            }
        }

        public void UpdateCamera()
        {
            var characterForwardPlanar = MathUtility.ProjectToPlane(characterController.transform.forward, Vector3.up);
            var characterRightPlanar = MathUtility.ProjectToPlane(characterController.transform.right, Vector3.up);
            var characterUp = Vector3.up;

            var cameraForwardPlanar = MathUtility.ProjectToPlane(virtualCameraTrasnform.transform.forward, Vector3.up);
            var cameraRightPlanar = MathUtility.ProjectToPlane(virtualCameraTrasnform.transform.right, Vector3.up);
            var cameraUp = Vector3.up;


            distance = (aimPressed) ? aimDistance : normalDistance;

            var deltaTime = Time.deltaTime;
            elevation += -lookingInput.y * deltaTime * rotationSpeed;
            polar += lookingInput.x * deltaTime * rotationSpeed;

            elevation = Mathf.Clamp(elevation, elevationMinMax.min, elevationMinMax.max);

            var relatedPosition = MathUtility.SphereicalPosition(distance, elevation * Mathf.Deg2Rad, polar * Mathf.Deg2Rad);
            var relatedOffset = positionOffset.x * characterRightPlanar
                                + positionOffset.y * characterUp
                                + positionOffset.z * characterForwardPlanar;
            
            var relatedLookingOffset = lookingOffset.x * characterRightPlanar
                                        + lookingOffset.y * characterUp
                                        + lookingOffset.z * characterForwardPlanar;

            var previousPosition = virtualCameraTrasnform.position;
            virtualCameraTrasnform.position = this.transform.position + relatedPosition + relatedOffset;
            var previousRotation = virtualCameraTrasnform.rotation;
            virtualCameraTrasnform.LookAt(characterController.transform.position + relatedLookingOffset, Vector3.up);

            if (virtualCameraTrasnform.position != previousPosition
                || virtualCameraTrasnform.rotation != previousRotation)
            {
                onVirtualCameraTransformChanged.Invoke(virtualCameraTrasnform);
            }
        }

        private void OnDrawGizmosSelected()
        {
            Color transparentGreen = new Color(0.0f, 1.0f, 0.0f, 0.35f);
            Color transparentRed = new Color(1.0f, 0.0f, 0.0f, 0.35f);

            if (isGrounded) Gizmos.color = transparentGreen;
            else Gizmos.color = transparentRed;

            // when selected, draw a gizmo in the position of, and matching radius of, the grounded collider
            Gizmos.DrawSphere(
                new Vector3(transform.position.x, transform.position.y - groundSphereOffset, transform.position.z),
                groundRadius);
        }
    }
}
