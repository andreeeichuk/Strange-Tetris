public interface IGameStateModel
{
    void CreateSlots(int count);
    void ResetSlots();
    void FillSlots(Coordinate[][] coordinates);
    void FreeSlot(int index);
}
