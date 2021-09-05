using UnityEngine;
using Entitas;

public class SnakeSegment
{
    static public GameObject Instantiate(GameObject segmentPrefab, int positionX, int positionY, GameObject parentGameObject)
    {
        return GameObject.Instantiate(segmentPrefab, new Vector2(positionX, positionY), Quaternion.identity, parentGameObject.transform);
    }
}