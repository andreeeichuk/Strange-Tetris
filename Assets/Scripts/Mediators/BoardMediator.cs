using strange.extensions.mediation.impl;
using UnityEngine;

public class BoardMediator : Mediator
{
    [Inject]
    public ILocalDataService LocalDataService { get; set; }

    [Inject]
    public BoardView BoardView { get; set; }    

    [Inject]
    public NewGameSignal NewGameSignal { get; set; }

    [Inject]
    public IBlockSetGenerator BlockSetGenerator { get; set; }

    public override void OnRegister()
    {
        NewGameSignal.AddListener(OnNewGame);
        BoardView.newBlockSetRequest.AddListener(OnNewBlockSetRequest);
    }

    public override void OnRemove()
    {
        NewGameSignal.RemoveListener(OnNewGame);
    }

    private void OnNewGame()
    {
        BoardView.Init();
    }

    private void OnNewBlockSetRequest(int blocksNumber)
    {
        GameObject[] blocks = BlockSetGenerator.GenerateBlockSet(blocksNumber);        

        BoardView.SpawnNewBlockSet(blocks);
    }
}
