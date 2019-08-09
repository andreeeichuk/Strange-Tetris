public class BlockSetGenerator : IBlockSetGenerator
{
    private IBlockTypes blockTypes;

    public BlockSetGenerator(IBlockTypes blockTypes)
    {
        this.blockTypes = blockTypes;
    }

    // generates block set by its type's index
    public void GenerateBlockSet(int blocksNumber)
    {
        int[] blockSet = new int[blocksNumber];

        for (int i = 0; i < blockSet.Length; i++)
        {
            blockSet[i] = UnityEngine.Random.Range(0, blockTypes.blocks.Length);
        }
    }
}
