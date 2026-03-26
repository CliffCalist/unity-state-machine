using System;

namespace WhiteArrow
{
    public class StateTransition<TStateEnum>
        where TStateEnum : Enum
    {
        public TStateEnum To { get; }
        public Func<bool> Condition { get; }



        public StateTransition(TStateEnum to, Func<bool> condition)
        {
            To = to;
            Condition = condition ?? throw new ArgumentNullException(nameof(condition));
        }
    }
}