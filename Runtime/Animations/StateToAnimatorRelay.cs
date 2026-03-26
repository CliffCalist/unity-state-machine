using System;
using System.Collections.Generic;
using UnityEngine;

namespace WhiteArrow
{
    [Serializable]
    public class StateToAnimatorRelay<TStateKey> : IDisposable
        where TStateKey : Enum
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private List<StateTriggerPair<TStateKey>> _animationTriggers;



        private StateMachine<TStateKey> _stateMachine;



        public void Init(StateMachine<TStateKey> stateMachine)
        {
            _stateMachine = stateMachine ?? throw new ArgumentNullException(nameof(stateMachine));
            _stateMachine.StateChanged += OnStateChanged;
        }

        private void OnStateChanged(TStateKey state)
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