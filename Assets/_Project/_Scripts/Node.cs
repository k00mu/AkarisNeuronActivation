using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GlobalGameJam
{
    public class Node
    {
        public Vector2 position;
        public Node parent;
        private List<Node> neighborNodeList;

        public Node(Vector2 position, Node parent = null)
        {
            this.position = position;
            this.parent = parent;
            neighborNodeList = new List<Node>();
        }

        public void AddNeighbor(Node node)
        {
            neighborNodeList.Add(node);
        }

        public void RemoveNeighbor(Node node)
        {
            neighborNodeList.Remove(node);
        }

        public Node GetNeighbor(int index)
        {
            return neighborNodeList[index];
        }

        public List<Node> GetNeighborList()
        {
            return neighborNodeList;
        }

        public int GetNeighborCount()
        {
            return neighborNodeList.Count;
        }

        public Node GetParent()
        {
            return parent;
        }

        public void DrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(position, 0.5f);
            Gizmos.color = Color.green;
            foreach (Node node in neighborNodeList)
            {
                Gizmos.DrawLine(position, node.position);
            }
        }
    }
}
