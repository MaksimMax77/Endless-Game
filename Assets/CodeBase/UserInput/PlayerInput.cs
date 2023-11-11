using System;
using CodeBase.GameLoop;
using UnityEngine;

namespace CodeBase.UserInput
{
    public class PlayerInput: UpdateObject
    {
        public event Action<Vector3> MoveInput; 
        
        public PlayerInput(GlobalUpdate globalUpdate) : base(globalUpdate)
        {
        }
        
        public override void Update()
        {
            var horizontal = Input.GetAxisRaw("Horizontal");
            var vertical = Input.GetAxisRaw("Vertical");

            MoveInput?.Invoke(new Vector3(horizontal, 0, vertical));
        }
    }
}
