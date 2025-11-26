using Cysharp.Threading.Tasks;
namespace Assets.CodeCore.Scripts.Game.Infostracture
{
    public interface IAsyncInitializable
    {
        public UniTask Initialize();
    }
}
