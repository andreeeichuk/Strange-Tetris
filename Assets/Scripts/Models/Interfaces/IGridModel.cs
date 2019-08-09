public interface IGridModel
{
    void Create(int x, int y);
    bool IsCellFilled(int x, int y);
    void SetCell(int x, int y, bool isFilled);
}
