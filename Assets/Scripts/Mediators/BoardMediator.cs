using strange.extensions.mediation.impl;

public class BoardMediator : Mediator
{
    [Inject]
    public BoardView BoardView { get; set; }

    [Inject]
    public NewGameSignal NewGameSignal { get; set; }

    public override void OnRegister()
    {
        NewGameSignal.AddListener(OnNewGame);
    }

    public override void OnRemove()
    {
        NewGameSignal.RemoveListener(OnNewGame);
    }

    private void OnNewGame()
    {
        BoardView.Init();
    }
}
