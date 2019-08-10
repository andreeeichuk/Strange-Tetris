using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchControlsView : ControlsView
{
    private void LateUpdate()
    {
        if (Input.touches.Length>0)
        {
            foreach(var touch in Input.touches)
            {
                if(touch.phase == TouchPhase.Began)
                {
                    TouchBegin.Dispatch(Camera.main.ScreenToWorldPoint(touch.position));
                }

                if(touch.phase == TouchPhase.Moved)
                {
                    TouchContinue.Dispatch(Camera.main.ScreenToWorldPoint(touch.position));
                }

                if (touch.phase == TouchPhase.Ended)
                {
                    TouchEnd.Dispatch(Camera.main.ScreenToWorldPoint(touch.position));
                }
            }
        }        
    }
}
