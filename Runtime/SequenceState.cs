namespace StateMachines
{
    public class SequenceState : State
    {
        private State[] states { get; }

        private int index;
        private readonly StateMachine stateMachine = new();

        public SequenceState(params State[] states)
        {
            this.states = states;
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();
            if (states.Length == 0)
            {
                IsCompleted = true;
                return;
            }

            stateMachine.State = states[0];
        }

        public override void Update()
        {
            stateMachine.Run();
            while (stateMachine.State is null or { IsFinished: true })
            {
                index++;
                if (index >= states.Length)
                {
                    IsCompleted = true;
                    return;
                }

                stateMachine.State = states[index];
            }
        }

        protected override void OnCancel()
        {
            base.OnCancel();
            stateMachine.Cancel();
        }
    }
}