using Entitas;
using UnityEngine;

public class InitializeSnakeSystem : IInitializeSystem
{
    Contexts _contexts;

    public InitializeSnakeSystem(Contexts contexts)
    {
        _contexts = contexts;
    }

    public void Initialize()
    {
        var cellPrefab = _contexts.game.globals.value.SnakeCell;
        var snakePositionsExceptHead = _contexts.game.globals.value.SnakePositionsExceptHead;
        snakePositionsExceptHead.Clear();
        int center = _contexts.game.globals.value.BorderSize / 2;

        var head = _contexts.game.CreateEntity();
        head.AddPosition(new IntVec2(center, center + 2));
        head.isSnake = true;
        var go = GameObject.Instantiate(cellPrefab, new Vector2(center, center + 2), Quaternion.identity);
        head.AddGameObject(go);
        head.isHead = true;

        var part = _contexts.game.CreateEntity();
        part.AddPosition(new IntVec2(center, center + 1));
        snakePositionsExceptHead.Add(new IntVec2(center, center + 1));
        part.isSnake = true;
        go = GameObject.Instantiate(cellPrefab, new Vector2(center, center + 1), Quaternion.identity);
        part.AddGameObject(go);
        part.AddPreviousSegment(head);

        var tail = _contexts.game.CreateEntity();
        tail.AddPosition(new IntVec2(center, center));
        snakePositionsExceptHead.Add(new IntVec2(center, center));
        tail.isSnake = true;
        go = GameObject.Instantiate(cellPrefab, new Vector2(center, center), Quaternion.identity);
        tail.AddGameObject(go);
        tail.isTail = true;
        tail.AddPreviousSegment(part);

        _contexts.game.ReplaceDirection(DirectionEnum.N);
        _contexts.game.ReplaceLastMovementDirection(DirectionEnum.N);
    }
}
