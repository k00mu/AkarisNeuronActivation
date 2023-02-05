using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Akari
{
    public class Arrow : MonoBehaviour
    {
        public Transform target;

        private void Start()
        {
            NerveSystem.Instance.onDestinationGenerated += OnDestinationGenerated;
        }

        private void OnDestinationGenerated(Node node)
        {
            target = node.transform;
        }

        private void Update()
        {
            if (target != null)
            {
                Vector3 dir = target.position - transform.position;
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
        }
    }
}
