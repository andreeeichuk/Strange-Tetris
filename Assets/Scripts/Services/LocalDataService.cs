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
}
