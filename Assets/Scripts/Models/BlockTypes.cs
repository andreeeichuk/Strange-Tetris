using UnityEngine;

[CreateAssetMenu(fileName = "BlockTypes", menuName = "Data/BlockTypes")]
public class BlockTypes : ScriptableObject, IBlockTypes
{
    public GameObject[] blocks { get { return _blocks; } }

    [SerializeField] private GameObject[] _blocks;
}
