using CodeBase.GameLoop;
using CodeBase.PlayerCode;
using CodeBase.Settings;
using UnityEngine;
using Zenject;

namespace CodeBase.ChunkSystem
{
    public class ChunksOnPathCreator: UpdateObject
    {
        [Inject] private ChunksCreator _chunksCreator;
        [Inject] private Player _player;
        [Inject] private ChunksOnPathCreatorSettings _chunksOnPathCreatorSettings;
        private float _chunkEdgeOffset;
        private float _prefabXScale;
        private float _prefabZScale;
        
        public ChunksOnPathCreator(GlobalUpdate globalUpdate) : base(globalUpdate)
        {
        }

        [Inject]
        public void Init()
        {
            _chunkEdgeOffset = _chunksOnPathCreatorSettings.ChunkEdgeOffset;
            _prefabXScale = _chunksOnPathCreatorSettings.PrefabXScale;
            _prefabZScale = _chunksOnPathCreatorSettings.PrefabZScale;
            _chunksCreator.PutChunkAndItems(Vector3.zero);
        }
        
        public override void Update()
        {
            CreateChunkOnPlayerPath();
        }

        private void SetChunkOnDirection(Vector3 direction, float chunkScale)
        {
            var pos = _player.CurrentChunk.transform.position + direction * chunkScale;
            _chunksCreator.PutChunkAndItems(pos);
        }

        private void CreateChunkOnPlayerPath()
        {
            var playerPos = _player.GetPlayerPosOnChunk();
            var distanceToChunkEdge = 0.50f - _chunkEdgeOffset;

            var playerChunkForward = _player.CurrentChunk.transform.forward;
            var playerChunkRight = _player.CurrentChunk.transform.right;
            
            if (playerPos.z > distanceToChunkEdge)
            {
                SetChunkOnDirection(playerChunkForward, _prefabZScale);
            }
            
            if (playerPos.z < -distanceToChunkEdge)
            {
                SetChunkOnDirection(playerChunkForward, -_prefabZScale);
            }
            
            if (playerPos.x > distanceToChunkEdge)
            {
                SetChunkOnDirection(playerChunkRight, _prefabXScale);
            }

            if (playerPos.x < -distanceToChunkEdge)
            {
                SetChunkOnDirection(playerChunkRight, -_prefabXScale);
            }
        }
    }
}
