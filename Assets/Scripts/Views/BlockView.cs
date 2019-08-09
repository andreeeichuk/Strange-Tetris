using strange.extensions.mediation.impl;
using UnityEngine;

public class BlockView : View
{
    [SerializeField] Transform pivot;
    [SerializeField] private float spawnScale = 1f;
    [SerializeField] private float gridScale = 1.38f;

    private Vector2 spawnPoint;
    private float spawnBorder;
    private bool isTaken;

    public void Init(float spawnBorder)
    {
        spawnPoint = transform.position - pivot.localPosition;
        transform.position -= pivot.localPosition;
        this.spawnBorder = spawnBorder;
    }

    public void Take()
    {
        if (!isTaken)
        {
            SetGridScale();
        }
        isTaken = true;
    }

    public void Place()
    {
        if(transform.position.y<spawnBorder)
        {
            isTaken = false;
            ReturnToSpawnPoint();
            SetSpawnScale();
        }
        else
        {
            // disassemble if can be placed;
        }
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
