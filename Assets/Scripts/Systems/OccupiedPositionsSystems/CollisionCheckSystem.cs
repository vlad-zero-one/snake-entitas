using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class CollisionCheckSystem : ReactiveSystem<GameEntity>
{
    Contexts _contexts;

    public CollisionCheckSystem(Contexts contexts) : base(contexts.game)
    {
        _contexts = contexts;
    }
    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Moving);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.isMoving;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        var occupiedPositions = _contexts.game.globals.value.OccupiedPositions;
        var headPosition = entities.SingleEntity().position.value;
        if (occupiedPositions.Contains(headPosition))
        {
            Debug.Log("GAME OVER");
        }
    }
}
