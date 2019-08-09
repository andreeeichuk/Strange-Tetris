public class GridModel : IGridModel
{
    private bool[,] cells;
    private int[] filledInRow;
    
    public void Create(int width, int height)
    {
        cells = new bool[width, height];
        filledInRow = new int[height];
    }

    public bool IsCellFilled(int x, int y)
    {
        return cells[x, y];
    }

    public void SetCell(int x, int y, bool isFilled)
    {
        cells[x, y] = isFilled;
        if (isFilled)
        {
            filledInRow[y]++;
            if(filledInRow[y]==cells.GetLength(0))
            {
                for (int i = 0; i < cells.GetLength(0); i++)
                {
                    SetCell(i, y, false);
                }
                // fire signal that row should be destroyed;
            }
        }
        else
        {
            filledInRow[y]--;
        }
    }
}
