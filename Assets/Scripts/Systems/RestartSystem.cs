using Entitas;
using Entitas.Unity;
using UnityEngine;

public class RestartSystem : IExecuteSystem
{
    private Contexts _contexts;

    public RestartSystem(Contexts contexts)
    {
        _contexts = contexts;
    }

    public void Execute()
    {
        /*
        var e = _contexts.game.CreateCollector(GameMatcher.Edible);
        var ee = e.GetCollectedEntities<GameEntity>();
        foreach(var eee in ee)
        {
            Debug.Log("!");
            var go = eee.gameObject.value;
            go.Unlink();
            eee.RemoveGameObject();
            GameObject.Destroy(go);
            eee.Destroy();
        }
        */
        Debug.Log("EXECUTED RESTART");
        var snake = GameObject.Find("Snake");
        foreach (Transform segment in snake.transform)
        {
            var entity = segment.gameObject.GetEntityLink().entity;
            segment.gameObject.Unlink();
            entity.Destroy();
            GameObject.Destroy(segment.gameObject);
        }
        var edibleGameObject = GameObject.Find("Edible").transform.GetChild(0).gameObject;
        var edibleEntity = edibleGameObject.GetEntityLink().entity;
        edibleGameObject.gameObject.Unlink();
        edibleEntity.Destroy();
        GameObject.Destroy(edibleGameObject);

        _contexts.game.isGameOver = false;
    }
    /*
public void Cleanup()
{
   var edible = _contexts.game.edibleEntity;
   GameObject.Destroy(edible.gameObject.value);
   edible.Destroy();

   var currentEntity = _contexts.game.tailEntity;
   while(currentEntity.hasPreviousSegment)
   {
       var nextEntity = currentEntity.previousSegment.value;
       //GameObject.Destroy(currentEntity.gameObject.value);
       currentEntity.Destroy();
       currentEntity = nextEntity;
   }
   //GameObject.Destroy(currentEntity.gameObject.value);
   currentEntity.Destroy();


   Debug.Log(_contexts.game.count);
   _contexts.game.DestroyAllEntities();
   Debug.Log(_contexts.game.count);

   //var borders = GameObject.Find("Borders");
   //foreach(Transform border in borders.transform)
   //{
   //    GameObject.Destroy(border.gameObject);
   //}
   //var barriers = GameObject.Find("Barriers");
   //foreach (Transform barrier in barriers.transform)
   //{
   //    GameObject.Destroy(barriers.gameObject);
   //}
   var snake = GameObject.Find("Snake");
   foreach (Transform segment in snake.transform)
   {
       GameObject.Destroy(snake.gameObject);
   }
   //var edibleGO = GameObject.Find("Edible");
   //GameObject.Destroy(edibleGO.transform.GetChild(0).gameObject);
}
*/

}
