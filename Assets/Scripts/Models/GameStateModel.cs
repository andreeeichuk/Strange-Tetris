using UnityEngine;
using System.Collections.Generic;

public class GameStateModel : IGameStateModel
{
    [Inject]
    public AllPlacedSignal AllPlacedSignal { get; set; }

    [Inject]
    public CheckMovesSignal CheckMoves { get; set; }

    public Coordinate[][] BlocksInSlots { get; set; }

    private int freeSlotsCount;

    public void CreateSlots(int count)
    {
        BlocksInSlots = new Coordinate[count][];
        freeSlotsCount = count;
    }

    public void FillSlots(Coordinate[][] coordinates)
    {
        BlocksInSlots = coordinates;
        freeSlotsCount = 0;
        TriggerPossibleMovesCheck();
    }

    public void FreeSlot(int index)
    {
        freeSlotsCount++;
        BlocksInSlots[index] = null;
        if(freeSlotsCount==BlocksInSlots.Length)
        {
            AllPlacedSignal.Dispatch();
        }
        else
        {
            TriggerPossibleMovesCheck();
        }
    }

    public void TriggerPossibleMovesCheck()
    {
        List<Coordinate[]> availableBloks = new List<Coordinate[]>();

        for (int i = 0; i < BlocksInSlots.Length; i++)
        {
            if(BlocksInSlots[i]!=null)
            {
                availableBloks.Add(BlocksInSlots[i]);
            }
        }

        CheckMoves.Dispatch(availableBloks);
    }
}
