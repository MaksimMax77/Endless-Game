using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.GameLoop
{
    public class GlobalUpdate : MonoBehaviour
    {
        private List<UpdateObject> _updateObjects = new List<UpdateObject>();

        public void Add(UpdateObject updateObject)
        {
            _updateObjects.Add(updateObject);
        }
        
        public void Remove(UpdateObject updateObject)
        {
            _updateObjects.Remove(updateObject);
        }
        
        private void Update()
        {
            for (int i = 0, len =  _updateObjects.Count; i < len; ++i)
            {
                _updateObjects[i].Update();
            }
        }
    }
}
