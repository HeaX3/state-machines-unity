using UnityEngine;

namespace StateMachines
{
    public class StateMachineController : MonoBehaviour
    {
        [SerializeField] private string _currentStateName = null;

        public StateMachine StateMachine { get; } = new();

        public State State
        {
            get => StateMachine.State;
            set => StateMachine.State = value;
        }

        private void Awake()
        {
            StateMachine.stateChanged += OnStateChanged;
        }

        private void OnDestroy()
        {
            State = null;
        }

        private void OnApplicationQuit()
        {
            State = null;
        }

        public void Run()
        {
            StateMachine.Run();
        }

        public void Cancel()
        {
            StateMachine.Cancel();
        }

        private void OnStateChanged(State state)
        {
            _currentStateName = state != null ? state.GetType().Name : "";
        }
    }
}