using strange.extensions.mediation.impl;
using UnityEngine;

public class BoardMediator : Mediator
{
    [Inject]
    public BoardView BoardView { get; set; }

    [Inject]
    public ILocalDataService LocalDataService { get; set; }

    [Inject]
    public IBlockSetGenerator BlockSetGenerator { get; set; }

    [Inject]
    public NewGameReadySignal NewGameReadySignal { get; set; }

    [Inject]
    public ElementsPlacedSignal ElementsPlacedSignal { get; set; }

    [Inject]
    public IGridModel GridModel { get; set; } // temporary solution

    public override void OnRegister()
    {
        NewGameReadySignal.AddListener(OnNewGameReady);
        ElementsPlacedSignal.AddListener(OnElementsPlaced);
        BoardView.newBlockSetRequest.AddListener(OnNewBlockSetRequest);
    }

    public override void OnRemove()
    {
        NewGameReadySignal.RemoveListener(OnNewGameReady);
        ElementsPlacedSignal.RemoveListener(OnElementsPlaced);
        BoardView.newBlockSetRequest.RemoveListener(OnNewBlockSetRequest);
    }
    
    private void OnNewBlockSetRequest(int blocksNumber)
    {
        GameObject[] blocks = BlockSetGenerator.GenerateBlockSet(blocksNumber);        

        BoardView.SpawnNewBlockSet(blocks);
    }

    private void OnNewGameReady()
    {
        int gridWidth = LocalDataService.GetGridWidth();
        int gridHeight = LocalDataService.GetGridHeight();

        BoardView.Init(gridWidth,gridHeight);

        // this should be set only once
        GridModel.SetOriginAndStep(BoardView.GridOrigin, BoardView.GridStep);
    }

    private void OnElementsPlaced(GameObject[] elements, Coordinate[] coordinates)
    {
        BoardView.SetPlacedEelements(elements, coordinates);
    }
}
