using UnityEngine;

namespace CodeBase.PlayerCode
{
    public class UnitRotation
    {
        private Transform _transform;

        public UnitRotation(Transform transform)
        {
            _transform = transform;
        }
        
        public void RotationToMoveDirection(float x, float y, Camera camera)
        {
            var dir = DirectionRelativeCamera(x, y, camera);
            var newDirection = Vector3.RotateTowards(_transform.forward, dir, 10 * Time.deltaTime, 0.0f);
            _transform.rotation = Quaternion.LookRotation(newDirection);
        }
        
        private Vector3 DirectionRelativeCamera(float x, float y, Camera camera)
        {
            var dir  = new Vector3(x, 0, y);

            dir = camera.transform.TransformDirection(dir);

            return new Vector3(dir.x, 0, dir.z);
        }
    }
}
