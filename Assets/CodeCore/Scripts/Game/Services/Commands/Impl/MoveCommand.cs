using Assets.CodeCore.Scripts.Game.Services.Entitieys.Impl;
using Cysharp.Threading.Tasks;
using UnityEngine;


namespace Assets.CodeCore.Scripts.Game.Services
{
    public class MoveCommand : ICommand
    {
        private readonly IMovable _entity;
        private readonly int _steps;
        private readonly Vector2 _direction;

        public MoveCommand(IMovable entity, int steps, Vector2 direction)
        {
            _entity = entity;
            _steps = steps;
            _direction = direction;
        }

        public UniTask Execute()
        {
            _entity.Move(_steps, _direction);

            return UniTask.CompletedTask;
        }
    }
}
