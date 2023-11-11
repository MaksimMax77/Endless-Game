using CodeBase.GameLoop;
using CodeBase.PlayerCode;
using CodeBase.Settings;
using UnityEngine;

namespace CodeBase.ChunkSystem
{
    public class ChunksOnPathCreator: UpdateObject
    {
        private float _chunkEdgeOffset;
        private float _prefabXScale;
        private float _prefabZScale;
        private ChunksManager _chunksManager;
        private Player _player;

        public ChunksOnPathCreator(Player player, ChunksOnPathCreatorSettings chunksOnPathCreatorSettings, ChunksManager chunksManager,
            GlobalUpdate globalUpdate) : base(globalUpdate)
        {
            _player = player;
            
            _chunkEdgeOffset = chunksOnPathCreatorSettings.ChunkEdgeOffset;
            _prefabXScale = chunksOnPathCreatorSettings.PrefabXScale;
            _prefabZScale = chunksOnPathCreatorSettings.PrefabZScale;

            _chunksManager = chunksManager;
            _chunksManager.PutChunkAndItems(Vector3.zero);
        }

        public override void Update()
        {
            CreateChunkOnPlayerPath();
        }

        private void SetChunkOnDirection(Vector3 direction, float chunkScale)
        {
            var pos = _player.CurrentChunk.transform.position + direction * chunkScale;
            _chunksManager.PutChunkAndItems(pos);
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
