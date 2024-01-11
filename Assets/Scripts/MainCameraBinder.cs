using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace TPSSample
{
    public class MainCameraBinder : MonoBehaviour
    {
        [SerializeField]
        Transform camera;

        [SerializeField]
        Transform virtualCameraTransform;

        //[SerializeField]
        //float positionLerpSpeed = 10f;

        public void LateUpdate()
        {
            //var nextPosition = Vector3.Lerp (camera.position, virtualCameraTransform.position, positionLerpSpeed);
            
            camera.SetPositionAndRotation(virtualCameraTransform.position, virtualCameraTransform.rotation);
        }
    }
}
