using strange.extensions.command.impl;

public class RestartGameCommand : Command
{
    [Inject]
    public ResetViewsSignal ResetViews { get; set; }

    [Inject]
    public NewGameSignal NewGame { get; set; }

    public override void Execute()
    {
        ResetViews.Dispatch();
        NewGame.Dispatch();
    }
}
