using System.Collections.Generic;
using Entitas;
using UnityEngine;


public class ChangeDeltaTimeSystem : IExecuteSystem
{
    private Contexts _contexts;

    public ChangeDeltaTimeSystem(Contexts contexts)
    {
        _contexts = contexts;
    }

    public void Execute()
    {
        if (!_contexts.game.isGameOver)
        {
            _contexts.game.ReplaceDeltaTime(Time.deltaTime);
        }
    }
}

/*
public class ChangeDeltaTimeSystem : ReactiveSystem<GameEntity>
{
    private Contexts _contexts;

    public ChangeDeltaTimeSystem(Contexts contexts) : base(contexts.game)
    {
        _contexts = contexts;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        Debug.Log("A");
        _contexts.game.ReplaceDeltaTime(Time.deltaTime);
    }

    protected override bool Filter(GameEntity entity)
    {
        return true;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.StartTime);
    }
}
*/