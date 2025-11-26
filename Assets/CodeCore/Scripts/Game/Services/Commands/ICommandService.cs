using Cysharp.Threading.Tasks;
using System.Collections.Generic;

namespace Assets.CodeCore.Scripts.Commands
{
    public interface ICommandService
    {
        public UniTask Execute(ICommand command);
        public UniTask Execute(IReadOnlyCollection<ICommand> commands, float durationBetweenCommands);
        public void ClearHistory();
    }
}
