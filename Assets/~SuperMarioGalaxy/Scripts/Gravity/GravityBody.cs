using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SuperMarioGalaxy
{
    [RequireComponent(typeof(Rigidbody))]
    public class GravityBody : MonoBehaviour
    {
        public bool useGravity = true;
        public virtual Vector3 Gravity
        {
            get
            {
                // Is useGravity false?
                if (!useGravity)
                {
                    // Return zero gs
                    return Vector3.zero;
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
        public Vector3 Velocity
        {
            get
            {
                return rigid.velocity;
            }
            set
            {
                rigid.velocity = value;
            }
        }

        private List<GravityAttractor> attractors = new List<GravityAttractor>();
        private Rigidbody rigid;
        private Vector3 normal;

        public void AddForce(Vector3 velocity, ForceMode forceMode = ForceMode.Acceleration)
        {
            rigid.AddForce(velocity, forceMode);
        }

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
