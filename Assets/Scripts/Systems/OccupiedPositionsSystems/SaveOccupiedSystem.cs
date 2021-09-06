using Entitas;
using UnityEngine;

public class SaveOccupiedSystem : IExecuteSystem
{
    private Contexts _contexts;

    public SaveOccupiedSystem(Contexts contexts)
    {
        _contexts = contexts;
    }

    public void Execute()
    {
        PlayerPrefs.SetInt("BorderSize", _contexts.game.globals.value.BorderSize);

        var barriers = _contexts.game.globals.value.BarrierPositions;
        PlayerPrefs.SetInt("BarrierSize", barriers.Count);
        for (int i = 0; i < barriers.Count; i++)
        {
            PlayerPrefs.SetInt("Barrier" + i + "X", barriers[i].X);
            PlayerPrefs.SetInt("Barrier" + i + "Y", barriers[i].Y);
        }
    }
}
