using strange.extensions.mediation.impl;
using UnityEngine;
using System;

public class BlockView : View
{
    [Inject]
    public TryPlaceBlockSignal TryPlaceBlockSignal { get; set; }

    public GameObject[] Elements { get { return elements; } }
    public int SlotIndex { get; private set; }

    [SerializeField] Transform pivot;
    [SerializeField] private float spawnScale = 1f;
    [SerializeField] private float gridScale = 1.38f;
    [SerializeField] GameObject[] elements;

    private Vector2 spawnPoint;
    private float spawnBorder;
    private bool isTaken;

    public void Init(float spawnBorder, int slot)
    {
        spawnPoint = transform.position - pivot.localPosition;
        transform.position -= pivot.localPosition;
        this.spawnBorder = spawnBorder;
        SlotIndex = slot;
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
            TryPlaceBlockSignal.Dispatch(this);            
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

    public void DestroySelf()
    {
        Array.Clear(elements, 0, elements.Length);
        Destroy(gameObject);
    }

    public Coordinate[] GetElementsLocalCoordinates()
    {
        Coordinate[] coordinates = new Coordinate[elements.Length];

        for (int i = 0; i < elements.Length; i++)
        {
            coordinates[i] = new Coordinate((int)elements[i].transform.localPosition.x,
                (int)elements[i].transform.localPosition.y);
        }

        return coordinates;
    }

    //returns its elements
    
}
