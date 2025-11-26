using Assets.CodeCore.Scripts.Game.Infostracture.Factories;
using Assets.CodeCore.Scripts.Game.Infostracture.StateMachine;
using Assets.CodeCore.Scripts.Game.Startup.GameStates;
using Assets.CodeCore.Scripts.Game.Startup.GameStates.States;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets.CodeCore.Scripts.Game.Startup
{
    public class GameEntryPoint : MonoBehaviour
    {
        [Inject] readonly GameStateMachine _stateMachine;
        [Inject] readonly GameStateFactory _gameStateFactory;

        public void Awake()
        {
            _stateMachine.Initialize(new List<IState>()
            {
                _gameStateFactory.CreateState<InitializeState>(),
                _gameStateFactory.CreateState<LoadLevelState>(),
                _gameStateFactory.CreateState<PlayState>(),
                _gameStateFactory.CreateState<PauseState>()
            });

            _stateMachine.TrySwitchState<InitializeState>();
        }

        public void Update()
        {
            _stateMachine.Update();
        }

        public void FixedUpdate()
        {
            _stateMachine.FixedUpdate();
        }
    }
}
