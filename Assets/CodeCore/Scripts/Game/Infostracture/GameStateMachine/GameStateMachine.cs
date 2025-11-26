using Assets.CodeCore.Scripts.Game.Infostracture.StateMachine.States.Interfaces;
using Assets.CodeCore.Scripts.Game.Startup.GameStates;
using System;
using System.Collections.Generic;


namespace Assets.CodeCore.Scripts.Game.Infostracture.StateMachine
{
    public class GameStateMachine : IStateSwitcher
    {
        private Dictionary<Type, IState> _states;
        private IState _currentState;

        private IUpdatableState _cashedUpdatable = null;
        private IFixedUpdatableState _cashedFixedUpdatable = null;

        public void Initialize(IEnumerable<IState> states)
        {
            _states = new();

            foreach(var state in states)
                _states.Add(state.GetType(), state);
        }

        public bool TrySwitchState<TState>() where TState : IState
        {
            IState state = GetState<TState>();

            if (_currentState == state)
                return false;

            if(TryGetExitable(_currentState, out var exitableState))
                exitableState.Exit();

            _currentState = state;

            TryGetUpdatable(_currentState, out _cashedUpdatable);
            TryGetFixedUpdatable(_currentState, out _cashedFixedUpdatable);

            if (TryGetEnterable(_currentState, out var enterableState))
                enterableState.Enter();

            return true;
        }

        public void Update()
        {
            _cashedUpdatable?.Update();
        }

        public void FixedUpdate()
        {
            _cashedFixedUpdatable?.FixedUpdate();
        }

        private IState GetState<TState>() where TState : IState
        {
            var stateType = typeof(TState);

            if (_states.ContainsKey(stateType) == false)
                throw new ArgumentException($"State of type {stateType.Name} not registered in FSM!");

            IState state = _states[typeof(TState)];
            return state;
        }

        private bool TryGetEnterable(IState state, out IEnterableState enterableState)
        {
            return TryGetInterface<IEnterableState>(state, out enterableState);
        }

        private bool TryGetExitable(IState state, out IExitableState exitableState)
        {
            return TryGetInterface<IExitableState>(state, out exitableState);
        }

        private bool TryGetUpdatable(IState state, out IUpdatableState updatableState)
        {
            return TryGetInterface<IUpdatableState>(state, out updatableState);
        }
        
        private bool TryGetFixedUpdatable(IState state, out IFixedUpdatableState updatableState)
        {
            return TryGetInterface<IFixedUpdatableState>(state, out updatableState);
        }

        private bool TryGetInterface<TInterface>(IState state, out TInterface result)
            where TInterface : class, IState
        {
            if (state is TInterface requiredInterface)
            {
                result = requiredInterface;
                return true;
            }

            result = null;
            return false;
        }

    }
}