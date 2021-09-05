﻿using UnityEngine;
using Entitas;
using System.Collections;
using System;

public class GameController : MonoBehaviour
{
    public Globals Globals;

    /*
    public GameObject SnakeGameObject;
    public GameObject BordersGameObject;
    public GameObject BarriersGameObject;
    public GameObject EdibleGameObject;
    */
    private Systems _systems;

    private Contexts _contexts;

    private void Start()
    {
        _contexts = Contexts.sharedInstance;

        _contexts.game.SetGlobals(Globals);

        SetCamera(_contexts);

        if (_systems == null)
        {
            _systems = CreateSystems(_contexts);
        }

        _systems.Initialize();
        //Debug.Log("STARTED");
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
            //.Add(new RestartCleanUpSystem(contexts))
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
        //_systems.TearDown();
        //_systems.ClearReactiveSystems();
        var s = new RestartSystem(_contexts);
        s.Execute();

        var i = new InitializeSnakeSystem(_contexts);
        i.Initialize();

        var e = new InitializeEdibleSystem(_contexts);
        e.Initialize();


        //_systems.Cleanup();
        //_contexts.Reset();
        //var n = new InitializeSnakeSystem(_contexts);
        //n.Initialize();
        //Start();
    }
}
