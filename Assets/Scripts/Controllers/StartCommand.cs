using strange.extensions.command.impl;
using strange.extensions.context.api;
using UnityEngine;

public class StartCommand : Command
{
    [Inject]
    public NewGameSignal newGameSignal { get; set; }

    [Inject(ContextKeys.CONTEXT_VIEW)]
    public GameObject contextView { get; set; }

    public override void Execute()
    {
        #if UNITY_EDITOR
            GameObject mouseControls = new GameObject();
            mouseControls.name = "Mouse Controls";
            mouseControls.AddComponent<MouseControlsView>();
            mouseControls.transform.parent = contextView.transform;

        #elif UNITY_ANDROID
            GameObject touchControls = new GameObject();
            touchControls.name = "Touch Controls";
            touchControls.AddComponent<TouchControlsView>();
            touchControls.transform.parent = contextView.transform;
        #endif

        newGameSignal.Dispatch();
    }
}
