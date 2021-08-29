using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

[Game, Unique, CreateAssetMenu]
public class Globals : ScriptableObject
{
    public GameObject SnakeCell;
    public GameObject BlockCell;
    public GameObject EdibleCell;

    public float Speed;
}