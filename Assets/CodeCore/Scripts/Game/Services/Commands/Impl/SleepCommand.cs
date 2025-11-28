using Cysharp.Threading.Tasks;

namespace Assets.CodeCore.Scripts.Game.Services.Commands.Impl
{
    public class SleepCommand : ICommand
    {
        private readonly int _steps;

        public SleepCommand(int steps)
        {
            _steps = steps;
        }
        public UniTask Execute()
        {
            return UniTask.WaitForSeconds(_steps);
        }
    }
}
