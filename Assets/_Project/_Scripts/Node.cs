using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GlobalGameJam
{
    public class Node : MonoBehaviour
    {
        public Vector2 Position;
        public Node Parent;
        public List<Node> NeighborNodeList { get; private set; } = new List<Node>();

        // public Node(Vector2 position, Node parent = null)
        // {
        //     this.Position = position;
        //     this.parent = parent;
        //     neighborNodeList = new List<Node>();
        // }

        public void AddNeighbor(Node node)
        {
            NeighborNodeList.Add(node);
        }

        public void RemoveNeighbor(Node node)
        {
            NeighborNodeList.Remove(node);
        }

        public Node GetNeighbor(int index)
        {
            return NeighborNodeList[index];
        }

        public int GetNeighborCount()
        {
            return NeighborNodeList.Count;
        }

        public bool CanMoveTo(Node node)
        {
            return NeighborNodeList.Contains(node) || node.NeighborNodeList.Contains(this);
        }

        public void DrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(Position, 0.5f);
            Gizmos.color = Color.green;
            foreach (Node node in NeighborNodeList)
            {
                Gizmos.DrawLine(Position, node.Position);
            }
        }


    }
}