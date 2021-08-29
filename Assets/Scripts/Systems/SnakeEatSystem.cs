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
        var headEntity = _contexts.game.headEntity;
        var edibleEntity = _contexts.game.edibleEntity;

        if (headEntity.position.value.X == edibleEntity.position.value.X
            && headEntity.position.value.Y == edibleEntity.position.value.Y)
        {
            edibleEntity.position.value.X += Random.Range(-5, 5);
            edibleEntity.position.value.Y += Random.Range(-5, 5);
            edibleEntity.gameObject.value.transform.position = new Vector2(edibleEntity.position.value.X, edibleEntity.position.value.Y);
            headEntity.isGrowing = true;
        }
    }
}
