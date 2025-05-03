using System;
using System.Collections.Generic;

namespace WhiteArrow
{
    public abstract class NestedStateMachine<TStateEnum> : State
        where TStateEnum : Enum
    {
        private StateMachine<TStateEnum> _core;



        protected void Init(
            Dictionary<TStateEnum, State> stateMap,
            Dictionary<TStateEnum, List<StateTransition<TStateEnum>>> transitionsMap,
            TStateEnum initialStateId)
        {
            _core = new(stateMap, transitionsMap, initialStateId);
            _core.StateChanged += OnStateChanged;
        }



        internal protected override void OnUpdate() => _core.OnUpdate();
        internal protected override void OnFixedUpdate() => _core.OnFixedUpdate();



        protected virtual void OnStateChanged(TStateEnum id) { }



        protected T GetState<T>(TStateEnum id)
            where T : State
        {
            return _core.GetState<T>(id);
        }
    }
}