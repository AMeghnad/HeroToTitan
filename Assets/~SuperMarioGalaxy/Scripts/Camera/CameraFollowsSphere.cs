using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SuperMarioGalaxy
{
    public class CameraFollowsSphere : MonoBehaviour
    {
        public GravityBody target;
        public Vector3 offset = new Vector3(0, 0, -20);
        public float speed = 5f;
        public float sphereRadius = 5f;

        private Vector3 centrePos; // Camera centre

        void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(centrePos, sphereRadius);
        }

        // Use this for initialization
        void Start()
        {
            centrePos = target.transform.position;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            // Is there a valid target?
            if (target != null)
            {
                float distance = Vector3.Distance(centrePos, target.transform.position);
                // Is target's distance further from centre of sphere
                if (distance > sphereRadius)
                {
                    // Move centre to new target pos
                    centrePos = Vector3.Lerp(centrePos, target.transform.position, speed * Time.deltaTime);
                }
                Vector3 worldOffset = Quaternion.LookRotation(target.Gravity) * offset;
                transform.position = Vector3.Lerp(transform.position, centrePos + worldOffset, speed * Time.deltaTime);
                transform.LookAt(target.transform, transform.up);
            }
        }
    }
}
