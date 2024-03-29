﻿using strange.extensions.mediation.impl;
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
    public GameSetupSignal GameSetup { get; set; }

    [Inject]
    public NewGameSignal NewGameSignal { get; set; }

    [Inject]
    public ElementsPlacedSignal ElementsPlacedSignal { get; set; }

    [Inject]
    public RowFilledSignal RowFilled { get; set; }

    [Inject]
    public AllPlacedSignal AllPlaced { get; set; }

    [Inject]
    public ResetViewsSignal ResetViews { get; set; }

    [Inject]
    public IGridModel GridModel { get; set; }

    [Inject]
    public IGameStateModel GameStateModel { get; set; }

    public override void OnRegister()
    {
        GameSetup.AddListener(OnGameSetup);
        NewGameSignal.AddListener(OnNewGame);
        ElementsPlacedSignal.AddListener(OnElementsPlaced);
        AllPlaced.AddListener(OnAllPlaced);
        RowFilled.AddListener(OnRowFilled);
        BoardView.newBlockSetRequest.AddListener(OnNewBlockSetRequest);
        BoardView.slotsFilled.AddListener(OnSlotsFilled);
        ResetViews.AddListener(OnResetViews);
    }

    public override void OnRemove()
    {
        GameSetup.RemoveListener(OnGameSetup);
        NewGameSignal.RemoveListener(OnNewGame);
        ElementsPlacedSignal.RemoveListener(OnElementsPlaced);
        AllPlaced.RemoveListener(OnAllPlaced);
        RowFilled.RemoveListener(OnRowFilled);
        BoardView.newBlockSetRequest.RemoveListener(OnNewBlockSetRequest);
        BoardView.slotsFilled.RemoveListener(OnSlotsFilled);
        ResetViews.RemoveListener(OnResetViews);
    }

    private void OnNewBlockSetRequest(int blocksNumber)
    {
        GameObject[] blocks = BlockSetGenerator.GenerateBlockSet(blocksNumber);        

        BoardView.SpawnNewBlockSet(blocks);
    }

    private void OnGameSetup()
    {
        int gridWidth = LocalDataService.GetGridWidth();
        int gridHeight = LocalDataService.GetGridHeight();

        BoardView.Init(gridWidth, gridHeight);
        GridModel.SetOriginAndStep(BoardView.GridOrigin, BoardView.GridStep);
    }

    private void OnNewGame()
    {
        BoardView.NewGame();
    }

    private void OnElementsPlaced(BlockView block, Coordinate[] coordinates)
    {
        BoardView.SetPlacedEelements(block, coordinates);
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

    private void OnResetViews()
    {
        BoardView.ResetView();
    }
}
