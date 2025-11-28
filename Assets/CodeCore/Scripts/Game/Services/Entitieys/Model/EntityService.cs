using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;

namespace Assets.CodeCore.Scripts.Game.Services.Entitieys.Model
{
    public class EntityService
    {
        private readonly ReactiveCollection<Entity> _entities = new();

        public IReadOnlyReactiveCollection<Entity> Entities => _entities;

        public void Add(IEnumerable<Entity> entities)
        {
            foreach(var entity in entities)
            {
                Add(entity);
            }
        }

        public void Add(Entity entity)
        {
            _entities.Add(entity);
        }
    
    }
}
