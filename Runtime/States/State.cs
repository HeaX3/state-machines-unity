using System;

namespace StateMachines
{
    public abstract class State
    {
        public delegate void StateEvent();

        public event StateEvent initialized = delegate { };
        public event StateEvent completed = delegate { };
        public event StateEvent cancelled = delegate { };
        public event StateEvent finished = delegate { };

        public bool IsInitialized { get; private set; }
        public bool IsCompleted { get; protected set; }
        public bool IsCancelled { get; private set; }
        public bool IsFinished { get; internal set; }

        public void Initialize()
        {
            IsInitialized = true;
            OnInitialize();
            initialized();
        }

        protected virtual void OnInitialize()
        {
        }

        public abstract void Update();

        public void Complete()
        {
            if (!IsCompleted) IsCompleted = true;
            OnComplete();
            completed();
        }

        protected virtual void OnComplete()
        {
        }

        public void Cancel()
        {
            IsCancelled = true;
            OnCancel();
            cancelled();
        }

        protected virtual void OnCancel()
        {
        }

        internal void Finish()
        {
            IsFinished = true;
            OnFinish();
            finished();
        }

        protected virtual void OnFinish()
        {
        }

        public State Then(Action resolve, Action reject = null)
        {
            completed += resolve.Invoke;
            if (reject != null) cancelled += reject.Invoke;
            return this;
        }

        public State After(Action callback)
        {
            finished += callback.Invoke;
            return this;
        }

        public State OnCompleted(Action callback)
        {
            completed += callback.Invoke;
            return this;
        }

        public State OnCancelled(Action callback)
        {
            cancelled += callback.Invoke;
            return this;
        }

        public State OnFinished(Action callback)
        {
            finished += callback.Invoke;
            return this;
        }
    }
}