using Entitas;
using Entitas.Unity;
using UnityEngine;

public class LoadEdibleSystem : IInitializeSystem
{
    private Contexts _contexts;

    GameObject _ediblePrefab, _edibleParent;

    public LoadEdibleSystem(Contexts contexts)
    {
        _contexts = contexts;
        _ediblePrefab = _contexts.game.globals.value.EdibleCell;
        _edibleParent = GameObject.Find("Edible");
    }

    public void Initialize()
    {
        GameEntity edibleEntity;
        if (!_contexts.game.isEdible)
        {
            Debug.Log("Recreate edible");
            edibleEntity = _contexts.game.CreateEntity();
            edibleEntity.ReplacePosition(new IntVec2(PlayerPrefs.GetInt("EdibleX"), PlayerPrefs.GetInt("EdibleY")));
            edibleEntity.isEdible = true;

            var go = GameObject.Instantiate(
                _ediblePrefab,
                new Vector2(edibleEntity.position.value.X, edibleEntity.position.value.Y),
                Quaternion.identity,
                _edibleParent.transform);
            go.Link(edibleEntity);
            edibleEntity.ReplaceGameObject(go);
        }
        else
        {
            Debug.Log("Try get from contexts");
            edibleEntity = _contexts.game.edibleEntity;
            edibleEntity.ReplacePosition(new IntVec2(PlayerPrefs.GetInt("EdibleX"), PlayerPrefs.GetInt("EdibleY")));

        }
        _contexts.game.edibleEntity.gameObject.value.transform.position = new Vector2(_contexts.game.edibleEntity.position.value.X,
            _contexts.game.edibleEntity.position.value.Y);
    }
}
