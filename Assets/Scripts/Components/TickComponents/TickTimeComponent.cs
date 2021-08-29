using Entitas;
using Entitas.CodeGeneration.Attributes;

[Game, Unique]
public class TickTimeComponent : IComponent
{
    public float value;
}