using System;
using System.Collections.Generic;

namespace WhiteArrow
{
    public abstract class NestedStateMachine<TStateKey> : State
        where TStateKey : Enum
    {
        private StateMachine<TStateKey> _stateMachine;



        protected void Init(
            Dictionary<TStateKey, State> stateMap,
            Dictionary<TStateKey, List<StateTransition<TStateKey>>> transitionsMap,
            TStateKey initialStateId)
        {
            _stateMachine = new(stateMap, transitionsMap, initialStateId);
            _stateMachine.StateChanged += OnStateChanged;
        }



        internal protected override void OnUpdate() => _stateMachine.OnUpdate();
        internal protected override void OnFixedUpdate() => _stateMachine.OnFixedUpdate();



        protected virtual void OnStateChanged(TStateKey key) { }



        protected T GetState<T>(TStateKey key)
            where T : State
        {
            return _stateMachine.GetState<T>(key);
        }
    }
}