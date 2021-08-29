using UnityEngine;
using Entitas;
using System.Collections;

public class GameController : MonoBehaviour
{
    public Globals Globals;

    private Systems _systems;

    private void Start()
    {
        Contexts contexts = Contexts.sharedInstance;

        contexts.game.SetGlobals(Globals);

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
            .Add(new InitializeEdibleSystem(contexts))
            .Add(new InputSystem(contexts))
            .Add(new MoveSystem(contexts))
            .Add(new SnakeEatSystem(contexts))
            .Add(new SnakeViewSystem(contexts))
            .Add(new ChangeDeltaTimeSystem(contexts))
            .Add(new IncrementTickSystem(contexts))
            ;

    }
}
