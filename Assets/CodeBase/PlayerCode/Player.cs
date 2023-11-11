using System;
using CodeBase.Animations;
using CodeBase.Cameras;
using CodeBase.ChunkSystem;
using CodeBase.UserInput;
using UnityEngine;

namespace CodeBase.PlayerCode
{
    public class Player : IDisposable
    {
        private PlayerInput _playerInput;
        private PlayerGameObject _playerGameObject;
        private UnitRotation _unitRotation;
        private AnimationsControl _animationsControl;
        private Camera _camera;
   
        public Chunk CurrentChunk { get; private set; }
        
        private Player(PlayerInput playerInput, 
            PlayerGameObject playerGameObject, UnitCamera unitCamera)
        {
            _playerInput = playerInput;
            _playerGameObject = playerGameObject;
            _camera = unitCamera.Camera;
            _animationsControl = playerGameObject.GetComponent<AnimationsControl>();

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
            _animationsControl.AnimateMove(inputDirection.x, inputDirection.z);
            _unitRotation.RotationToMoveDirection(inputDirection.x, inputDirection.z, _camera);
        }
    }
}
