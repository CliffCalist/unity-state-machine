# Unity State Machine

A modular and extensible state machine framework for Unity.  
Originally designed for gameplay systems in production projects, this state machine provides a clear and maintainable way to manage complex behaviors and transitions.

## ✨ Features

- Strongly-typed enum-based states
- Separation of state logic and transitions
- Nested state machines
- MonoBehaviour-friendly wrapper
- Built-in Animator integration (via StateToAnimatorRelay)
- Reactive transition system with `Func<bool>`

## 🧩 Components

- `StateMachine<TEnum>` — core logic
- `State` — abstract class with Enter/Exit/Update hooks
- `StateTransition<TEnum>` — encapsulates transition conditions
- `MonoStateMachine<TEnum>` — wrapper for MonoBehaviours
- `NestedStateMachine<TEnum>` — compose machines inside states
- `StateToAnimatorRelay<TEnum>` — sync state changes to Animator triggers

## 🚀 Usage

1. Inherit from `State` and implement logic.
2. Define enum for your states.
3. Create state map and transition map.
4. Initialize `StateMachine<T>` or use `MonoStateMachine<T>`.
