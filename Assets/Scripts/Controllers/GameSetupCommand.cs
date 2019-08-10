using strange.extensions.command.impl;
using UnityEngine;

public class GameSetupCommand : Command
{
    [Inject]
    public ILocalDataService LocalDataService { get; set; }

    [Inject]
    public IGridModel GridModel { get; set; }

    [Inject]
    public IGameStateModel GameStateModel { get; set; }

    [Inject]
    public NewGameSignal NewGameSignal { get; set; }

    public override void Execute()
    {
        int gridWidth = LocalDataService.GetGridWidth();
        int gridHeight = LocalDataService.GetGridHeight();
        int slotsCount = LocalDataService.GetSlotsCount();

        GridModel.Create(gridWidth, gridHeight);
        GameStateModel.CreateSlots(slotsCount);

        NewGameSignal.Dispatch();
    }
}
