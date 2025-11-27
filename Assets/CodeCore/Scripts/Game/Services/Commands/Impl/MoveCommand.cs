using Assets.CodeCore.Scripts.Game.Services.Entitieys.Impl;
using Cysharp.Threading.Tasks;
using System.Numerics;

namespace Assets.CodeCore.Scripts.Game.Services
{
    public class MoveCommand : ICommand
    {
        private readonly IMovable _entity;
        private readonly int _steps;
        private readonly bool _leftDirection;

        public MoveCommand(IMovable entity, int steps, Vector2 direction)
        {
            _entity = entity;
            _steps = steps;
            _leftDirection = direction.X < 0;
        }

        public UniTask Execute()
        {
            if (_leftDirection)
                _entity.MoveLeft(_steps);
            else
                _entity.MoveRight(_steps);

            return UniTask.CompletedTask;
        }
    }
}
