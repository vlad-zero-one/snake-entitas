using Entitas;
using System;
using System.Collections.Generic;
using UnityEngine;

public class InitializeOccupiedPossitions : IInitializeSystem
{
    private Contexts _contexts;

    private GameObject _blockPrefab;

    public InitializeOccupiedPossitions(Contexts contexts)
    {
        _contexts = contexts;
    }

    public void Initialize()
    {
        _blockPrefab = _contexts.game.globals.value.BlockCell;

        if (_contexts.game.globals.value.BorderPositions != null)
            _contexts.game.globals.value.BorderPositions.Clear();

        int borderSize = _contexts.game.globals.value.BorderSize;
        for (int i = 0; i < borderSize + 1; i++)
        {
            CreateBorderBlock(-1, i - 1);
            CreateBorderBlock(i - 1, borderSize);
            CreateBorderBlock(borderSize, i);
            CreateBorderBlock(i, -1);
        }
        var barriers = _contexts.game.globals.value.BarrierPositions;
        foreach (var intvec2 in barriers)
        {
            CreateBarrierBlock(intvec2.X, intvec2.Y);
        }
    }

    private void CreateBorderBlock(int x, int y)
    {
        _contexts.game.globals.value.BorderPositions.Add(CreateBlock(x, y));
    }

    private void CreateBarrierBlock(int x, int y)
    {
        CreateBlock(x, y);
    }

    private IntVec2 CreateBlock(int x, int y)
    {
        var go = GameObject.Instantiate(_blockPrefab, new Vector2(x, y), Quaternion.identity);
        var entity = _contexts.game.CreateEntity();
        var intvec2 = new IntVec2(x, y);
        entity.AddPosition(intvec2);
        entity.AddGameObject(go);
        return intvec2;
    }
}
