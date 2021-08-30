using UnityEngine;
using Entitas;
using System.Collections;
using System;

public class GameController : MonoBehaviour
{
    public Globals Globals;

    private Systems _systems;

    private void Start()
    {
        Contexts contexts = Contexts.sharedInstance;

        contexts.game.SetGlobals(Globals);

        SetCamera(contexts);

        _systems = CreateSystems(contexts);

        _systems.Initialize();
    }

    private void Update()
    {
        _systems.Execute();
    }

    private Systems CreateSystems(Contexts contexts)
    {
        return new Feature("Game")
            .Add(new InitializeSnakeSystem(contexts))
            .Add(new InitializeOccupiedPossitions(contexts))
            .Add(new InitializeEdibleSystem(contexts))
            .Add(new InputSystem(contexts))
            .Add(new MoveAndGrowSystem(contexts))
            .Add(new CollisionCheckSystem(contexts))
            .Add(new SnakeEatSystem(contexts))
            .Add(new SnakeViewSystem(contexts))
            .Add(new ChangeDeltaTimeSystem(contexts))
            .Add(new IncrementTickSystem(contexts))
            ;

    }

    private void SetCamera(Contexts contexts)
    {
        Camera.main.transform.position = new Vector3(contexts.game.globals.value.BorderSize / 2 - 0.5f, contexts.game.globals.value.BorderSize / 2 - 0.5f, -10);
        Camera.main.orthographicSize = contexts.game.globals.value.BorderSize / 2 + 3;
    }

    private void OnDisable()
    {
        Globals.BorderPositions.Clear();
        Globals.SnakePositionsExceptHead.Clear();
    }
}
