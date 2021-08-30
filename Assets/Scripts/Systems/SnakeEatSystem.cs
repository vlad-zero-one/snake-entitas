using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class SnakeEatSystem : ReactiveSystem<GameEntity>
{
    private Contexts _contexts;

    public SnakeEatSystem(Contexts contexts) : base(contexts.game)
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
        var headPosition = _contexts.game.headEntity.position.value;
        var ediblePosition = _contexts.game.edibleEntity.position.value;
        var barrierPositions = _contexts.game.globals.value.BarrierPositions;
        var snakePositionsExceptHead = _contexts.game.globals.value.SnakePositionsExceptHead;
        var borderSize = _contexts.game.globals.value.BorderSize;

        if (headPosition.X == ediblePosition.X
            && headPosition.Y == ediblePosition.Y)
        {
            IntVec2 position = new IntVec2(Random.Range(0, borderSize), Random.Range(0, borderSize));
            while (barrierPositions.Contains(position)
                    || snakePositionsExceptHead.Contains(position)
                    || (position.X == headPosition.X && position.Y == headPosition.Y))
            {
                position = new IntVec2(Random.Range(0, borderSize), Random.Range(0, borderSize));
            }
            _contexts.game.edibleEntity.position.value = new IntVec2(position.X, position.Y);
            _contexts.game.edibleEntity.gameObject.value.transform.position = new Vector2(position.X, position.Y);
            _contexts.game.headEntity.isGrowing = true;
        }
    }
}
