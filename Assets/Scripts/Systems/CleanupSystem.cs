using Entitas;
using Entitas.Unity;
using UnityEngine;

public class CleanupSystem : ICleanupSystem
{
    private Contexts _contexts;

    private GameObject _snakeParrent, _edibleParrent, _bordersParrent, _barriersParrent;

    public CleanupSystem(Contexts contexts)
    {
        _contexts = contexts;
        _snakeParrent = GameObject.Find("Snake");
        _edibleParrent = GameObject.Find("Edible");
        _bordersParrent = GameObject.Find("Borders");
        _barriersParrent = GameObject.Find("Barriers");
    }

    public void Cleanup()
    {
        Debug.Log("EXECUTED RESTART");
        foreach (Transform segment in _snakeParrent.transform)
        {
            var entity = segment.gameObject.GetEntityLink().entity;
            segment.gameObject.Unlink();
            entity.Destroy();
            GameObject.Destroy(segment.gameObject);
        }
        foreach (Transform edible in _edibleParrent.transform)
        {
            var edibleEntity = edible.gameObject.GetEntityLink().entity;
            edible.gameObject.Unlink();
            edibleEntity.Destroy();
            GameObject.Destroy(edible.gameObject);
        }
        foreach (Transform border in _bordersParrent.transform)
        {
            var borderEntity = border.gameObject.GetEntityLink().entity;
            border.gameObject.Unlink();
            borderEntity.Destroy();
            GameObject.Destroy(border.gameObject);
        }
        foreach (Transform barrier in _barriersParrent.transform)
        {
            var barrierEntity = barrier.gameObject.GetEntityLink().entity;
            barrier.gameObject.Unlink();
            barrierEntity.Destroy();
            GameObject.Destroy(barrier.gameObject);
        }

        _contexts.game.isGameOver = false;
    }
}