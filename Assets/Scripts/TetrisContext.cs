using UnityEngine;
using strange.extensions.context.api;
using strange.extensions.context.impl;
using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.dispatcher.eventdispatcher.impl;
using strange.extensions.command.api;
using strange.extensions.command.impl;

public class TetrisContext : MVCSContext
{    

    public TetrisContext(MonoBehaviour view) : base(view)
    {
    }

    public TetrisContext(MonoBehaviour view, ContextStartupFlags flags) : base(view, flags)
    {
    }

    protected override void addCoreComponents()
    {
        base.addCoreComponents();
        injectionBinder.Unbind<ICommandBinder>();
        injectionBinder.Bind<ICommandBinder>().To<SignalCommandBinder>().ToSingleton();
    }

    override public IContext Start()
    {
        base.Start();
        StartSignal startSignal = injectionBinder.GetInstance<StartSignal>();
        startSignal.Dispatch();
        return this;
    }

    protected override void mapBindings()
    {
        injectionBinder.Bind<IGridModel>().To<GridModel>().ToSingleton();
        injectionBinder.Bind<IGameStateModel>().To<GameStateModel>().ToSingleton();
        injectionBinder.Bind<IBlockSetGenerator>().To<BlockSetGenerator>().ToSingleton();
        injectionBinder.Bind<ILocalDataService>().To<LocalDataService>().ToSingleton();

        commandBinder.Bind<StartSignal>().To<StartCommand>().Once();
        commandBinder.Bind<NewGameSignal>().To<NewGameCommand>();
        commandBinder.Bind<TryTouchBlockSignal>().To<TryTouchBlockCommand>();
        commandBinder.Bind<TryPlaceBlockSignal>().To<TryPlaceBlockCommand>();
        commandBinder.Bind<BlockPlacedSignal>().To<BlockPlacedCommand>();

        injectionBinder.Bind<NewGameReadySignal>().ToSingleton();
        injectionBinder.Bind<BlockTouchedSignal>().ToSingleton();
        injectionBinder.Bind<ElementsPlacedSignal>().ToSingleton();
        injectionBinder.Bind<AllPlacedSignal>().ToSingleton();
        injectionBinder.Bind<RowFilledSignal>().ToSingleton();

        mediationBinder.Bind<BoardView>().To<BoardMediator>();
        mediationBinder.Bind<MouseControlsView>().To<ControlsMediator>();
    }
}
