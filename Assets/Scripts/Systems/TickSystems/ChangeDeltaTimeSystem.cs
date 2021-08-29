using Entitas;
using UnityEngine;

public class ChangeDeltaTimeSystem : IExecuteSystem
{
    private Contexts _contexts;

    public ChangeDeltaTimeSystem(Contexts contexts)
    {
        _contexts = contexts;
    }

    public void Execute()
    {
        _contexts.game.ReplaceDeltaTime(Time.deltaTime);
    }
}
