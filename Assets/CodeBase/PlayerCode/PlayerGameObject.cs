using System;
using CodeBase.ChunkSystem;
using UnityEngine;

namespace CodeBase.PlayerCode
{
    public class PlayerGameObject : MonoBehaviour
    {
        public event Action<Chunk> EnterOnChunk;
        private void OnTriggerEnter(Collider other)
        {
            var chunk = other.gameObject.GetComponent<Chunk>();

            if (chunk == null)
            {
                return;
            }
            EnterOnChunk?.Invoke(chunk);
        }
    }
}
