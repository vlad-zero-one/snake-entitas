﻿using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class SnakeViewSystem : ReactiveSystem<GameEntity>
{
    Contexts _contexts;

    public SnakeViewSystem(Contexts contexts) : base(contexts.game)
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
        foreach (var entity in entities)
        {
            entity.gameObject.value.transform.position = new Vector2(entity.position.value.X, entity.position.value.Y);
            //Debug.Log(entity.gameObject.value.transform.position.ToString());
            entity.isMoving = false;
        }
    }
}
