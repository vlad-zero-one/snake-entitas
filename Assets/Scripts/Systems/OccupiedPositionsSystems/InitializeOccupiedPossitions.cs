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

        if (_contexts.game.globals.value.OccupiedPositions != null)
            _contexts.game.globals.value.OccupiedPositions.Clear();

        int borderSize = _contexts.game.globals.value.BorderSize;
        for (int i = 0; i < borderSize + 1; i++)
        {
            CreateBorderBlock(-1, i - 1);
            CreateBorderBlock(borderSize, i);
            CreateBorderBlock(i - 1, borderSize);
            CreateBorderBlock(i, -1);
        }
        var barriers = _contexts.game.globals.value.AddBarrier;
        foreach (var intvec2 in barriers)
        {
            CreateBorderBlock(intvec2.X, intvec2.Y);
        }
    }

    private void CreateBorderBlock(int x, int y)
    {
        var go = GameObject.Instantiate(_blockPrefab, new Vector2(x, y), Quaternion.identity);
        var entity = _contexts.game.CreateEntity();
        var intvec2 = new IntVec2(x, y);
        entity.AddPosition(intvec2);
        entity.AddGameObject(go);
        _contexts.game.globals.value.OccupiedPositions.Add(intvec2);
    }
}
