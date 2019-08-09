using UnityEngine;

public class BlockSetGenerator : IBlockSetGenerator
{
    private ILocalDataService localDataService;

    public BlockSetGenerator(ILocalDataService localDataService)
    {
        this.localDataService = localDataService;
    }

    // generates block set by its type's index
    public GameObject[] GenerateBlockSet(int blocksNumber)
    {
        GameObject[] blockSet = new GameObject[blocksNumber];

        for (int i = 0; i < blockSet.Length; i++)
        {
            int random = Random.Range(0, localDataService.GetBlockTypesCount());
            blockSet[i] = localDataService.GetBlockByIndex(random);
        }

        return blockSet;
    }
}
