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
        public float triggerRadius = 2f;

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

        CameraTrigger Scan(Vector3 position)
        {
            Collider[] hits = Physics.OverlapSphere(position, triggerRadius);
            foreach (Collider col in hits)
            {
                CameraTrigger cameraTrigger = col.GetComponent<CameraTrigger>();
                if (cameraTrigger != null)
                    return cameraTrigger;
            }
            return null;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            // Is there a valid target?
            if (target != null)
            {
                CameraTrigger cameraTrigger = Scan(target.transform.position);
                if (cameraTrigger != null)
                {
                    // Get the closest node tied to the camera trigger
                    CameraNode focusNode = cameraTrigger.GetClosestNode(target.transform.position);
                    // Position the camera to that closest camera node
                    transform.position = Vector3.Lerp(transform.position, focusNode.transform.position, speed * Time.deltaTime);
                    // Is the node's follow target setting enabled?
                    if (focusNode.followTarget)
                    {
                        // Follow the target (rotate to the target)
                        Vector3 direction = target.transform.position - transform.position;
                        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction, transform.up), speed * Time.deltaTime);
                    }
                    else
                    {
                        // Rotate to node's orientation (don't follow target)
                        transform.rotation = Quaternion.Slerp(transform.rotation, focusNode.transform.rotation, speed * Time.deltaTime);
                    }
                }
                else
                {
                    float distance = Vector3.Distance(centrePos, target.transform.position);
                    // Is target's distance further from centre of sphere?
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
}
