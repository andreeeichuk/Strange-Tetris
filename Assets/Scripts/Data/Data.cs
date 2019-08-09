using UnityEngine;
using strange.extensions.mediation.impl;


public class Data : View
{
    public BlockTypes blockTypes;
    public int gridWidth;
    public int gridHeight;
    public int slotsCount;

    public GameObject GetBlockType(int index)
    {
        return blockTypes.blocks[index];
    }
}
