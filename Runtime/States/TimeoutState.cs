using UnityEngine;

namespace StateMachines
{
    public class TimeoutState : State
    {
        private float timeout { get; }

        private float _t;

        public TimeoutState(float timeout)
        {
            this.timeout = timeout;
        }

        public override void Update()
        {
            _t += Time.deltaTime;
            IsCompleted |= _t >= timeout;
        }
    }
}