using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Akari
{
    public class Line : MonoBehaviour
    {
        public Transform StartPoint;
        public Transform EndPoint;

        private LineRenderer lineRenderer;

        private void Awake()
        {
            lineRenderer = GetComponent<LineRenderer>();
        }

        public void SetPosition(Transform startPoint, Transform endPoint)
        {
            StartPoint = startPoint;
            EndPoint = endPoint;

            lineRenderer.SetPosition(0, StartPoint.position);
            lineRenderer.SetPosition(1, EndPoint.position);
        }
    }
}
