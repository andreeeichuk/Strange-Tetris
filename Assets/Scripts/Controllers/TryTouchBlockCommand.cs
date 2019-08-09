using strange.extensions.command.impl;
using UnityEngine;

public class TryTouchBlockCommand : Command
{
    [Inject]
    public Vector2 Position { get; set; }

    [Inject]
    public BlockTouchedSignal BlockTouchedSignal { get; set; }

    public override void Execute()
    {
        RaycastHit2D raycastHit2D = Physics2D.Raycast(Position, Vector2.zero);
        if (raycastHit2D)
        {
            BlockView block = raycastHit2D.collider.GetComponent<BlockView>();
            BlockTouchedSignal.Dispatch(block);
        }
    }
}
