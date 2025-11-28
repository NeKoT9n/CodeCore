using System;
using System.Collections.Generic;

namespace Assets.CodeCore.Scripts.Game.Services.Entitieys.Factory
{
    public class PluginFactoryBase<TKey, TFactoryPlugin>
    {
        private Dictionary<TKey, TFactoryPlugin> _factories = new();

        protected PluginFactoryBase(IEnumerable<IFactoryPlugin<TKey>> plugins)
        {
            foreach (var factory in plugins)
            {
                if (_factories.ContainsKey(factory.Key))
                {
                    throw new InvalidOperationException($"Duplicate plugin for key: {factory.Key}");
                }

                _factories.Add(factory.Key, (TFactoryPlugin)factory);
            }
        }

        protected TFactoryPlugin GetFactory(TKey key)
        {
            if (_factories.TryGetValue(key, out var factory))
            {
                return factory;
            }
            throw new KeyNotFoundException($"factory not found for key: {key}");
        }
    }

    public interface IFactoryPlugin<TKey>
    {
        TKey Key { get; }
    }
}
