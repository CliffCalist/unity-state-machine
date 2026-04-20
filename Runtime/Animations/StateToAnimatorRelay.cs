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
        private TStateKey _lastState;



        public StateToAnimatorRelay(Animator animator, List<StateTriggerPair<TStateKey>> animationTriggers)
        {
            _animator = animator ?? throw new ArgumentNullException(nameof(animator));
            _animationTriggers = new(animationTriggers ?? throw new ArgumentNullException(nameof(animationTriggers)));
        }



        public void Init(StateMachine<TStateKey> stateMachine)
        {
            _stateMachine = stateMachine ?? throw new ArgumentNullException(nameof(stateMachine));
            OnStateChanged(stateMachine.CurrentStateKey);
            _stateMachine.StateChanged += OnStateChanged;
        }

        private void OnStateChanged(TStateKey currentState)
        {
            if (_lastState != null)
            {
                var currentPair = _animationTriggers.Find(p => p.State.Equals(_lastState));
                if (currentPair != null)
                    _animator.ResetTrigger(currentPair.AnimationTrigger);
            }

            var nextPair = _animationTriggers.Find(p => p.State.Equals(currentState));
            if (nextPair != null)
                _animator.SetTrigger(nextPair.AnimationTrigger);

            _lastState = currentState;
        }

        public void Dispose()
        {
            _stateMachine.StateChanged -= OnStateChanged;
        }
    }
}