using System;
using UnityEngine;

namespace StateMachines
{
    public class ActionState : State
    {
        private readonly Action _action;
        public string info;

        public ActionState(Action action)
        {
            _action = action;
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();
            try
            {
                _action.Invoke();
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message + "\n" + e.StackTrace);
            }
            finally
            {
                IsCompleted = true;
            }
        }

        public override void Update()
        {
            // nothing to do here
        }

        public override string ToString()
        {
            return GetType().Name + (!string.IsNullOrWhiteSpace(info) ? ":\n" + info : "");
        }
    }
}