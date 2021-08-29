using Entitas;
using UnityEngine;

public class InitializeEdibleSystem : IInitializeSystem
{
    private Contexts _contexts;

    public InitializeEdibleSystem(Contexts contexts)
    {
        _contexts = contexts;
    }

    public void Initialize()
    {
        var ediblePrefab = _contexts.game.globals.value.EdibleCell;
        var edibleEntity = _contexts.game.CreateEntity();
        edibleEntity.isEdible = true;
        edibleEntity.AddPosition(new IntVec2(Random.Range(-5, 5), Random.Range(-5, 5)));
        var go = GameObject.Instantiate(
            ediblePrefab,
            new Vector2(edibleEntity.position.value.X, edibleEntity.position.value.Y),
            Quaternion.identity);
        edibleEntity.ReplaceGameObject(go);
    }
}
