namespace WhiteArrow
{
    public abstract class State
    {
        protected bool _isEnabled { get; private set; }



        internal void Enter()
        {
            _isEnabled = true;
            EnterCore();
        }

        protected virtual void EnterCore() { }



        internal void Exit()
        {
            _isEnabled = false;
            ExitCore();
        }

        protected virtual void ExitCore() { }



        internal protected virtual void OnUpdate() { }
        internal protected virtual void OnFixedUpdate() { }
    }
}