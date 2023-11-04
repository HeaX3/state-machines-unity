namespace StateMachines
{
    public class StateMachine
    {
        public delegate void StateEvent(State state);

        public event StateEvent stateChanged = delegate { };

        private State _state;

        public State State
        {
            get => _state;
            set => ApplyState(value);
        }

        public State Sequence(params State[] states)
        {
            var state = new SequenceState(states);
            State = state;
            return state;
        }

        private void ApplyState(State state)
        {
            var active = _state;
            if (state != active && active != null)
            {
                if (!active.IsFinished)
                {
                    active.IsFinished = true;
                    if (active.IsCompleted) active.Complete();
                    else active.Cancel();
                    active.Finish();
                }
            }

            _state = state;
            OnStateChanged(state);
            if (_state != state) return;
            stateChanged(state);
            if (_state != state) return;
            if (state != null) state.Initialize();
        }

        public virtual void Run()
        {
            var current = _state;

            if (current == null) return;
            if (!current.IsInitialized) current.Initialize();
            if (current.IsCancelled)
            {
                if (!current.IsFinished) current.Finish();
                if (current == _state) ApplyState(null);
                return;
            }

            current.Update();
            if (current.IsCompleted)
            {
                if (!current.IsFinished)
                {
                    current.IsFinished = true;
                    current.Complete();
                    current.Finish();
                }
            }

            if (current.IsFinished && _state == current)
            {
                ApplyState(null);
            }
        }

        public void Cancel()
        {
            State = null;
            OnCancel();
        }

        protected virtual void OnCancel()
        {
        }

        protected virtual void OnStateChanged(State state)
        {
        }
    }
}