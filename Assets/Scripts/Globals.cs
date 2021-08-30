using Entitas;
using Entitas.CodeGeneration.Attributes;
using System.Collections.Generic;
using UnityEngine;

[Game, Unique, CreateAssetMenu]
public class Globals : ScriptableObject
{
    public GameObject SnakeCell;
    public GameObject BlockCell;
    public GameObject EdibleCell;

    public float Speed;

    public int BorderSize;

    public List<IntVec2> AddBarrier;

    public List<IntVec2> OccupiedPositions = new List<IntVec2>();
}