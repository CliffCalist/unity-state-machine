using System;

namespace WhiteArrow
{
    public class StateTransition<TStateEnum>
        where TStateEnum : Enum
    {
        public TStateEnum To { get; }
        public Func<bool> CanChangeToThis { get; }



        public StateTransition(TStateEnum to, Func<bool> canChangeToThis)
        {
            To = to;
            CanChangeToThis = canChangeToThis ?? throw new ArgumentNullException(nameof(canChangeToThis));
        }
    }
}