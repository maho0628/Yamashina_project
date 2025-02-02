using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Toshiki
{
    public class PlayerState : MonoBehaviour
    {
        protected PlayerStateMachine stateMachine;

        //protected string animBoolName;
        //protected bool triggerCalled;
        //protected float stateTimer;

        public virtual void Enter()
        {
            //triggerCalled = false;
        }

        public virtual void Update()
        {
            //stateTimer -= Time.deltaTime;
        }

        public virtual void Exit() { }

        public void AnimationFinishTrigger()
        {
            //triggerCalled = true;
        }
    }
}