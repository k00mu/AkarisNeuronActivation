using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GlobalGameJam
{
    public class Player : MonoBehaviour
    {
        public Vector2 Position { get; private set; }

        private void Start()
        {
            Position = UnitTest.Instance.GetRandomPositionFromNodePositionList();
            transform.position = Position;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                TryToMove(-1, 1);
            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                TryToMove(1, 1);
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                TryToMove(-1, -1);
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                TryToMove(1, -1);
            }
        }

        private void TryToMove(int x, int y)
        {
            Vector2 newPosition = new Vector2(Position.x + x * UnitTest.Instance.TravelDistance, Position.y + y * UnitTest.Instance.TravelDistance);
            Debug.Log(newPosition);

            if (!UnitTest.Instance.GetNodeAtPositionV1(Position).CanMoveTo(UnitTest.Instance.GetNodeAtPositionV1(newPosition)))
            {
                return;
            }

            Position = newPosition;
            transform.position = Position;

            // if (UnitTest.Instance.nodePositionList.Contains(newPosition))
            // {
            //     if (UnitTest.Instance.GetNodeAtPositionV2(newPosition).NeighborNodeList.Contains(UnitTest.Instance.GetNodeAtPositionV2(newPosition)))
            //     {
            //         
            //     }
            // }
        }
    }
}
