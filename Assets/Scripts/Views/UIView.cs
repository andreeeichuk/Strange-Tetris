using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;
using UnityEngine;
using UnityEngine.UI;

public class UIView : View
{
    public GameObject popup;

    public Signal restartPressed = new Signal();

    public void ShowNoMovesPopup()
    {
        popup.SetActive(true);
    }

    public void PressRestart()
    {
        restartPressed.Dispatch();
    }

    public void ResetView()
    {
        popup.SetActive(false);
    }
}
