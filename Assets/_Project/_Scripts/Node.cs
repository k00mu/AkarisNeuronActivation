using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Akari
{
    public class Node : MonoBehaviour
    {
        public Vector2 Position;
        public Node Parent;
        public List<Node> NeighborNodeList { get; private set; } = new List<Node>();

        public NodeType Type { get; private set; }

        [SerializeField] private Mesh normalMesh;
        [SerializeField] private Mesh badMesh;
        [SerializeField] private Mesh destinationMesh;
        private MeshFilter meshFilter;
        private MeshRenderer meshRenderer;

        public void SetType(NodeType type)
        {
            Type = type;
            if (meshFilter == null)
                meshFilter = GetComponent<MeshFilter>();
            if (meshRenderer == null)
                meshRenderer = GetComponent<MeshRenderer>();
            switch (Type)
            {
                case NodeType.Normal:
                    if (normalMesh) meshFilter.mesh = normalMesh;
                    meshRenderer.material.color = Color.white;
                    break;
                case NodeType.Bad:
                    if (badMesh) meshFilter.mesh = badMesh;
                    meshRenderer.material.color = Color.red;
                    break;
                case NodeType.Destination:
                    if (destinationMesh) meshFilter.mesh = destinationMesh;
                    meshRenderer.material.color = Color.green;
                    break;
                default:
                    break;
            }
        }

        public NodeType DoSomethingToPlayerBasedOnNodeType()
        {
            switch (Type)
            {
                case NodeType.Normal:
                    break;
                case NodeType.Bad:
                    Debug.Log("Bad node");
                    SetType(NodeType.Normal);
                    return NodeType.Bad;
                case NodeType.Destination:
                    Debug.Log("Destination node");
                    // add points

                    NerveSystem.Instance.Restart();
                    return NodeType.Destination;
                default:
                    break;
            }
            return Type;
        }

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
