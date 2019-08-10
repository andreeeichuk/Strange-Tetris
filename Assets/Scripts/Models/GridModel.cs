using UnityEngine;

public class GridModel : IGridModel
{
    [Inject]
    public RowFilledSignal RowFilled { get; set; }

    private int width;
    private int height;
    private bool[,] cells;
    private int[] filledInRow;
    private Vector2 gridOrigin;
    private float gridStep;
    
    public void Create(int width, int height)
    {
        this.width = width;
        this.height = height;
        cells = new bool[width, height];
        filledInRow = new int[height];
    }

    public void SetOriginAndStep(Vector2 origin, float step)
    {
        gridOrigin = origin;
        gridStep = step;
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
                RowFilled.Dispatch(y);
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
            if(coordinates[i].x>width-1||coordinates[i].x<0||coordinates[i].y>height-1||coordinates[i].y<0)
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

    public Coordinate[] ConvertVectorsToCoordinates(Vector2[] vectors)
    {
        Coordinate[] coordinates = new Coordinate[vectors.Length];

        for (int i = 0; i < vectors.Length; i++)
        {
            Vector2 diff = vectors[i] - gridOrigin;
            int x = Mathf.RoundToInt(diff.x / gridStep);
            int y = Mathf.RoundToInt(diff.y / gridStep);
            coordinates[i] = new Coordinate(x, y);
        }

        return coordinates;
    }

    public bool CheckMovesForBlock(Coordinate[] coordinates)
    {
        int blockWidth = 0;
        int blockHeight = 0;

        for (int i = 0; i < coordinates.Length; i++)
        {
            if(coordinates[i].x>blockWidth)
            {
                blockWidth = coordinates[i].x;
            }

            if(coordinates[i].y>blockHeight)
            {
                blockHeight = coordinates[i].y;
            }
        }

        for (int i = 0; i < height - blockHeight; i++)
        {
            for (int j = 0; j < width - blockWidth; j++)
            {
                if (CheckSpot(coordinates, j, i))
                    return true;
            }
        }

        return false;
    }

    private bool CheckSpot(Coordinate[] coordinates, int offsetX, int offsetY)
    {        
        for (int k = 0; k < coordinates.Length; k++)
        {
            if (cells[coordinates[k].x + offsetX, coordinates[k].y + offsetY])
            {
                return false;
            }
        }

        
        Debug.Log($"First Move: {coordinates[0].x + offsetX}, {coordinates[0].y + offsetY}");
        

        return true;
        
    }
}
