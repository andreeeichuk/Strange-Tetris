using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;
using UnityEngine;

public class BoardView : View
{
    public Signal<int> newBlockSetRequest = new Signal<int>();

    [SerializeField] private Vector2 gridOrigin = new Vector2(-6.13f,-3.43f);
    [SerializeField] private float gridStep = 1.373f;
    [SerializeField] private float spawnBorder = -5f;
    [SerializeField] private Vector2 blockSpawnPosition = new Vector2(0f,-7.5f);
    [SerializeField] private Vector2 blockSpawnOffset = new Vector2(5f, 0f);

    private Vector2[] blockSpawnPositions;    

    public void Init()
    {
        Debug.Log("Board Init");
        SetBlockSpawnPositions();
        RequestNewBlockSet();
    }    

    public void SpawnNewBlockSet(GameObject[] blocks)
    {
        for (int i = 0; i < blockSpawnPositions.Length; i++)
        {
            SpawnBlock(i, blocks[i]);
        }
    }
    
    public void SpawnBlock(int spawnPoint, GameObject block)
    {
        BlockView blockView = Instantiate(block, blockSpawnPositions[spawnPoint], Quaternion.identity, this.transform).GetComponent<BlockView>();
        blockView.Init(spawnBorder);
    }

    private void SetBlockSpawnPositions()
    {
        blockSpawnPositions = new Vector2[3];

        blockSpawnPositions[0] = blockSpawnPosition;
        blockSpawnPositions[1] = blockSpawnPosition + blockSpawnOffset;
        blockSpawnPositions[2] = blockSpawnPosition - blockSpawnOffset;
    }

    private void RequestNewBlockSet()
    {
        newBlockSetRequest.Dispatch(blockSpawnPositions.Length);
    }
}
