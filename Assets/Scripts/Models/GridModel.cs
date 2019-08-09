public class GridModel : IGridModel
{
    private int width;
    private int height;
    private bool[,] cells;
    private int[] filledInRow;
    
    public void Create(int width, int height)
    {
        this.width = width;
        this.height = height;
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
            if(filledInRow[y] == width)
            {
                for (int i = 0; i < width; i++)
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

    public bool TryPlaceBlock(Coordinate[] coordinates)
    {
        bool success = true;
        for (int i = 0; i < coordinates.Length; i++)
        {
            if(coordinates[i].x>width||coordinates[i].x<0||coordinates[i].y>height||coordinates[i].y<0)
            {
                success = false;
                break;
            }
            else
            {
                if(cells[coordinates[i].x,coordinates[i].y])
                {
                    success = false;
                    break;
                }                
            }
        }

        return success;
    }

    public void PlaceBlock(Coordinate[] coordinates)
    {
        for (int i = 0; i < coordinates.Length; i++)
        {
            SetCell(coordinates[i].x, coordinates[i].y, true);
        }
    }
}
