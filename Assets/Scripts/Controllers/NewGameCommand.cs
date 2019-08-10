using strange.extensions.command.impl;
using UnityEngine;

public class NewGameCommand : Command
{
    [Inject]
    public ILocalDataService LocalDataService { get; set; }

    [Inject]
    public IGridModel GridModel { get; set; }

    [Inject]
    public IGameStateModel GameStateModel { get; set; }

    [Inject]
    public NewGameReadySignal NewGameReadySignal { get; set; }

    public override void Execute()
    {
        int gridWidth = LocalDataService.GetGridWidth();
        int gridHeight = LocalDataService.GetGridHeight();
        int slotsCount = LocalDataService.GetSlotsCount();

        GridModel.Create(gridWidth, gridHeight);
        GameStateModel.CreateSlots(slotsCount);

        NewGameReadySignal.Dispatch();
    }
}
