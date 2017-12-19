using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SuperMarioGalaxy
{
    [RequireComponent(typeof(Animator))]
    public class PullStarAnim : MonoBehaviour
    {
        public Animator anim;
        public PullStar pullStar;

        private bool isHovering = false;

        // Use this for initialization
        void Start()
        {
            anim = GetComponent<Animator>();
            pullStar = GetComponent<PullStar>();

            // Subscribe to delegates
            pullStar.onEnter += OnEnter;
            pullStar.onExit += OnExit;
        }

        // Update is called once per frame
        void Update()
        {
            anim.SetBool("isHovering", isHovering);
        }

        void OnEnter()
        {
            isHovering = true;
        }

        void OnExit()
        {
            isHovering = false;
        }
    }
}
