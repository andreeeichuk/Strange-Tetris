using UnityEngine;

public interface ILocalDataService
{
    GameObject GetBlockByIndex(int index);
    int GetBlockTypesCount();
}
