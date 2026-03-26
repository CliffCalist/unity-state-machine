using System;
using UnityEngine;

namespace WhiteArrow
{
    [Serializable]
    public class StateTriggerPair<TState>
        where TState : Enum
    {
        [SerializeField] private TState _state;
        [SerializeField] private string _animationTrigger;



        public TState State => _state;
        public string AnimationTrigger => _animationTrigger;
    }
}