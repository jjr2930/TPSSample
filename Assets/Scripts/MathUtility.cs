using System.Collections;
using Unity.Hierarchy;
using Unity.Mathematics;
using UnityEngine;

namespace TPSSample
{
    public static class MathUtility 
    {
        public static Vector3 ProjectToPlane(Vector3 vector, Vector3 planeUp)
        {
            return vector - MathUtility.Project(vector, planeUp);
        }

        public static Vector3 Project(Vector3 v1, Vector3 v2)
        {
            return (Vector3.Dot(v1, v2) / Vector3.Dot(v2, v2)) * v2;
        }

        public static Vector3 ClmapVectorLength(Vector3 v1, float min, float max)
        {
            var normalized = v1.normalized;
            if (v1.magnitude < min)
            {
                return normalized * min;
            }
            else if (v1.magnitude > max)
            {
                return normalized * max;
            }
            else
            {
                return v1;
            }
        }

        public static Vector3 SphereicalPosition(float radius, float elevation, float polar)
        {
            //sin , cos save? to dictionary?
            var a = radius * Mathf.Cos(elevation);
            return new Vector3
            (
                a * Mathf.Sin(polar),
                radius * Mathf.Sin(elevation),
                a * Mathf.Cos(polar)
            );
        }

        public static float GetSqrDistancePlanar(Vector3 v1, Vector3 v2)
        {
            var dir = v2 - v1;
            dir.y = 0f;
            return dir.sqrMagnitude;
        }

        public static float GetDistancePlanar(Vector3 v1, Vector3 v2)
        {
            var dir = v2 - v1;
            dir.y = 0f;
            return dir.magnitude;
        }
    }
}
