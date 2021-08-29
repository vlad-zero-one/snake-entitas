using System.Collections.Generic;
using Entitas;

public class MoveSystem : ReactiveSystem<GameEntity>
{
    private Contexts _contexts;

    public MoveSystem(Contexts contexts) : base(contexts.game)
    {
        _contexts = contexts;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        //return context.CreateCollector(GameMatcher.AnyOf(GameMatcher.Head, GameMatcher.Tail, GameMatcher.Tick));
        return context.CreateCollector(GameMatcher.Tick);
    }

    protected override bool Filter(GameEntity entity)
    {
        //return entity.isHead || entity.isTail || entity.isTick;
        return entity.isTick;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        //GameEntity head = entities[0];
        //GameEntity tail = entities[1];
        //bool canMove = false;
        //foreach (var entity in entities)
        //{
        //    if (entity.isHead)
        //    {
        //        head = entity;
        //    }
        //    else if (entity.isTail)
        //    {
        //        tail = entity;
        //    }
        //    else if (entity.isTick)
        //    {
        //        canMove = true;
        //    }
        //}

        bool canMove = entities.SingleEntity().isTick ? true : false;
        if (canMove)
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
    }
}
