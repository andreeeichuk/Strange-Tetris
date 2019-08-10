using strange.extensions.mediation.impl;
using UnityEngine;

public class ControlsMediator : Mediator
{
    #if UNITY_EDITOR
        [Inject]
        public MouseControlsView ControlsView { get; set; }
    #elif UNITY_ANDROID
        [Inject]
        public TouchControlsView ControlsView { get; set; }
    #endif

    [Inject]
    public TryTouchBlockSignal TryTouchBlockSignal { get; set; }

    [Inject]
    public BlockTouchedSignal BlockTouchedSignal { get; set; }

    private BlockView selectedBlock;
    private Vector2 startTouchPosition;
    private Vector2 startBlockPosition;

    public override void OnRegister()
    {
        BlockTouchedSignal.AddListener(OnBlockTouched);
        ControlsView.TouchBegin.AddListener(OnTouchBegin);
        ControlsView.TouchContinue.AddListener(OnTouchContinue);
        ControlsView.TouchEnd.AddListener(OnTouchEnd);
    }

    public override void OnRemove()
    {
        BlockTouchedSignal.RemoveListener(OnBlockTouched);
        ControlsView.TouchBegin.RemoveListener(OnTouchBegin);
        ControlsView.TouchContinue.RemoveListener(OnTouchContinue);
        ControlsView.TouchEnd.RemoveListener(OnTouchEnd);
    }

    private void OnTouchBegin(Vector2 position)
    {
        TryTouchBlockSignal.Dispatch(position);
        startTouchPosition = position;
    }

    private void OnTouchContinue(Vector2 position)
    {
        if(selectedBlock!=null)
        {
            selectedBlock.SetPosition(startBlockPosition + position - startTouchPosition);
        }
    }

    private void OnTouchEnd(Vector2 position)
    {
        if(selectedBlock!=null)
        {
            selectedBlock.Place();
            selectedBlock = null;
        }
    }

    private void OnBlockTouched(BlockView block)
    {
        selectedBlock = block;
        startBlockPosition = block.transform.position;
        selectedBlock.Take();
    }
}
