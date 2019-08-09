using strange.extensions.command.impl;

public class BlockPlacedCommand : Command
{
    [Inject]
    public BlockView BlockView { get; set; }

    [Inject]
    public Coordinate[] Coordinates { get; set; }

    [Inject]
    public BoardView BoardView { get; set; }

    [Inject]
    public IGameStateModel GameStateModel { get; set; }

    public override void Execute()
    {
        BoardView.SetPlacedEelements(BlockView.Elements, Coordinates);
        BlockView.DestroySelf();
        GameStateModel.FreeOneSlot();
    }
}
