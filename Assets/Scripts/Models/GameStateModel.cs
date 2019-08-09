public class GameStateModel : IGameStateModel
{
    public int FreeBlockSlots { get; private set; }

    public void CreateFreeSlots(int count)
    {
        FreeBlockSlots = count;
    }

    public void FreeOneSlot()
    {
        FreeBlockSlots--;
        if(FreeBlockSlots==0)
        {
            // fire signal that we need next batch
        }
    }
}
