using strange.extensions.command.impl;

public class RestartGameCommand : Command
{
    [Inject]
    public ResetViewsSignal ResetViews { get; set; }

    [Inject]
    public NewGameSignal NewGame { get; set; }

    [Inject]
    public IGameStateModel GameState { get; set; }

    [Inject]
    public IGridModel Grid { get; set; }

    public override void Execute()
    {
        ResetModels();
        ResetViews.Dispatch();
        NewGame.Dispatch();
    }

    private void ResetModels()
    {
        Grid.ResetCellsAndRows();
        GameState.ResetSlots();
    }
}
