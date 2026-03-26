using System;
using UnityEngine;

namespace WhiteArrow
{
    [Serializable]
    public class StateTriggerPair<TStateKey>
        where TStateKey : Enum
    {
        [SerializeField] private TStateKey _state;
        [SerializeField] private string _animationTrigger;



        public TStateKey State => _state;
        public string AnimationTrigger => _animationTrigger;
    }
}