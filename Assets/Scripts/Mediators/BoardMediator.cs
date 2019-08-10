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
    public RowFilledSignal RowFilled { get; set; }

    [Inject]
    public AllPlacedSignal AllPlaced { get; set; }

    [Inject]
    public IGridModel GridModel { get; set; } // temporary solution

    [Inject]
    public IGameStateModel GameStateModel { get; set; }

    public override void OnRegister()
    {
        NewGameReadySignal.AddListener(OnNewGameReady);
        ElementsPlacedSignal.AddListener(OnElementsPlaced);
        AllPlaced.AddListener(OnAllPlaced);
        RowFilled.AddListener(OnRowFilled);
        BoardView.newBlockSetRequest.AddListener(OnNewBlockSetRequest);
        BoardView.slotsFilled.AddListener(OnSlotsFilled);
    }

    public override void OnRemove()
    {
        NewGameReadySignal.RemoveListener(OnNewGameReady);
        ElementsPlacedSignal.RemoveListener(OnElementsPlaced);
        AllPlaced.RemoveListener(OnAllPlaced);
        RowFilled.RemoveListener(OnRowFilled);
        BoardView.newBlockSetRequest.RemoveListener(OnNewBlockSetRequest);
        BoardView.slotsFilled.RemoveListener(OnSlotsFilled);
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

    private void OnAllPlaced()
    {
        BoardView.RequestNewBlockSet();
    }

    private void OnRowFilled(int rowIndex)
    {
        BoardView.ClearRow(rowIndex);
    }
    
    private void OnSlotsFilled(BlockView[] blockViews)
    {
        Coordinate[][] c = new Coordinate[blockViews.Length][];

        for (int i = 0; i < blockViews.Length; i++)
        {
            c[i] = blockViews[i].GetElementsLocalCoordinates(); 
        }

        GameStateModel.FillSlots(c);
    }
}
