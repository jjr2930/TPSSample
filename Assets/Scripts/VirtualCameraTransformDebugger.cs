using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TPSSample
{
    public class VirtualCameraTransformDebugger : MonoBehaviour
    {
        [SerializeField] LayerMask layerMask;
        [SerializeField] float radius = 0.1f;
        [SerializeField] Vector3 forward;

        public void Update()
        {
            forward = transform.forward;
        }
        public void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Ray ray = new Ray(transform.position, transform.forward * 1000f);
            RaycastHit hit = new RaycastHit();
            Gizmos.DrawRay(ray);
            Gizmos.color = new Color(0f, 1f, 0f, 0.5f);
            if (Physics.Raycast(ray, out hit, 1000f, layerMask.value))
            {
                //Debug.Log("Debugger hit point : " + hit.point);
                Gizmos.DrawSphere(hit.point, radius);
            }
        }
    }
}
