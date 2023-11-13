using System.Collections.Generic;
using System.Linq;
using CodeBase.ItemsCreation;
using CodeBase.ObjectsCreation;
using CodeBase.Settings;
using UnityEngine;
using Zenject;

namespace CodeBase.ChunkSystem
{
    public class ChunksCreator
    {
        [Inject] private ChunkItemsControl _chunkItemsControl;    
        [Inject] private GameObjectsControl _gameObjectsControl;
        [Inject] private ChunksCreatorSettings _chunksCreatorSettings;
        private int _chunksAmountOnScene;

        private Queue<Chunk> _disabledChunks = new();
        private List<Chunk> _enabledChunks = new();

        [Inject]
        public void Init()
        {
            _chunksAmountOnScene = _chunksCreatorSettings.ChunksAmount;
            PrepareChunks(_chunksCreatorSettings.ChunkPrefab);
        }
        
        public void PutChunkAndItems(Vector3 pos)
        {
            if (ChunkOnPositionIsSet(pos))
            {
                return;
            }
            
            var chunk = GetChunkAndRemoveItems();
            
            chunk.transform.position = pos;
            
            _chunkItemsControl.SetItems(pos, chunk);
        }

        private Chunk GetChunkAndRemoveItems()
        {
            var chunk = GetEnabledChunk();
            
            if (chunk == null)
            {
                chunk = EnableChunk();
            }
            else
            {
                _chunkItemsControl.DisableChunkItems(chunk.transform.position);
            }

            return chunk;
        }

        private Chunk GetEnabledChunk()
        {
            if (_disabledChunks.Count != 0)
            {
                return null;
            }

            var chunk = _enabledChunks[0];

            _enabledChunks.Remove(chunk);
            _enabledChunks.Add(chunk);

            return chunk;
        }

        private Chunk EnableChunk()
        {
            var chunk = _disabledChunks.Dequeue(); 
            _enabledChunks.Add(chunk);
            _gameObjectsControl.SetActive(chunk.gameObject, true);
            return chunk;
        }
        
        private void DisableChunk(Chunk chunk)
        {
            _disabledChunks.Enqueue(chunk);
            _enabledChunks.Remove(chunk);
            _gameObjectsControl.SetActive(chunk.gameObject, false);
        }
        
        private bool ChunkOnPositionIsSet(Vector3 pos)
        {
            return _enabledChunks.Count != 0 && _enabledChunks.Any(chunk => Equals(chunk.transform.position, pos));
        }

        private void PrepareChunks(Chunk chunkPrefab)
        {
            for (var i = 0; i < _chunksAmountOnScene; ++i)
            {
                var chunk = _gameObjectsControl.InstantiateObj(chunkPrefab);
                DisableChunk(chunk);
            }
        }
    }
}
