using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GGL;

namespace SuperMarioGalaxy
{
    [RequireComponent(typeof(Rigidbody))]
    public class Player : MonoBehaviour
    {
        public float speed = 20f;
        public float groundDistance = 3f;

        private Rigidbody rigid;
        private GravityNormal body;
        private Vector3 force;

        void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, transform.position + force);

            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + -transform.up * groundDistance);
        }

        // Use this for initialization
        void Start()
        {
            rigid = GetComponent<Rigidbody>();
            body = GetComponent<GravityNormal>();
        }

        public void Move(float inputH, float inputV)
        {
            Vector3 localForce = new Vector3(inputH, 0, inputV);
            force = Quaternion.LookRotation(Camera.main.transform.up, -body.Gravity) * localForce;
            GizmosGL.AddLine(transform.position, transform.position + force, 0.3f, 0.3f, Color.blue, Color.blue);
            rigid.AddForce(force * speed);

            if (body.isGrounded)
            {
                Vector3 groundHitPos = body.HitPoint;
                GizmosGL.AddSphere(groundHitPos, 1f, null, Color.red);
                float distanceToGround = Vector3.Distance(transform.position, groundHitPos);

                GizmosGL.AddSphere(groundHitPos + body.Gravity.normalized * distanceToGround, 1f, null, Color.blue);

                if (distanceToGround > groundDistance)
                {
                    transform.position = groundHitPos + body.Gravity.normalized * distanceToGround;
                }
            }
        }
    }
}
