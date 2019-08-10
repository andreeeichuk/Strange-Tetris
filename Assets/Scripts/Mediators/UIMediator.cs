using strange.extensions.mediation.impl;
using UnityEngine;

public class UIMediator : Mediator
{
    [Inject]
    public UIView UI { get; set; }

    [Inject]
    public NoMovesSignal NoMoves { get; set; }

    [Inject]
    public RestartGameSignal RestartGame { get; set; }

    [Inject]
    public ResetViewsSignal ResetViews { get; set; }

    public override void OnRegister()
    {
        NoMoves.AddListener(OnNoMoves);
        ResetViews.AddListener(OnResetViews);
        UI.restartPressed.AddListener(OnRestartButtonPressed);
    }

    public override void OnRemove()
    {
        NoMoves.RemoveListener(OnNoMoves);
        ResetViews.RemoveListener(OnResetViews);
        UI.restartPressed.RemoveListener(OnRestartButtonPressed);
    }

    public void OnNoMoves()
    {
        UI.ShowNoMovesPopup();
    }

    public void OnRestartButtonPressed()
    {
        RestartGame.Dispatch();
    }

    public void OnResetViews()
    {
        UI.ResetView();
    }
}
