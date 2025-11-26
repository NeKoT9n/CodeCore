using Cysharp.Threading.Tasks;
using System.Collections.Generic;

namespace Assets.CodeCore.Scripts.Commands
{
    public class CommandService : ICommandService
    {
        private readonly Stack<ICommand> _history = new();

        public async UniTask Execute(ICommand command)
        {
            await command?.Execute();
            _history.Push(command);
        }

        public async UniTask Execute(IReadOnlyCollection<ICommand> commands, float durationBetweenCommands)
        {
            foreach (var command in commands)
            {
                await Execute(command);
                await UniTask.WaitForSeconds(durationBetweenCommands);  
            }
        }
        public void ClearHistory()
        {
            _history.Clear();
        }
    }
}
