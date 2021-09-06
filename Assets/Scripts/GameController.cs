using UnityEngine;
using Entitas;

public class GameController : MonoBehaviour
{
    public Globals Globals;

    private Systems _systems;

    private Contexts _contexts;

    private void Start()
    {
        _contexts = Contexts.sharedInstance;

        _contexts.game.SetGlobals(Globals);
    }

    private void Update()
    {
        if (_systems != null) _systems.Execute();
    }


    public void NewGame()
    {
        if (_systems == null)
        {
            _systems = CreateSystems(_contexts);
        }
        else
        {
            _systems.Cleanup();
        }

        _contexts.game.isLoad = false;

        _systems.Initialize();
        SetCamera(_contexts);

        Debug.Log("NEW GAME");
    }

    public void LoadGame()
    {
        if (_systems == null)
        {
            _systems = CreateSystems(_contexts);
        }
        else
        {
            _systems.Cleanup();
        }

        _contexts.game.isLoad = true;

        _systems.Initialize();
        SetCamera(_contexts);

        Debug.Log("LOAD GAME");
    }

    private Systems CreateSystems(Contexts contexts)
    {
        return new Feature("Game")
            .Add(new LoadOrInitSnakeSystem(contexts))
            .Add(new LoadOrInitOccupiedSystem(contexts))
            .Add(new LoadOrInitEdibleSystem(contexts))


            .Add(new ViewEdibleSystem(contexts))
            .Add(new ViewOccupiedPositionsSystem(contexts))
            .Add(new InputSystem(contexts))
            .Add(new MoveAndGrowSystem(contexts))
            .Add(new ViewSnakeSystem(contexts))
            .Add(new CollisionCheckSystem(contexts))
            .Add(new SnakeEatSystem(contexts))
            .Add(new RepositionSnakeGameObjectSystem(contexts))

            .Add(new StartTimeSystem(contexts))
            .Add(new ChangeDeltaTimeSystem(contexts))
            .Add(new IncrementTickSystem(contexts))

            .Add(new CleanupSystem(contexts))
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

    public void Restart()
    {
        _systems.Cleanup();
        _systems.Initialize();
    }
}
