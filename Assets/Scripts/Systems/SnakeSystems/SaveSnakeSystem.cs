using Entitas;
using UnityEngine;

public class SaveSnakeSystem : IExecuteSystem
{
    private Contexts _contexts;

    public SaveSnakeSystem(Contexts contexts)
    {
        _contexts = contexts;
    }

    public void Execute()
    {
        PlayerPrefs.SetInt("Direction", (int)_contexts.game.direction.value);
        PlayerPrefs.SetInt("LastMovementDirection", (int)_contexts.game.lastMovementDirection.value);

        var snake = _contexts.game.globals.value.SnakePositionsExceptHead;
        PlayerPrefs.SetInt("SnakeSize", snake.Count + 1);
        for (int i = 0; i < snake.Count; i++)
        {
            PlayerPrefs.SetInt("Snake" + i + "X", snake[i].X);
            PlayerPrefs.SetInt("Snake" + i + "Y", snake[i].Y);
        }
        PlayerPrefs.SetInt("Snake" + snake.Count + "X", _contexts.game.headEntity.position.value.X);
        PlayerPrefs.SetInt("Snake" + snake.Count + "Y", _contexts.game.headEntity.position.value.Y);

        int isGrowing = _contexts.game.headEntity.isGrowing == true ? 1 : 0;
        PlayerPrefs.SetInt("IsGrowing", isGrowing);

        PlayerPrefs.SetFloat("Speed", _contexts.game.globals.value.Speed);
    }
}
