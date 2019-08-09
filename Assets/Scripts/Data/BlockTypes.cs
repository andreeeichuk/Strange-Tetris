using UnityEngine;

[CreateAssetMenu(fileName = "BlockTypes", menuName = "Data/BlockTypes")]
public class BlockTypes : ScriptableObject
{
    public GameObject[] blocks { get { return _blocks; } }

    [SerializeField] private GameObject[] _blocks;
}
