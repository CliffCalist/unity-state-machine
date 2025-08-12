# Unity State Machine

A lightweight and extensible state machine framework for Unity.  
Originally created for production gameplay systems, this framework provides a clear, maintainable way to manage complex behaviors, transitions, and nested state logic.

## Features

- Strongly-typed enum-based states
- Separation of state logic and transitions
- Nested state machines for complex flows
- MonoBehaviour-friendly wrapper for quick integration
- Built-in Animator integration (via `StateToAnimatorRelay`)

## Installing

To install via UPM, use **"Install package from git URL"** and add the following:

```
https://github.com/CliffCalist/unity-state-machine.git
```


## Quick Start

**Note:** `StateMachine<T>` is not a MonoBehaviour.  
To keep it updated, you must manually call:
- `OnUpdate()` from your `MonoBehaviour.Update()` method.
- `OnFixedUpdate()` from your `MonoBehaviour.FixedUpdate()` method.

### Define Your States
Create an enum to represent your states:
```csharp
public enum PlayerState
{
    Idle,
    Run,
    Jump
}
```

### Create State Classes
Inherit from `State` and override only what you need:
 - `EnterCore()` — called when the state becomes active
 - `ExitCore()` — called when the state is deactivated
 - `OnUpdate()` — called every frame (Unity Update)
 - `OnFixedUpdate()` — called in physics loop (Unity FixedUpdate)
```csharp
public class IdleState : State
{
    // Invoked when the state becomes active
    protected override void EnterCore()
    {
        Debug.Log("Entering Idle");
    }

    // Invoked once when the state is deactivated
    protected override void ExitCore()
    {
        Debug.Log("Exiting Idle");
    }

    // Called every frame
    public override void OnUpdate()
    {
        // Idle logic per frame
    }

    // Called in physics loop
    public override void OnFixedUpdate()
    {
        // Physics-related idle logic
    }
}
```

### Set Up the State Machine
Create the state machine and define transitions:
```csharp
var states = new Dictionary<PlayerState, State>
{
    { PlayerState.Idle, new IdleState() },
    { PlayerState.Run, new RunState() }
};

var transitions = new Dictionary<PlayerState, List<StateTransition<PlayerState>>>
{
    { PlayerState.Idle, new List<StateTransition<PlayerState>>
        {
            new StateTransition<PlayerState>(PlayerState.Run, () => Input.GetKey(KeyCode.W))
        }
    }
};

var sm = new StateMachine<PlayerState>(states, transitions, PlayerState.Idle);
```

## Advanced Usage

### Extending StateMachine<TStateEnum>
You can inherit from `StateMachine<TStateEnum>` to customize behavior.  
The base class allows you to override:
- `OnUpdateCore()` — logic executed every frame.
- `OnFixedUpdateCore()` — logic executed in the physics loop.
- `OnStateChanged()` — called whenever the active state changes.

You also have access to:
- `GetState<T>(TStateEnum id)` — retrieve a specific state instance by ID.

### NestedStateMachine<TStateEnum>
This is a `State` that also acts as a facade for a separate `StateMachine<TStateEnum>`.  
It enables building **nested state machines**, where a single state contains its own sub-state logic.

### MonoStateMachine<TStateEnum>
This MonoBehaviour wrapper reduces boilerplate when your state machine lives on a Unity component.  
Initialize it via:
```csharp
InitStateMachine(
    Dictionary<TStateEnum, State> stateMap,
    Dictionary<TStateEnum, List<StateTransition<TStateEnum>>> transitionsMap,
    TStateEnum initialStateId
);
```
Access the core machine through the `Core` property.  
You can override `OnUpdateCore`, `OnFixedUpdateCore`, and `OnStateChanged` just like in `StateMachine<T>`.  
It also provides `GetState<T>(TStateEnum id)` to easily fetch and interact with specific states.

### IdleState
A simple, logic-free state often used as an initial "idle" state.

### StateToAnimatorRelay<TStateID>
This component lets you link a state machine to an Animator by selecting:
- The state enum value.
- The Animator trigger name.

The relay listens for state changes in the linked state machine and calls:
```csharp
_animator.SetTrigger(pair.AnimationTrigger);
```
according to your inspector configuration.

## Roadmap

- [ ] Change inheritance to interfaces where applicable
- [ ] Refactor creation of `transitionsMap` during `StateMachine<T>` initialization
- [ ] Allow dynamically adding and removing states with transitions
- [ ] Ability to disable specific states and transitions without deleting them
