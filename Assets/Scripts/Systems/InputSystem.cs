using Entitas;
using UnityEngine;

public class InputSystem : IExecuteSystem
{
    Contexts _contexts;

    public InputSystem(Contexts contexts)
    {
        _contexts = contexts;
    }

    public void Execute()
    {
        DirectionEnum lastMovementDirection = _contexts.game.lastMovementDirection.value;

        if (Input.GetKey(KeyCode.W) && lastMovementDirection != DirectionEnum.S)
        {
            _contexts.game.ReplaceDirection(DirectionEnum.N);
        }
        else if (Input.GetKey(KeyCode.D) && lastMovementDirection != DirectionEnum.W)
        {
            _contexts.game.ReplaceDirection(DirectionEnum.E);
        }
        else if (Input.GetKey(KeyCode.S) && lastMovementDirection != DirectionEnum.N)
        {
            _contexts.game.ReplaceDirection(DirectionEnum.S);
        }
        else if (Input.GetKey(KeyCode.A) && lastMovementDirection != DirectionEnum.E)
        {
            _contexts.game.ReplaceDirection(DirectionEnum.W);
        }
    }
}