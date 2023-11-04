using System;

namespace StateMachines
{
    public class OptionalActionState : ActionState
    {
        public bool doPerform = true;

        public OptionalActionState(Action action) : base(action)
        {
        }

        protected override void OnInitialize()
        {
            if (!doPerform)
            {
                IsCompleted = true;
                return;
            }

            base.OnInitialize();
        }
    }
}