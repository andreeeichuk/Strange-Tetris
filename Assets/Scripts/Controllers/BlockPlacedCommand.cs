using strange.extensions.command.impl;

public class BlockPlacedCommand : Command
{
    [Inject]
    public BlockView BlockView { get; set; }

    [Inject]
    public Coordinate[] Coordinates { get; set; }

    [Inject]
    public ElementsPlacedSignal ElementsPlacedSignal { get; set; }

    [Inject]
    public IGameStateModel GameStateModel { get; set; }

    public override void Execute()
    {
        ElementsPlacedSignal.Dispatch(BlockView.Elements, Coordinates);
        BlockView.DestroySelf();
        GameStateModel.FreeOneSlot();
    }
}
