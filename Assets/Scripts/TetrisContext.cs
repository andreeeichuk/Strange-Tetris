using UnityEngine;
using strange.extensions.context.api;
using strange.extensions.context.impl;
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
        commandBinder.Bind<GameSetupSignal>().To<GameSetupCommand>();
        commandBinder.Bind<TryTouchBlockSignal>().To<TryTouchBlockCommand>();
        commandBinder.Bind<TryPlaceBlockSignal>().To<TryPlaceBlockCommand>();
        commandBinder.Bind<CheckMovesSignal>().To<CheckMovesCommand>();
        commandBinder.Bind<RestartGameSignal>().To<RestartGameCommand>();

        injectionBinder.Bind<NewGameSignal>().ToSingleton();
        injectionBinder.Bind<BlockTouchedSignal>().ToSingleton();
        injectionBinder.Bind<ElementsPlacedSignal>().ToSingleton();
        injectionBinder.Bind<AllPlacedSignal>().ToSingleton();
        injectionBinder.Bind<RowFilledSignal>().ToSingleton();
        injectionBinder.Bind<NoMovesSignal>().ToSingleton();
        injectionBinder.Bind<ResetViewsSignal>().ToSingleton();

        mediationBinder.Bind<BoardView>().To<BoardMediator>();
        mediationBinder.Bind<UIView>().To<UIMediator>();

        #if UNITY_EDITOR
            mediationBinder.Bind<MouseControlsView>().To<ControlsMediator>(); 
        #elif UNITY_ANDROID
            mediationBinder.Bind<TouchControlsView>().To<ControlsMediator>();
        #endif
    }
}
