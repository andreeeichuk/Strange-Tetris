﻿using strange.extensions.mediation.impl;
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

    private GameObject[,] elementsOnGrid;

    public void Init(int gridWidth, int gridHeight)
    {
        Debug.Log("Board Init");
        elementsOnGrid = new GameObject[gridWidth, gridHeight];
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

    public void SetPlacedEelements(GameObject[] elements, Coordinate[] coordinates)
    {
        for (int i = 0; i < elements.Length; i++)
        {
            elements[i].transform.parent = transform;
            elementsOnGrid[coordinates[i].x, coordinates[i].y] = elements[i];
        }
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

    // returns true if all vectors are located inside grid
    public Coordinate[] ConvertVectorsToCoordinates(Vector2[] vectors)
    {
        Coordinate[] coordinates = new Coordinate[vectors.Length];

        for(int i=0;i<vectors.Length;i++)
        {
            Vector2 diff = vectors[i] - gridOrigin;
            int x = (int)(diff.x / gridStep);
            int y = (int)(diff.y / gridStep);
            coordinates[i] = new Coordinate(x, y);
        }

        return coordinates;
    }
}
