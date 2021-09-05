using Entitas;
using UnityEngine;

public class InitializeEdibleSystem : IInitializeSystem
{
    private Contexts _contexts;

    public InitializeEdibleSystem(Contexts contexts)
    {
        _contexts = contexts;
    }

    public void Initialize()
    {
        int borderSize = _contexts.game.globals.value.BorderSize;
        var barrierPositions = _contexts.game.globals.value.BarrierPositions;
        var snakePositionsExceptHead = _contexts.game.globals.value.SnakePositionsExceptHead;
        var headPosition = _contexts.game.headEntity.position.value;

        IntVec2 position = new IntVec2(Random.Range(0, borderSize), Random.Range(0, borderSize));
        while (barrierPositions.Contains(position)
            || snakePositionsExceptHead.Contains(position)
            || (position.X == headPosition.X && position.Y == headPosition.Y))
        {
            position = new IntVec2(Random.Range(0, borderSize), Random.Range(0, borderSize));
        }

        var ediblePrefab = _contexts.game.globals.value.EdibleCell;
        var edibleEntity = _contexts.game.CreateEntity();
        edibleEntity.isEdible = true;
        edibleEntity.AddPosition(position);
    }
}
