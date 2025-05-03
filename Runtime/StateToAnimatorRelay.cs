using System;
using System.Collections.Generic;
using UnityEngine;

namespace WhiteArrow
{
    [Serializable]
    public class StateToAnimatorRelay<TStateID> : IDisposable
        where TStateID : Enum
    {

        [Serializable]
        public class StateTriggerPair
        {
            [SerializeField] private TStateID _state;
            [SerializeField] private string _animationTrigger;


            public TStateID State => _state;
            public string AnimationTrigger => _animationTrigger;
        }



        [SerializeField] private Animator _animator;
        [SerializeField] private List<StateTriggerPair> _animationTriggers;



        private StateMachine<TStateID> _stateMachine;



        public void Init(StateMachine<TStateID> stateMachine)
        {
            _stateMachine = stateMachine ?? throw new ArgumentNullException(nameof(stateMachine));
            _stateMachine.StateChanged += OnStateChanged;
        }

        public void Dispose()
        {
            _stateMachine.StateChanged -= OnStateChanged;
        }



        private void OnStateChanged(TStateID state)
        {
            var pair = _animationTriggers.Find(p => p.State.Equals(state));
            if (pair == null)
                return;

            _animator.SetTrigger(pair.AnimationTrigger);
        }
    }
}