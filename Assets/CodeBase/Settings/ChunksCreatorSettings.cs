using CodeBase.ChunkSystem;
using UnityEngine;

namespace CodeBase.Settings
{
    [CreateAssetMenu(menuName = "ChunksManagerSettings", fileName = "ChunksManagerSettings")]
    public class ChunksCreatorSettings : ScriptableObject
    {
        [SerializeField] private Chunk chunkPrefab;
        [SerializeField] private int _chunksAmount;

        public Chunk ChunkPrefab => chunkPrefab;
        public int ChunksAmount => _chunksAmount;
    }
    //TODO сделать манагер настроек
}
