using System.Collections.Generic;
using System.Linq;
using CodeBase.ItemsCreation;
using CodeBase.ObjectsCreation;
using CodeBase.Settings;
using UnityEngine;

namespace CodeBase.ChunkSystem
{
    public class ChunksManager
    {
        private ChunkItemsControl _chunkItemsControl;
        private int _chunksAmount;
        
        private Queue<Chunk> _enabledChunks = new();
        private Queue<Chunk> _disabledChunks = new();

        private GameObjectsControl _gameObjectsControl;
        
        private ChunksManager(ChunksManagerSettings chunksManagerSettings, ChunkItemsControl chunkItemsControl, GameObjectsControl gameObjectsControl)
        {
            _chunksAmount = chunksManagerSettings.ChunksAmount;
            _chunkItemsControl = chunkItemsControl;
            _gameObjectsControl = gameObjectsControl;
            PrepareChunks(chunksManagerSettings.ChunkPrefab);
        }

        public void PutChunkAndItems(Vector3 pos)
        {
            if (ChunkOnPositionIsSet(pos))
            {
                return;
            }
            
            var chunk = GetObject();
            _gameObjectsControl.SetActive(chunk.gameObject, true);
            chunk.transform.position = pos;
            
            _chunkItemsControl.SetItems(pos, chunk);
            RemoveChunkAndItems();
        }

        private void RemoveChunkAndItems()
        {
            if (_enabledChunks.Count != _chunksAmount)
            {
                return;
            }
            
            var removedChunk = SwitchList(_enabledChunks, _disabledChunks);//TODO сделать список объектов для  удаления, которые будут добаваляться при наступлении игрока на них, чтоб удалять самый делекий от игрока чанк, чтоб починить бан 
            _gameObjectsControl.SetActive(removedChunk.gameObject, false);
            _chunkItemsControl.DisableChunkItems(removedChunk.transform.position);
        }

        private Chunk GetObject()
        {
            return SwitchList(_disabledChunks, _enabledChunks);
        }
        
        private bool ChunkOnPositionIsSet(Vector3 pos)
        {
            return _enabledChunks.Count != 0 && _enabledChunks.Any(chunk => Equals(chunk.transform.position, pos));
        }

        private void PrepareChunks(Chunk chunkPrefab)
        {
            for (var i = 0; i < _chunksAmount; ++i)
            {
                var chunk = _gameObjectsControl.InstantiateObj(chunkPrefab);
                _gameObjectsControl.SetActive(chunk.gameObject, false);
                _disabledChunks.Enqueue(chunk);
            }
        }

        private Chunk SwitchList(Queue<Chunk> oldList, Queue<Chunk> newList)
        {
            var obj = oldList.Dequeue();
            newList.Enqueue(obj);
            return obj;
        }
    }
}
