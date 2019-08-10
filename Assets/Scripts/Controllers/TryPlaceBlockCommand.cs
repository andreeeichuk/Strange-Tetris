using strange.extensions.command.impl;
using UnityEngine;

public class TryPlaceBlockCommand : Command
{
    [Inject]
    public BlockView Block { get; set; }    

    [Inject]
    public IGridModel GridModel { get; set; }    

    [Inject]
    public ElementsPlacedSignal ElementsPlacedSignal { get; set; }

    [Inject]
    public IGameStateModel GameStateModel { get; set; }

    public override void Execute()
    {
        GameObject[] elements = Block.Elements;

        Vector2[] elementsPositions = new Vector2[elements.Length];

        for (int i = 0; i < elements.Length; i++)
        {
            elementsPositions[i] = elements[i].transform.position;
        }

        Coordinate[] coordinates = GridModel.ConvertVectorsToCoordinates(elementsPositions);        

        if(GridModel.TryPlaceBlock(coordinates))
        {
            ElementsPlacedSignal.Dispatch(Block, coordinates);
            GridModel.PlaceBlock(coordinates);
            GameStateModel.FreeSlot(Block.SlotIndex);
            Block.DestroySelf();
        }
    }
}
