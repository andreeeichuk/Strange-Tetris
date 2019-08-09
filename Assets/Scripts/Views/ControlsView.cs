using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;
using UnityEngine;

public abstract class ControlsView : View
{
    public Signal<Vector2> TouchBegin = new Signal<Vector2>();
    public Signal<Vector2> TouchContinue = new Signal<Vector2>();
    public Signal<Vector2> TouchEnd = new Signal<Vector2>();
}
