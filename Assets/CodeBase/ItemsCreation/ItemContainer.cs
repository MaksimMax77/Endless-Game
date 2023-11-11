using UnityEngine;

namespace CodeBase.ItemsCreation
{
    public class ItemContainer 
    {
        public int ItemsIndex
        {
            get;
            private set;
        }

        public Vector3 Position
        {
            get;
            private set;
        }
        public GameObject Item
        {
            get;
            private set;
        }

        public ItemContainer(int itemsIndex, Vector3 position)
        {
            ItemsIndex = itemsIndex;
            Position = position;
        }

        public void SetItem(GameObject item)
        {
            Item = item;
        }
    }
}
