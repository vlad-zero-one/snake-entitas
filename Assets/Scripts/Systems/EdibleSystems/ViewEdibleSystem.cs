using System.Collections.Generic;
using Entitas;
using Entitas.Unity;
using UnityEngine;

public class ViewEdibleSystem : ReactiveSystem<GameEntity>
{
    private Contexts _contexts;

    private GameObject _edible, _ediblePrefab;

    public ViewEdibleSystem(Contexts contexts) : base(contexts.game)
    {
        _contexts = contexts;
        _edible = GameObject.Find("Edible");
        _ediblePrefab = _contexts.game.globals.value.EdibleCell;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Edible);
    }

    protected override bool Filter(GameEntity entity)
    {
        // if entity doesn't have gameObject
        return !entity.hasGameObject;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        // instantiate
        foreach (var entity in entities)
        {
            var go = GameObject.Instantiate(_ediblePrefab,
                new Vector2(entity.position.value.X, entity.position.value.Y),
                Quaternion.identity,
                _edible.transform);
            go.Link(entity);
            entity.AddGameObject(go);
        }
    }
}