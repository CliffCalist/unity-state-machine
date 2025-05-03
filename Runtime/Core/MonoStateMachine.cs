using System;
using System.Collections.Generic;
using UnityEngine;

namespace WhiteArrow
{
    public abstract class MonoStateMachine<TStateEnum> : MonoBehaviour
        where TStateEnum : Enum
    {
        public StateMachine<TStateEnum> Core { get; private set; }



        protected void InitStateMachine(
            Dictionary<TStateEnum, State> stateMap,
            Dictionary<TStateEnum, List<StateTransition<TStateEnum>>> transitionsMap,
            TStateEnum initialStateId)
        {
            Core = new(stateMap, transitionsMap, initialStateId);
            Core.StateChanged += OnStateChanged;
        }



        private void Update()
        {
            Core?.OnUpdate();
            OnUpdateCore();
        }

        protected virtual void OnUpdateCore() { }



        private void FixedUpdate()
        {
            Core?.OnFixedUpdate();
            OnFixedUpdateCore();
        }

        protected virtual void OnFixedUpdateCore() { }



        protected virtual void OnStateChanged(TStateEnum id) { }



        protected T GetState<T>(TStateEnum id)
            where T : State
        {
            return Core.GetState<T>(id);
        }
    }
}