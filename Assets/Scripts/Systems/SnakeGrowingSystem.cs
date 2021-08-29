using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class SnakeGrowingSystem : ReactiveSystem<GameEntity>
{
    private Contexts _contexts;

    public SnakeGrowingSystem(Contexts contexts) : base(contexts.game)
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
        /*
        if (_contexts.game.headEntity.isGrowing)
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
            newHead.isGrowing = true;
            currentHead.ReplacePreviousSegment(newHead);
        }
        */
    }
}
