using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class MoveAndGrowSystem : ReactiveSystem<GameEntity>
{
    private Contexts _contexts;

    public MoveAndGrowSystem(Contexts contexts) : base(contexts.game)
    {
        _contexts = contexts;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Tick);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.isTick;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        if (_contexts.game.headEntity.isGrowing)
        {
            Grow();
        }
        else
        {
            Move();
        }
    }

    private void Move()
    {
        GameEntity head = _contexts.game.headEntity;
        GameEntity tail = _contexts.game.tailEntity;

        tail.isTail = false;
        tail.previousSegment.value.isTail = true;
        head.ReplacePreviousSegment(tail);
        head.isHead = false;
        tail.isHead = true;
        tail.position.value = head.position.value;

        CalculatePosition(tail.position);

        tail.isMoving = true;

        _contexts.game.lastMovementDirection.value = _contexts.game.direction.value;
    }

    private void Grow()
    {
        var currentHead = _contexts.game.headEntity;
        var cellPrefab = _contexts.game.globals.value.SnakeCell;

        var newHead = _contexts.game.CreateEntity();
        newHead.AddPosition(new IntVec2(currentHead.position.value.X, currentHead.position.value.Y));
        newHead.isSnake = true;

        currentHead.isHead = false;
        newHead.isHead = true;

        CalculatePosition(newHead.position);

        var go = GameObject.Instantiate(cellPrefab, new Vector2(newHead.position.value.X, newHead.position.value.Y), Quaternion.identity);
        newHead.AddGameObject(go);

        _contexts.game.lastMovementDirection.value = _contexts.game.direction.value;

        currentHead.isGrowing = false;
        currentHead.ReplacePreviousSegment(newHead);
    }

    private void CalculatePosition(PositionComponent position)
    {
        var direction = _contexts.game.direction;
        if (direction.value == DirectionEnum.N)
            position.value.Y++;
        else if (direction.value == DirectionEnum.E)
            position.value.X++;
        else if (direction.value == DirectionEnum.S)
            position.value.Y--;
        else if (direction.value == DirectionEnum.W)
            position.value.X--;
    }
}
