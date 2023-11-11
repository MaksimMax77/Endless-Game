using UnityEngine;

namespace CodeBase.Settings
{
    [CreateAssetMenu(menuName = "ChunkItemsControlSettings", fileName = "ChunkItemsControlSettings")]
    public class ChunkItemsControlSettings: ScriptableObject
    {
        [SerializeField] private GameObject[] _itemsPrefabs;
        [SerializeField] private int _amountItemsOnChunk;
        [SerializeField] private int _itemsAmountOnStart;

        public GameObject[] ItemsPrefabs => _itemsPrefabs;
        public int AmountItemsOnChunk => _amountItemsOnChunk;
        public int ItemsAmountOnStart => _itemsAmountOnStart;
    }
}
