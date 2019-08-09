public class GameStateModel : IGameStateModel
{
    [Inject]
    public AllPlacedSignal AllPlacedSignal { get; set; }

    public int FreeBlockSlots { get; private set; }

    private int count;

    public void CreateFreeSlots(int count)
    {
        this.count = count;
        FreeBlockSlots = count;
    }

    public void FreeOneSlot()
    {
        FreeBlockSlots--;
        if(FreeBlockSlots==0)
        {
            AllPlacedSignal.Dispatch();
            CreateFreeSlots(count);
        }
    }
}
