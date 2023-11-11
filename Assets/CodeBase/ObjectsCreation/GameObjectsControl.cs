using UnityEngine;

namespace CodeBase.ObjectsCreation
{
    public class GameObjectsControl : MonoBehaviour
    {
        [SerializeField] private Transform _enabledObjectsParent; 
        [SerializeField] private Transform _disabledObjectsParent;

        public T InstantiateObj<T>(T prefab) where T: MonoBehaviour
        {
            return Instantiate(prefab, _enabledObjectsParent);
        }
        
        public GameObject InstantiateObj(GameObject prefab) 
        {
            return Instantiate(prefab, _enabledObjectsParent);
        }

        public void SetActive(GameObject obj, bool value)
        {
            obj.SetActive(value);
            obj.transform.SetParent(value ? _enabledObjectsParent : _disabledObjectsParent);
        }
    }
}
