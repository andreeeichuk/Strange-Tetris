using UnityEngine;

public class MouseControlsView : ControlsView
{
    private void LateUpdate()
    {
        if(Input.GetMouseButtonDown(0))
        {
            TouchBegin.Dispatch(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }

        if(Input.GetMouseButton(0))
        {
            TouchContinue.Dispatch(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }

        if(Input.GetMouseButtonUp(0))
        {
            TouchEnd.Dispatch(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
    }
}
