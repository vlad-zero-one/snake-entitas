using Entitas;
using UnityEngine;

public class LoadOrInitOccupiedSystem : IInitializeSystem
{
    private Contexts _contexts;

    public LoadOrInitOccupiedSystem(Contexts contexts)
    {
        _contexts = contexts;
    }

    public void Initialize()
    {
        if (_contexts.game.isLoad)
        {
            Load();
        }
        else
        {
            Init();
        }
    }

    public void Load()
    {
        _contexts.game.globals.value.BorderSize = PlayerPrefs.GetInt("BorderSize");

        _contexts.game.globals.value.BorderPositions.Clear();
        _contexts.game.globals.value.BarrierPositions.Clear();

        int barriersSize = PlayerPrefs.GetInt("BarrierSize");
        var barriersList = _contexts.game.globals.value.BarrierPositions;

        for (int i = 0; i < barriersSize; i++)
        {
            IntVec2 barrierPosition = new IntVec2
            {
                X = PlayerPrefs.GetInt("Barrier" + i + "X"),
                Y = PlayerPrefs.GetInt("Barrier" + i + "Y")
            };

            barriersList.Add(barrierPosition);
        }

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

    public void Init()
    {
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
        var entity = _contexts.game.CreateEntity();
        var intvec2 = new IntVec2(x, y);
        entity.AddPosition(intvec2);
        entity.isBorder = true;

        _contexts.game.globals.value.BorderPositions.Add(intvec2);
    }

    private void CreateBarrierBlock(int x, int y)
    {
        var entity = _contexts.game.CreateEntity();
        var intvec2 = new IntVec2(x, y);
        entity.AddPosition(intvec2);
        entity.isBarrier = true;
    }
}
