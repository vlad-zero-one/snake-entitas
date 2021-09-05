using Entitas;
using Entitas.CodeGeneration.Attributes;
using System.Collections.Generic;
using UnityEngine;

[Game, CreateAssetMenu]
public class Level : ScriptableObject
{
    public float Speed;

    public int BorderSize;

    public List<IntVec2> BarrierPositions;
}