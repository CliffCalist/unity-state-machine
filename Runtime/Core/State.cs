namespace WhiteArrow
{
    public abstract class State
    {
        public bool IsEnabled { get; private set; }



        internal void Enter()
        {
            IsEnabled = true;
            EnterCore();
        }

        protected virtual void EnterCore() { }



        internal void Exit()
        {
            IsEnabled = false;
            ExitCore();
        }

        protected virtual void ExitCore() { }



        internal protected virtual void OnUpdate() { }
        internal protected virtual void OnFixedUpdate() { }
    }
}