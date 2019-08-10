public interface IGameStateModel
{
    void CreateSlots(int count);
    void FillSlots(Coordinate[][] coordinates);
    void FreeSlot(int index);
}
