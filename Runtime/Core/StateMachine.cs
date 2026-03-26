using System;
using System.Collections.Generic;

namespace WhiteArrow
{
    public class StateMachine<TStateKey>
         where TStateKey : Enum
    {
        private readonly Dictionary<TStateKey, State> _stateMap;
        private readonly Dictionary<TStateKey, List<StateTransition<TStateKey>>> _transitionsMap;

        private State _currentState;
        public TStateKey CurrentStateKey { get; private set; }



        public event Action<TStateKey> StateChanged;



        public StateMachine(
            Dictionary<TStateKey, State> stateMap,
            Dictionary<TStateKey, List<StateTransition<TStateKey>>> transitionsMap,
            TStateKey initialStateId)
        {
            _stateMap = stateMap ?? throw new ArgumentNullException(nameof(stateMap));
            _transitionsMap = transitionsMap ?? throw new ArgumentNullException(nameof(transitionsMap));

            if (!stateMap.ContainsKey(initialStateId))
                throw new ArgumentException($"Initial state '{initialStateId}' is not defined in the state map.");

            CurrentStateKey = initialStateId;
            _currentState = _stateMap[initialStateId];
            _currentState.Enter();
            StateChanged?.Invoke(CurrentStateKey);
        }



        public void OnUpdate()
        {
            foreach (var state in _stateMap.Values)
                state.OnUpdate();

            if (_transitionsMap.TryGetValue(CurrentStateKey, out var possibleTransitions))
            {
                foreach (var transition in possibleTransitions)
                {
                    if (transition.Condition())
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



        private void ChangeState(TStateKey key)
        {
            _currentState.Exit();

            _currentState = _stateMap[key];
            CurrentStateKey = key;

            _currentState.Enter();

            OnStateChanged(key);
            StateChanged?.Invoke(CurrentStateKey);
        }

        protected virtual void OnStateChanged(TStateKey key) { }



        internal protected T GetState<T>(TStateKey key)
            where T : State
        {
            return _stateMap[key] as T;
        }
    }
}