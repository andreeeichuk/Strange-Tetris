using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;
using UnityEngine;
using System.Collections.Generic;

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
    private List<GameObject> availableBlocks = new List<GameObject>();

    public void Init(int gridWidth, int gridHeight)
    {
        elementsOnGrid = new GameObject[gridWidth, gridHeight];
        SetBlockSpawnPositions();
    }
    
    public void NewGame()
    {
        RequestNewBlockSet();
    }

    public void ResetView()
    {
        ClearAll();
    }

    private void ClearAll()
    {
        for (int i = 0; i < elementsOnGrid.GetLength(0); i++)
        {
            for (int j = 0; j < elementsOnGrid.GetLength(1); j++)
            {
                if(elementsOnGrid[i,j]!=null)
                {
                    Destroy(elementsOnGrid[i, j]);
                }
            }
        }

        foreach (var item in availableBlocks)
        {
            Destroy(item);
        }

        availableBlocks.Clear();
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
        availableBlocks.Add(b);
        return b;
    }

    public void SetPlacedEelements(BlockView block, Coordinate[] coordinates)
    {
        availableBlocks.Remove(block.gameObject);

        GameObject[] elements = block.Elements;

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
