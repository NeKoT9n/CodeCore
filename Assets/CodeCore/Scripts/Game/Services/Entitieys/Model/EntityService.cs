using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;

namespace Assets.CodeCore.Scripts.Game.Services.Entitieys.Model
{
    public class EntityService
    {
        private readonly ReactiveCollection<Player> _entities = new();

        public IReadOnlyReactiveCollection<Player> Entities => _entities;

        public void Add(IEnumerable<Player> entities)
        {
            foreach(var entity in entities)
            {
                Add(entity);
            }
        }

        public void Add(Player entity)
        {
            _entities.Add(entity);
        }
    
    }
}
