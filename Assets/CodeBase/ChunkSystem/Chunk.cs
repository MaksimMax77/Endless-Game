using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.ChunkSystem
{
    public class Chunk : MonoBehaviour
    {
        [SerializeField] private SquareZone _squareZone;
        
        //TODO подумать насколько оно тут нужно 
        public List<Vector3> CreateRandomPositions(int positionsAmount)
        {
            var positions = new List<Vector3>();
            
            for (var i = 0; i < positionsAmount; i++)
            {
                var uniquePosSet = false;

                while (!uniquePosSet)
                {
                    var randomPosInChunk = ReturnRandomPosInChunk();
                    
                    if (!positions.Contains(randomPosInChunk))
                    {
                        positions.Add(randomPosInChunk);
                        uniquePosSet = true;
                    }
                }
            }
            return positions;
        }
        
        private Vector3 ReturnRandomPosInChunk()
        {
            var pos = transform.position + _squareZone.Center + new Vector3(
                Random.Range(-_squareZone.SquareField.x / 2, _squareZone.SquareField.x / 2),
                Random.Range(-_squareZone.SquareField.y / 2, _squareZone.SquareField.y / 2),
                Random.Range(-_squareZone.SquareField.z / 2, _squareZone.SquareField.z / 2));
            return pos;
        }
    }
}
