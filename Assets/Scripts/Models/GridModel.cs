public class GridModel : IGridModel
{
    private bool[,] cells;
    
    public void Create(int height, int width)
    {
        cells = new bool[height, width];
    }

    public bool IsCellFilled(int x, int y)
    {
        return cells[x, y];
    }

    public void SetCell(int x, int y, bool isFilled)
    {
        cells[x, y] = isFilled;
    }
}
