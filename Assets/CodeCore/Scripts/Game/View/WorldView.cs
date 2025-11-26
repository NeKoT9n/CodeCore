using UnityEngine;

namespace Assets.CodeCore.Scripts.Game.Services
{
    public class WorldView : MonoBehaviour, IWorldView
    {
        public GameObject GameObject => gameObject;
    }
}
