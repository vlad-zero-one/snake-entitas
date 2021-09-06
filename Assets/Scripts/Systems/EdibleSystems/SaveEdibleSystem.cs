using Entitas;
using UnityEngine;

public class SaveEdibleSystem : IExecuteSystem
{
    private Contexts _contexts;

    public SaveEdibleSystem(Contexts contexts)
    {
        _contexts = contexts;
    }

    public void Execute()
    {
        PlayerPrefs.SetInt("EdibleX", _contexts.game.edibleEntity.position.value.X);
        PlayerPrefs.SetInt("EdibleY", _contexts.game.edibleEntity.position.value.Y);
    }
}
