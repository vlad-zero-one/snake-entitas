using System.Collections.Generic;
using Entitas;
using Entitas.Unity;
using UnityEngine;

public class ViewOccupiedPositionsSystem : ReactiveSystem<GameEntity>
{
    private Contexts _contexts;

    private GameObject _blockPrefab, _borders, _barriers;

    public ViewOccupiedPositionsSystem(Contexts contexts) : base(contexts.game)
    {
        _contexts = contexts;
        _blockPrefab = _contexts.game.globals.value.BlockCell;
        _borders = GameObject.Find("Borders");
        _barriers = GameObject.Find("Barriers");
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.AnyOf(GameMatcher.Border, GameMatcher.Barrier));
    }

    protected override bool Filter(GameEntity entity)
    {
        return !entity.hasGameObject;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach(var entity in entities)
        {
            var go = GameObject.Instantiate(_blockPrefab,
                new Vector2(entity.position.value.X, entity.position.value.Y),
                Quaternion.identity);
            entity.AddGameObject(go);
            go.Link(entity);

            if (entity.isBorder)
            {
                go.transform.SetParent(_borders.transform);
            }
            else
            {
                go.transform.SetParent(_barriers.transform);
            }
        }
    }
}