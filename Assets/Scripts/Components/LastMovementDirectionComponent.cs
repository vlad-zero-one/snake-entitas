using Entitas;
using Entitas.CodeGeneration.Attributes;

[Game, Unique]
public class LastMovementDirectionComponent : IComponent
{
    public DirectionEnum value;
}