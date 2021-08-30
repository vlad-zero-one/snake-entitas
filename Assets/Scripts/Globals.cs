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

    public List<IntVec2> BarrierPositions;

    public List<IntVec2> SnakePositionsExceptHead = new List<IntVec2>();
    public List<IntVec2> BorderPositions = new List<IntVec2>();
}