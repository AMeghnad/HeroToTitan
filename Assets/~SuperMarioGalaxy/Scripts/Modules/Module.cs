using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SuperMarioGalaxy
{
    public class Module : MonoBehaviour
    {
        public delegate void EventCallback();
        public delegate void ActionCallback(GravityBody body);

        public EventCallback onEnter;
        public EventCallback onStay;
        public EventCallback onExit;

        public ActionCallback onAction;

        // Use this for initialization
        public virtual void Enter()
        {
            // Check for subscribed functions
            if (onEnter != null)
            {
                onEnter.Invoke();
            }
        }

        // Update is called once per frame
        public virtual void Stay()
        {
            if (onStay != null)
            {
                onStay.Invoke();
            }
        }

        public virtual void Exit()
        {
            if (onExit != null)
            {
                onExit.Invoke();
            }
        }

        public virtual void Action(GravityBody body)
        {
            if (onAction != null)
            {
                onAction.Invoke(body);
            }
        }
    }
}
