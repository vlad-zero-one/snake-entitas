using Entitas;

internal class StartTimeSystem : IExecuteSystem
{
    private Contexts _contexts;

    public StartTimeSystem(Contexts contexts)
    {
        _contexts = contexts;
    }

    public void Execute()
    {
        if(!_contexts.game.isStartTime)
        {
            _contexts.game.isStartTime = true;
        }
    }
}