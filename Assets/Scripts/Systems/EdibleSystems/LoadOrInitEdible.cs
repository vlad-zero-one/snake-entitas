using Entitas;
using Entitas.Unity;
using UnityEngine;

public class LoadOrInitEdibleSystem : IInitializeSystem
{
    private Contexts _contexts;

    GameObject _ediblePrefab, _edibleParent;

    public LoadOrInitEdibleSystem(Contexts contexts)
    {
        _contexts = contexts;
        _ediblePrefab = _contexts.game.globals.value.EdibleCell;
        _edibleParent = GameObject.Find("Edible");
    }

    public void Initialize()
    {
        if(_contexts.game.isLoad)
        {
            Load();
        }
        else
        {
            Init();
        }
    }

    public void Load()
    {
        GameEntity edibleEntity;
        if (!_contexts.game.isEdible)
        {
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
            edibleEntity = _contexts.game.edibleEntity;
            edibleEntity.ReplacePosition(new IntVec2(PlayerPrefs.GetInt("EdibleX"), PlayerPrefs.GetInt("EdibleY")));

        }
        _contexts.game.edibleEntity.gameObject.value.transform.position = new Vector2(_contexts.game.edibleEntity.position.value.X,
            _contexts.game.edibleEntity.position.value.Y);
    }

    public void Init()
    {
        int borderSize = _contexts.game.globals.value.BorderSize;
        var barrierPositions = _contexts.game.globals.value.BarrierPositions;
        var snakePositionsExceptHead = _contexts.game.globals.value.SnakePositionsExceptHead;
        var headPosition = _contexts.game.headEntity.position.value;

        IntVec2 position = new IntVec2(Random.Range(0, borderSize), Random.Range(0, borderSize));
        while (barrierPositions.Contains(position)
            || snakePositionsExceptHead.Contains(position)
            || (position.X == headPosition.X && position.Y == headPosition.Y))
        {
            position = new IntVec2(Random.Range(0, borderSize), Random.Range(0, borderSize));
        }

        var ediblePrefab = _contexts.game.globals.value.EdibleCell;
        var edibleEntity = _contexts.game.CreateEntity();
        edibleEntity.isEdible = true;
        edibleEntity.AddPosition(position);
    }
}
