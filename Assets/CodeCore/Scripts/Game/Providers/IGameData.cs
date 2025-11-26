using UnityEngine;

namespace Assets.CodeCore.Scripts.Game.Providers
{
    public interface IGameData { }

    public class ScriptableObjectGameData : ScriptableObject, IGameData { }
}
