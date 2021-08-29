using System.Collections.Generic;
using Entitas;

public class IncrementTickSystem : ReactiveSystem<GameEntity>
{
    private Contexts _contexts;

    public IncrementTickSystem(Contexts contexts) : base(contexts.game)
    {
        _contexts = contexts;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.DeltaTime);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasDeltaTime;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        var entity = entities.SingleEntity();
        var currentTickTime = _contexts.game.hasTickTime ? _contexts.game.tickTime.value : 0;
        currentTickTime += entity.deltaTime.value;

        var speed = _contexts.game.globals.value.Speed;
        if (currentTickTime > 1 / speed)
        {
            _contexts.game.isTick = false;
            _contexts.game.isTick = true;
            currentTickTime = 0;
        }

        _contexts.game.ReplaceTickTime(currentTickTime);

    }
}
