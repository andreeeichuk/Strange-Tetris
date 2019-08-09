using UnityEngine;

public class LocalDataService : ILocalDataService
{
    private Data data;

    public LocalDataService()
    {
        data = Object.FindObjectOfType<Data>();
    }

    public GameObject GetBlockByIndex(int index)
    {
        return data.blockTypes.blocks[index];
    }

    public int GetBlockTypesCount()
    {
        return data.blockTypes.blocks.Length;
    }

    public int GetGridWidth()
    {
        return data.gridWidth;
    }

    public int GetGridHeight()
    {
        return data.gridHeight;
    }

    public int GetSlotsCount()
    {
        return data.slotsCount;
    }
}
