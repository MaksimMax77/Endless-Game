using System.Collections.Generic;
using System.Linq;
using CodeBase.ChunkSystem;
using CodeBase.ObjectsCreation;
using CodeBase.Settings;
using UnityEngine;
using Zenject;

namespace CodeBase.ItemsCreation
{
    public class ChunkItemsControl
    {
        [Inject] private readonly GameObjectsControl _gameObjectsControl;
        [Inject] private ChunkItemsControlSettings _chunkItemsControlSettings;
        private readonly Dictionary<GameObject, List<GameObject>> _disabledObjects = new();
        private readonly Dictionary<Vector3, List<ItemContainer>> _itemsContainers = new();
        
        private GameObject[] _itemsPrefabs;
        private int _amountItemsOnChunk;
        private int _itemsAmountOnStart;

        [Inject]
        public void Init()
        {
            _itemsPrefabs = _chunkItemsControlSettings.ItemsPrefabs;
            _amountItemsOnChunk = _chunkItemsControlSettings.AmountItemsOnChunk;
            _itemsAmountOnStart = _chunkItemsControlSettings.ItemsAmountOnStart;

            InstallDictionaries();
            PrepareItems();
        }
        
        public void SetItems(Vector3 chunkPos, Chunk chunk)
        {
            if (TryReCreateBySavedData(chunkPos))
            {
                return;
            }

            PutRandomItemsOnNewPosition(chunkPos, chunk.CreateRandomPositions(_amountItemsOnChunk)); 
        }
        
        private void PutRandomItemsOnNewPosition(Vector3 chunkPos, IReadOnlyList<Vector3> positions)
        {
            var itemsContainers = new List<ItemContainer>();
            _itemsContainers.TryAdd(chunkPos, itemsContainers);
            
            for (var i = 0; i < _amountItemsOnChunk; ++i)
            {
                var index = Random.Range(0, _itemsPrefabs.Length);
                var item = GetObjectByIndex(index);
                item.transform.position = positions[i];

                var itemContainer = new ItemContainer(index, positions[i]);
                itemContainer.SetItem(item);
                itemsContainers.Add(itemContainer);
            }
        }

        private bool TryReCreateBySavedData(Vector3 chunkPos)
        {
            if (!_itemsContainers.TryGetValue(chunkPos, out var itemContainers))
            {
                return false;
            }

            for (int i = 0, len = itemContainers.Count; i < len; ++i)
            {
                var item = GetObjectByIndex(itemContainers[i].ItemsIndex);
                item.transform.position = itemContainers[i].Position;
                itemContainers[i].SetItem(item);
            }

            return true;
        }

        public void DisableChunkItems(Vector3 chunkPosition)
        {
            if (!_itemsContainers.TryGetValue(chunkPosition, out var itemsContainers))
            {
                return;
            }

            for (int i = 0, len = itemsContainers.Count; i < len; ++i)
            {
                var item = itemsContainers[i].Item;
                ReturnObjectToDisabled(item);
            }
        }
        
        private void InstallDictionaries()
        {
            for (int i = 0, len = _itemsPrefabs.Length; i < len; ++i)
            {
                _disabledObjects.Add(_itemsPrefabs[i], new List<GameObject>());
            }
        }
        
        private void PrepareItems()
        {
            for (int i = 0, len = _itemsPrefabs.Length; i < len; ++i)
            {
                if (!_disabledObjects.TryGetValue(_itemsPrefabs[i], out var items))
                {
                    return;
                }
                
                for (var j = 0; j < _itemsAmountOnStart; ++j)
                {
                    var item =  _gameObjectsControl.InstantiateObj(_itemsPrefabs[i]);
                    _gameObjectsControl.SetActive(item, false);
                    items.Add(item);
                }
            }
        }
        
        private GameObject GetObjectByIndex(int index)
        {
            var disabledItems = _disabledObjects.ElementAt(index).Value;

            GameObject item;
            
            if (disabledItems.Count == 0)
            {
                item = _gameObjectsControl.InstantiateObj(_itemsPrefabs[index]);
            }
            else
            {
                item = disabledItems[0];
                disabledItems.Remove(item);
            }
            
            _gameObjectsControl.SetActive(item, true);
            return item;
        }

        private void ReturnObjectToDisabled(GameObject item)
        {
            _disabledObjects.TryGetValue(item, out var disabledItemsList);
            _gameObjectsControl.SetActive(item, false);
            disabledItemsList?.Add(item);
        }
    }
}
