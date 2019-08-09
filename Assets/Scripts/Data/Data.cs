using UnityEngine;
using strange.extensions.mediation.impl;


public class Data : View
{
    public BlockTypes blockTypes;

    public GameObject GetBlockType(int index)
    {
        return blockTypes.blocks[index];
    }
}
