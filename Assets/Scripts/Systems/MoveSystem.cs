﻿using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class MoveSystem : ReactiveSystem<GameEntity>
{
    private Contexts _contexts;

    public MoveSystem(Contexts contexts) : base(contexts.game)
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
        if (!_contexts.game.headEntity.isGrowing)
        {
            GameEntity head = _contexts.game.headEntity;
            GameEntity tail = _contexts.game.tailEntity;
            var direction = _contexts.game.direction;

            tail.isTail = false;
            tail.previousSegment.value.isTail = true;
            head.ReplacePreviousSegment(tail);
            head.isHead = false;
            tail.isHead = true;
            tail.position.value = head.position.value;

            if (direction.value == DirectionEnum.N)
                tail.position.value.Y++;
            else if (direction.value == DirectionEnum.E)
                tail.position.value.X++;
            else if (direction.value == DirectionEnum.S)
                tail.position.value.Y--;
            else if (direction.value == DirectionEnum.W)
                tail.position.value.X--;
            tail.isMoving = true;

            _contexts.game.lastMovementDirection.value = _contexts.game.direction.value;
        }
        else
        {
            var currentHead = _contexts.game.headEntity;
            var cellPrefab = _contexts.game.globals.value.SnakeCell;

            var newHead = _contexts.game.CreateEntity();
            newHead.AddPosition(new IntVec2(currentHead.position.value.X, currentHead.position.value.Y));
            newHead.isSnake = true;

            currentHead.isHead = false;
            newHead.isHead = true;

            var direction = _contexts.game.direction;

            if (direction.value == DirectionEnum.N)
                newHead.position.value.Y++;
            else if (direction.value == DirectionEnum.E)
                newHead.position.value.X++;
            else if (direction.value == DirectionEnum.S)
                newHead.position.value.Y--;
            else if (direction.value == DirectionEnum.W)
                newHead.position.value.X--;

            var go = GameObject.Instantiate(cellPrefab, new Vector2(newHead.position.value.X, newHead.position.value.Y), Quaternion.identity);
            newHead.AddGameObject(go);

            _contexts.game.lastMovementDirection.value = _contexts.game.direction.value;

            //сделать ещё одну систему, сбрасывающую этот флаг, поставить после вырастания на след. круге
            //_contexts.game.headEntity.isGrowing = false;
            currentHead.isGrowing = false;
            currentHead.ReplacePreviousSegment(newHead);
        }
    }
}
