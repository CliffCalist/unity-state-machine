using System;
using System.Collections.Generic;
using UnityEngine;

namespace WhiteArrow
{
    public abstract class MonoStateMachine<TStateKey> : MonoBehaviour
        where TStateKey : Enum
    {
        protected StateMachine<TStateKey> _stateMachine { get; private set; }



        protected void InitStateMachine(
            Dictionary<TStateKey, State> stateMap,
            Dictionary<TStateKey, List<StateTransition<TStateKey>>> transitionsMap,
            TStateKey initialStateId)
        {
            _stateMachine = new(stateMap, transitionsMap, initialStateId);
            _stateMachine.StateChanged += OnStateChanged;
        }



        private void Update()
        {
            _stateMachine?.OnUpdate();
            OnUpdateCore();
        }

        protected virtual void OnUpdateCore() { }



        private void FixedUpdate()
        {
            _stateMachine?.OnFixedUpdate();
            OnFixedUpdateCore();
        }

        protected virtual void OnFixedUpdateCore() { }



        protected virtual void OnStateChanged(TStateKey id) { }



        protected T GetState<T>(TStateKey id)
            where T : State
        {
            return _stateMachine.GetState<T>(id);
        }
    }
}