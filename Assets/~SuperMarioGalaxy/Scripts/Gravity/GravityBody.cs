using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SuperMarioGalaxy
{
    [RequireComponent(typeof(Rigidbody))]
    public class GravityBody : MonoBehaviour
    {
        public virtual Vector3 Gravity
        {
            get
            {
                // If there are no attractions
                if (attractors.Count == 0)
                {
                    // Return default gravity
                    return Physics.gravity;
                }
                // Reset gravity before calculating
                Vector3 gravity = Vector3.zero;
                // Loop through each gravity attractor
                foreach (GravityAttractor a in attractors)
                {
                    // Append gravity
                    gravity += a.GetGravity(transform.position);
                }
                // Return gravity
                return gravity;
            }
        }

        private List<GravityAttractor> attractors = new List<GravityAttractor>();
        private Rigidbody rigid;
        private Vector3 normal;

        // Use this for initialization
        void Start()
        {
            rigid = GetComponent<Rigidbody>();
            rigid.useGravity = false;
        }

        // Update is called once per frame
        void Update()
        {
            Vector3 gravity = Gravity;
            rigid.AddForce(gravity);

            normal = Vector3.Lerp(normal, gravity, 10 * Time.deltaTime);
            // Rotate to surface normal
            transform.up = -normal;
        }

        void OnTriggerEnter(Collider other)
        {
            GravityAttractor a = other.GetComponent<GravityAttractor>();
            if (a != null)
            {
                // Add attractor to the list
                attractors.Add(a);
            }
        }

        void OnTriggerExit(Collider other)
        {
            // Try getting gravity attractor component
            GravityAttractor a = other.GetComponent<GravityAttractor>();
            if (a != null)
            {
                // Remove attractor to the list
                attractors.Remove(a);
            }
        }
    }
}
