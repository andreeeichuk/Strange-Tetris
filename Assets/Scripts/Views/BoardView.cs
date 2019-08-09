﻿using strange.extensions.mediation.impl;
using UnityEngine;

public class BoardView : View
{
    [SerializeField] private Vector2 blockSpawnPosition = new Vector2(0f,-7.5f);
    [SerializeField] private Vector2 blockSpawnOffset = new Vector2(5f, 0f);

    [SerializeField] private GameObject[] blocks;

    private Vector2[] blockSpawnPositions;

    public void Init()
    {
        SetBlockSpawnPositions();
    }
    
    public void SpawnBlock(int spawnPoint, int blockVariant)
    {
        Instantiate(blocks[blockVariant], blockSpawnPositions[spawnPoint], Quaternion.identity, this.transform);
    }

    private void SetBlockSpawnPositions()
    {
        blockSpawnPositions = new Vector2[3];

        blockSpawnPositions[0] = blockSpawnPosition;
        blockSpawnPositions[1] = blockSpawnPosition + blockSpawnOffset;
        blockSpawnPositions[2] = blockSpawnPosition - blockSpawnOffset;
    }
}