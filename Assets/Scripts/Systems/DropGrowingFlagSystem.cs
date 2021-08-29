using Entitas;

public class DropGrowingFlagSystem : IExecuteSystem
{
    private Contexts _contexts;

    public DropGrowingFlagSystem(Contexts contexts)
    {
        _contexts = contexts;
    }

    public void Execute()
    {
        /*
        if(_contexts.game.headEntity.isGrowing && _contexts.game.isTick)
            _contexts.game.headEntity.isGrowing = false;
            */
    }
}
