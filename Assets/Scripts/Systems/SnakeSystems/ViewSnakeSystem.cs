using System.Collections.Generic;
using Entitas;
using Entitas.Unity;
using UnityEngine;

public class ViewSnakeSystem : ReactiveSystem<GameEntity>
{
    private Contexts _contexts;

    private GameObject _snake, _cellPrefab;

    public ViewSnakeSystem(Contexts contexts) : base(contexts.game)
    {
        _contexts = contexts;
        _snake = GameObject.Find("Snake");
        _cellPrefab = _contexts.game.globals.value.SnakeCell;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Snake);
    }

    protected override bool Filter(GameEntity entity)
    {
        return !entity.hasGameObject;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach(var entity in entities)
        {
            var go = SnakeSegment.Instantiate(_cellPrefab, entity.position.value.X, entity.position.value.Y, _snake);
            go.Link(entity);
            entity.AddGameObject(go);
        }
    }
}
