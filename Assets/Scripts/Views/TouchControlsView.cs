using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchControlsView : ControlsView
{
    private void LateUpdate()
    {
        if (Input.touches.Length>0)
        {
            //foreach(var touch in Input.touches)
            //{
            //    if(touch.phase == TouchPhase.Began)
            //    {
            //        TouchBegin.Dispatch(Camera.main.ScreenToWorldPoint(touch.position));
            //    }

            //    if(touch.phase == TouchPhase.Moved)
            //    {
            //        TouchContinue.Dispatch(Camera.main.ScreenToWorldPoint(touch.position));
            //    }

            //    if (touch.phase == TouchPhase.Ended)
            //    {
            //        TouchEnd.Dispatch(Camera.main.ScreenToWorldPoint(touch.position));
            //    }
            //}
            Touch firstTouch = Input.touches[0];

            if (firstTouch.phase == TouchPhase.Began)
            {
                TouchBegin.Dispatch(Camera.main.ScreenToWorldPoint(firstTouch.position));
            }

            if (firstTouch.phase == TouchPhase.Moved)
            {
                TouchContinue.Dispatch(Camera.main.ScreenToWorldPoint(firstTouch.position));
            }

            if (firstTouch.phase == TouchPhase.Ended || firstTouch.phase == TouchPhase.Canceled)
            {
                TouchEnd.Dispatch(Camera.main.ScreenToWorldPoint(firstTouch.position));
            }
        }
    }
}
