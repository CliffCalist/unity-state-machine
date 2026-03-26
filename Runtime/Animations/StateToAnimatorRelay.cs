using System;
using System.Collections.Generic;
using UnityEngine;

namespace WhiteArrow
{
    [Serializable]
    public class StateToAnimatorRelay<TState> : IDisposable
        where TState : Enum
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private List<StateTriggerPair<TState>> _animationTriggers;



        private StateMachine<TState> _stateMachine;



        public void Init(StateMachine<TState> stateMachine)
        {
            _stateMachine = stateMachine ?? throw new ArgumentNullException(nameof(stateMachine));
            _stateMachine.StateChanged += OnStateChanged;
        }

        private void OnStateChanged(TState state)
        {
            var pair = _animationTriggers.Find(p => p.State.Equals(state));
            if (pair == null)
                return;

            _animator.SetTrigger(pair.AnimationTrigger);
        }

        public void Dispose()
        {
            _stateMachine.StateChanged -= OnStateChanged;
        }
    }
}