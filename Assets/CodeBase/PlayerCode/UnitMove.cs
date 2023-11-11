using UnityEngine;

namespace CodeBase.PlayerCode
{
    public class UnitMove
    {
        private float _speed;
        private Transform _unitTransform;

        public UnitMove(float speed, Transform transform)
        {
            _speed = speed;
            _unitTransform = transform;
        }

        public void UpdatePosition(Vector3 dir)
        {
            _unitTransform.position += dir * _speed * Time.deltaTime;
        }
    }
}
