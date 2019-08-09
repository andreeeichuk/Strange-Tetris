using UnityEngine;

public interface IGridModel
{
    void Create(int x, int y);
    void SetOriginAndStep(Vector2 origin, float step);
    bool IsCellFilled(int x, int y);
    void SetCell(int x, int y, bool isFilled);
    bool TryPlaceBlock(Coordinate[] coordinates);
    void PlaceBlock(Coordinate[] coordinates);
    Coordinate[] ConvertVectorsToCoordinates(Vector2[] vectors);
}
