using Entitas;
using Entitas.Unity;
using UnityEngine;

public class LoadSnakeSystem : IInitializeSystem
{
    private Contexts _contexts;

    GameObject cellPrefab, _snakeParent;

    public LoadSnakeSystem(Contexts contexts)
    {
        _contexts = contexts;
        cellPrefab = _contexts.game.globals.value.SnakeCell;
        _snakeParent = GameObject.Find("Snake");
    }

    public void Initialize()
    {
        _contexts.game.ReplaceDirection((DirectionEnum)PlayerPrefs.GetInt("Direction"));
        _contexts.game.ReplaceLastMovementDirection((DirectionEnum)PlayerPrefs.GetInt("LastMovementDirection"));

        _contexts.game.globals.value.SnakePositionsExceptHead.Clear();
        var snakePositionsExceptHead = _contexts.game.globals.value.SnakePositionsExceptHead;
 
        IntVec2 position = new IntVec2(PlayerPrefs.GetInt("Snake0X"), PlayerPrefs.GetInt("Snake0Y"));
        GameEntity head, tail;

        tail = _contexts.game.CreateEntity();
        tail.isTail = true;
        tail.isSnake = true;
        tail.ReplacePosition(position);
        snakePositionsExceptHead.Add(position);
        var go = SnakeSegment.Instantiate(cellPrefab, position.X, position.Y, _snakeParent);
        go.Link(tail);
        tail.ReplaceGameObject(go);
        _contexts.game.tailEntity.ReplacePosition(position);

        GameEntity entityForQueue = tail;
        for (int i = 1; i < PlayerPrefs.GetInt("SnakeSize") - 1; i++)
        {
            position = new IntVec2(PlayerPrefs.GetInt("Snake" + i + "X"), PlayerPrefs.GetInt("Snake" + i + "Y"));
            var segment = _contexts.game.CreateEntity();
            segment.isSnake = true;
            segment.ReplacePosition(position);
            snakePositionsExceptHead.Add(position);
            go = SnakeSegment.Instantiate(cellPrefab, position.X, position.Y, _snakeParent);
            go.Link(segment);
            segment.ReplaceGameObject(go);

            entityForQueue.ReplacePreviousSegment(segment);
            entityForQueue = segment;
        }

        int lastIndex = PlayerPrefs.GetInt("SnakeSize") - 1;
        position = new IntVec2(PlayerPrefs.GetInt("Snake" + lastIndex + "X"), PlayerPrefs.GetInt("Snake" + lastIndex + "Y"));
        head = _contexts.game.CreateEntity();
        head.isHead = true;
        head.isSnake = true;
        head.ReplacePosition(position);
        go = SnakeSegment.Instantiate(cellPrefab, position.X, position.Y, _snakeParent);
        go.Link(head);
        head.ReplaceGameObject(go);
        _contexts.game.headEntity.ReplacePosition(position);
        entityForQueue.ReplacePreviousSegment(head);

        int isGrowing = PlayerPrefs.GetInt("IsGrowing");
        _contexts.game.headEntity.isGrowing = isGrowing == 1 ? true : false;
    }
}
