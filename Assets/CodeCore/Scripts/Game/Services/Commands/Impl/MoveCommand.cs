using Assets.CodeCore.Scripts.Game.Services.Entitieys.Model;
using Cysharp.Threading.Tasks;
using System;
using System.Numerics;

namespace Assets.CodeCore.Scripts.Game.Services
{
    public class MoveCommand : ICommand
    {
        private readonly Entity _entity;
        private readonly int _steps;

        public MoveCommand(Entity entity, int steps, Vector2 direction)
        {
            _entity = entity;
            _steps = steps;
        }

        public UniTask Execute()
        {
            throw new NotImplementedException();
        }
    }
}
