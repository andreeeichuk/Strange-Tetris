using strange.extensions.command.impl;

public class StartCommand : Command
{
    [Inject]
    public NewGameSignal newGameSignal { get; set; }

    public override void Execute()
    {
        newGameSignal.Dispatch();
    }
}
