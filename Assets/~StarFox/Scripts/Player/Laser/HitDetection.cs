using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StarFox
{
    public class HitDetection : MonoBehaviour
    {
        public GameObject hitPrefab;
        public GameObject impactPrefab;
        public string[] collisionTags = new string[] { "Ground" };
        public string[] triggerTags = new string[] { "Player" };

        private Laser laser;

        // Use this for initialization
        void Start()
        {
            laser = GetComponent<Laser>();
        }

        void OnTriggerEnter(Collider other)
        {
            for (int i = 0; i < collisionTags.Length; i++)
            {
                string triggerTag = triggerTags[i];
                // If we have collided with an object that has the collisionTag AND the laser's owner is not the thing we hit
                if (other.tag == triggerTag && laser.owner != other.transform)
                {
                    // Instantiate hitPrefab and impactPrefab
                    // Apply damage to object
                    Destroy(gameObject);    // Destroy laser
                }
            }
        }

        void OnCollisionEnter(Collision other)
        {
            for (int i = 0; i < collisionTags.Length; i++)
            {
                string collisionTag = collisionTags[i];
                // If we have collided with an object that has the collisionTag AND the laser's owner is not the thing we hit
                if (other.collider.tag == collisionTag && laser.owner != other.transform)
                {
                    // Instantiate hitPrefab and impactPrefab
                    // Apply damage to object
                    Destroy(gameObject);    // Destroy laser
                }
            }
        }
    }
}
