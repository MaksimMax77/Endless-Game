using UnityEngine;

namespace CodeBase.Settings
{
    [CreateAssetMenu(menuName = "ChunksOnPathCreatorSettings", fileName = "ChunksOnPathCreatorSettings")]
    public class ChunksOnPathCreatorSettings : ScriptableObject
    {
        [SerializeField] private float _chunkEdgeOffset;        
        [SerializeField] private float _prefabXScale;
        [SerializeField] private float _prefabZScale;

        public float ChunkEdgeOffset => _chunkEdgeOffset;
        public float PrefabXScale => _prefabXScale;
        public float PrefabZScale => _prefabZScale;
    }
}
