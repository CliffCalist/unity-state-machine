using System;
using System.Collections.Generic;

namespace WhiteArrow
{
    public class StateMachine<TStateEnum>
         where TStateEnum : Enum
    {
        private Dictionary<TStateEnum, State> _stateMap;
        private Dictionary<TStateEnum, List<StateTransition<TStateEnum>>> _transitionsMap;

        private State _currentState;
        public TStateEnum CurrentStateId { get; private set; }


        public bool IsSettingsSeted => _stateMap != null && _transitionsMap != null;


        public event Action<TStateEnum> StateChanged;



        public StateMachine(
            Dictionary<TStateEnum, State> stateMap,
            Dictionary<TStateEnum, List<StateTransition<TStateEnum>>> transitionsMap,
            TStateEnum initialStateId)
        {
            _stateMap = stateMap ?? throw new ArgumentNullException(nameof(stateMap));
            _transitionsMap = transitionsMap ?? throw new ArgumentNullException(nameof(transitionsMap));

            if (!stateMap.ContainsKey(initialStateId))
                throw new ArgumentException($"Initial state '{initialStateId}' is not defined in the state map.");

            CurrentStateId = initialStateId;
            _currentState = _stateMap[initialStateId];
            _currentState.Enter();
            StateChanged?.Invoke(CurrentStateId);
        }



        public void OnUpdate()
        {
            if (!IsSettingsSeted)
                return;

            foreach (var state in _stateMap.Values)
                state.OnUpdate();

            if (_transitionsMap.TryGetValue(CurrentStateId, out var possibleTransitions))
            {
                foreach (var transition in possibleTransitions)
                {
                    if (transition.CanChangeToThis())
                    {
                        ChangeState(transition.To);
                        break;
                    }
                }
            }

            OnUpdateCore();
        }

        protected virtual void OnUpdateCore() { }



        public void OnFixedUpdate()
        {
            foreach (var state in _stateMap.Values)
                state.OnFixedUpdate();

            OnFixedUpdateCore();
        }

        protected virtual void OnFixedUpdateCore() { }



        private void ChangeState(TStateEnum id)
        {
            _currentState.Exit();

            _currentState = _stateMap[id];
            CurrentStateId = id;

            _currentState.Enter();

            OnStateChanged(id);
            StateChanged?.Invoke(CurrentStateId);
        }

        protected virtual void OnStateChanged(TStateEnum id) { }



        internal protected T GetState<T>(TStateEnum id)
            where T : State
        {
            return _stateMap[id] as T;
        }
    }
}