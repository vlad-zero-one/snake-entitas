using System.Collections.Generic;
using Entitas;
using Entitas.Unity;
using UnityEngine;

public class MoveAndGrowSystem : ReactiveSystem<GameEntity>
{
    private Contexts _contexts;

    private GameObject _snake;

    public MoveAndGrowSystem(Contexts contexts) : base(contexts.game)
    {
        _contexts = contexts;
        _snake = GameObject.Find("Snake");
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
        tail.RemovePreviousSegment();
        _contexts.game.globals.value.SnakePositionsExceptHead.Remove(tail.position.value);
        tail.position.value = head.position.value;

        CalculatePosition(tail.position);
        _contexts.game.globals.value.SnakePositionsExceptHead.Add(head.position.value);

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

        _contexts.game.lastMovementDirection.value = _contexts.game.direction.value;

        currentHead.isGrowing = false;
        currentHead.ReplacePreviousSegment(newHead);
        _contexts.game.globals.value.SnakePositionsExceptHead.Add(currentHead.position.value);
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
