using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Akari
{
    public class CameraMovement : MonoBehaviour
    {
        public Transform Target;
        public float SmoothTime = 0.3f;
        public Vector3 Offset;

        private Vector3 velocity = Vector3.zero;

        private void Update()
        {
            Vector3 targetPosition = Target.position + Offset;
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, SmoothTime);
        }
    }
}
