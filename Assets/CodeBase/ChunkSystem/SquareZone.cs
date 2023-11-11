using UnityEngine;

namespace CodeBase.ChunkSystem
{
    public class SquareZone : MonoBehaviour
    {
        [SerializeField] private Vector3 _squareField;
        [SerializeField] private Vector3 _center;
        public Vector3 SquareField => _squareField;
        public Vector3 Center => _center;
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = new Color(1, 0.2f, 0, 0.5f);
            Gizmos.DrawCube(transform.localPosition + _center, _squareField);
        }
    }
}
