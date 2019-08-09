using strange.extensions.context.impl;


public class TetrisRoot : ContextView
{
    private void Awake()
    {
        context = new TetrisContext(this);
    }
}
