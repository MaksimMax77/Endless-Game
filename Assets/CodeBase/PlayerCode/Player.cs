using System;
using CodeBase.Animations;
using CodeBase.Cameras;
using CodeBase.ChunkSystem;
using CodeBase.UserInput;
using UnityEngine;
using Zenject;

namespace CodeBase.PlayerCode
{
    public class Player : IDisposable
    {
        [Inject] private PlayerInput _playerInput;
        [Inject] private PlayerGameObject _playerGameObject;
        private UnitRotation _unitRotation;
        private AnimationsControl _animationsControl;
        private Camera _camera;
   
        public Chunk CurrentChunk { get; private set; }
        
        [Inject]
        public void Init(UnitCamera unitCamera)
        {
            _camera = unitCamera.Camera;
            _animationsControl = _playerGameObject.GetComponent<AnimationsControl>();
            _unitRotation = new UnitRotation(_playerGameObject.transform);
            
            _playerGameObject.EnterOnChunk += OnEnterOnChunk;
            _playerInput.MoveInput += OnMoveInput;
        }
        
        public void Dispose()
        {
            _playerGameObject.EnterOnChunk -= OnEnterOnChunk;
            _playerInput.MoveInput -= OnMoveInput;
        }
        
        public Vector3 GetPlayerPosOnChunk()
        {
            return CurrentChunk.transform.InverseTransformPoint(_playerGameObject.transform.position);
        }

        private void OnEnterOnChunk(Chunk chunk)
        {
            CurrentChunk = chunk;
        }

        private void OnMoveInput(Vector3 inputDirection)
        {
            /*if (_animationsControl == null)//TODO убрать 
            {
                _playerGameObject.transform.position += inputDirection * 10 * Time.deltaTime; 
                return;
            }*/
            
            _animationsControl.AnimateMove(inputDirection.x, inputDirection.z);
            _unitRotation.RotationToMoveDirection(inputDirection.x, inputDirection.z, _camera);
        }
    }
}
