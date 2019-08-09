using strange.extensions.context.impl;
using UnityEngine;

public class TetrisRoot : ContextView
{ 
    private void Awake()
    {
        context = new TetrisContext(this);
    }
}
