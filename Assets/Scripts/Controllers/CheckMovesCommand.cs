using strange.extensions.command.impl;
using System.Collections.Generic;
using UnityEngine;

public class CheckMovesCommand : Command
{
    [Inject]
    public List<Coordinate[]> coordinates { get; set; }

    [Inject]
    public IGridModel GridModel { get; set; }

    public override void Execute()
    {
        bool movesArePossible = false;

        for (int i = 0; i < coordinates.Count; i++)
        {
            if(GridModel.CheckMovesForBlock(coordinates[i]))
            {
                Debug.Log($"Block N {i+1}");
                movesArePossible = true;
                break;
            }
        }

        if(!movesArePossible)
        {
            Debug.Log("No Moves!");
        }
        else
        {
            Debug.Log("Have Moves!");
        }
    }
}
