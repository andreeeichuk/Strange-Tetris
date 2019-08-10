using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;
using UnityEngine;

public class BoardView : View
{
    public Signal<int> newBlockSetRequest = new Signal<int>();
    public Signal<BlockView[]> slotsFilled = new Signal<BlockView[]>();

    public Vector2 GridOrigin { get { return gridOrigin; } }
    public float GridStep { get { return gridStep; } }

    [SerializeField] private Vector2 gridOrigin = new Vector2(-6.13f,-3.43f);
    [SerializeField] private float gridStep = 1.373f;
    [SerializeField] private float spawnBorder = -5f;
    [SerializeField] private Vector2 blockSpawnPosition = new Vector2(0f,-7.5f);
    [SerializeField] private Vector2 blockSpawnOffset = new Vector2(5f, 0f);

    private Vector2[] blockSpawnPositions;

    private GameObject[,] elementsOnGrid;
    private GameObject[] blocksToPlace;

    public void Init(int gridWidth, int gridHeight)
    {
        Debug.Log("Board Init");
        elementsOnGrid = new GameObject[gridWidth, gridHeight];
        SetBlockSpawnPositions();
        RequestNewBlockSet();
    }    

    public void SpawnNewBlockSet(GameObject[] blocks)
    {
        BlockView[] blockViews = new BlockView[blocks.Length];

        for (int i = 0; i < blockSpawnPositions.Length; i++)
        {
            GameObject spawnedBlock = SpawnBlock(i, blocks[i]);
            BlockView blockView = spawnedBlock.GetComponent<BlockView>();
            blockView.Init(spawnBorder, i);
            blockViews[i] = blockView;
        }

        slotsFilled.Dispatch(blockViews);
    }

    public GameObject SpawnBlock(int spawnPoint, GameObject block)
    {
        GameObject b = Instantiate(block, blockSpawnPositions[spawnPoint], Quaternion.identity, this.transform);
        return b;
    }

    public void SetPlacedEelements(GameObject[] elements, Coordinate[] coordinates)
    {
        for (int i = 0; i < elements.Length; i++)
        {
            elements[i].transform.parent = transform;
            elements[i].transform.position = gridOrigin + new Vector2(coordinates[i].x * gridStep, coordinates[i].y * gridStep);
            elements[i].GetComponent<SpriteRenderer>().sortingOrder = 9;
            elementsOnGrid[coordinates[i].x, coordinates[i].y] = elements[i];
        }
    }

    public void ClearRow(int rowIndex)
    {
        for (int i = 0; i < elementsOnGrid.GetLength(0); i++)
        {
            Destroy(elementsOnGrid[i, rowIndex]);
        }
    }

    private void SetBlockSpawnPositions()
    {
        blockSpawnPositions = new Vector2[3];

        blockSpawnPositions[0] = blockSpawnPosition;
        blockSpawnPositions[1] = blockSpawnPosition + blockSpawnOffset;
        blockSpawnPositions[2] = blockSpawnPosition - blockSpawnOffset;
    }

    public void RequestNewBlockSet()
    {
        newBlockSetRequest.Dispatch(blockSpawnPositions.Length);
    }    
}
