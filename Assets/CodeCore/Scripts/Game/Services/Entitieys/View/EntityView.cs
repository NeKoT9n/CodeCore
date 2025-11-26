using Assets.CodeCore.Scripts.Game.Services.Scripts.Data;
using UnityEngine;

namespace Assets.CodeCore.Scripts.Game.Services.Scripts.View
{
    public class EntityView : WorldView
    {
        [SerializeField] private EntityTypeId _entityTypeId;
        [SerializeField] private ScriptView _scriptView;
        
        public ScriptView ScriptView => _scriptView;

    }
}
