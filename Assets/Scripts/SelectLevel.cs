using System.Collections.Generic;
using UnityEngine;

public class SelectLevel : MonoBehaviour
{
    public Globals Globals;

    public List<Level> Levels;

    public void Select(int levelNumber)
    {
        Level selected = Levels[levelNumber - 1];

        Globals.BorderSize = selected.BorderSize;
        Globals.Speed = selected.Speed;
        Globals.BarrierPositions = new List<IntVec2>(selected.BarrierPositions);
    }
}
