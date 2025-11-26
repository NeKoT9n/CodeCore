using Assets.CodeCore.Scripts.Game.Services.Scripts.Data;
using UnityEngine;

namespace Assets.CodeCore.Scripts.Game.Helpers
{
    public class SpawnPointMarker : MonoBehaviour
    {
        [SerializeField] private EntityTypeId _entityType;
        public EntityTypeId EntityType => _entityType;
        public Vector2 Position => transform.position;

        private void OnDrawGizmos()
        {
            Color color = Color.grey;

            switch (_entityType)
            {
                case EntityTypeId.Player:
                    color = Color.green;
                    break;

                case EntityTypeId.Enemy:
                    color = Color.red;
                    break;
            }

            Gizmos.color = color;
            Gizmos.DrawSphere(transform.position, 0.2f);
        }
    }
}
