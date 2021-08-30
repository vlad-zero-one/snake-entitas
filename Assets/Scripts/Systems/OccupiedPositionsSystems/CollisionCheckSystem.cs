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
        var borderPositions = _contexts.game.globals.value.BorderPositions;
        var barrierPositions = _contexts.game.globals.value.BarrierPositions;
        var headPosition = entities.SingleEntity().position.value;
        var snakePositionsExceptHead = _contexts.game.globals.value.SnakePositionsExceptHead;
        if (borderPositions.Contains(headPosition) || barrierPositions.Contains(headPosition) || snakePositionsExceptHead.Contains(headPosition))
        {
            Debug.Log("GAME OVER");
        }
    }
}
