using strange.extensions.mediation.impl;
using UnityEngine;

public class BlockView : View
{
    [SerializeField] Transform pivot;
    [SerializeField] private float spawnScale = 1f;
    [SerializeField] private float gridScale = 1.38f;

    private Vector2 spawnPoint;

    public void Init()
    {
        spawnPoint = transform.position;
        transform.position -= pivot.localPosition;
    }

    public void SetPosition(Vector2 vector2)
    {
        transform.position = vector2;
    }

    public void SetSpawnScale()
    {
        transform.localScale = new Vector3(spawnScale, spawnScale, spawnScale);
    }

    public void SetGridScale()
    {
        transform.localScale = new Vector3(gridScale, gridScale, gridScale);
    }

    public void ReturnToSpawnPoint()
    {
        transform.position = spawnPoint;
    }
}
