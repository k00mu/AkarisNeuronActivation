using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GlobalGameJam
{
    public class UnitTest : MonoBehaviour
    {
        [SerializeField] private Node nodePrefab;
        [SerializeField] private Line linePrefab;
        [SerializeField] private float minimumDistance = 5f;
        // [SerializeField] private float maximumDistance = 15f;

        public List<Vector2> nodePositionList = new List<Vector2>();

        private void Awake()
        {
            Node root = Instantiate(nodePrefab, Vector2.zero, Quaternion.identity, transform);
            root.Position = Vector2.zero;

            root.name = "Node " + root.Position;
            nodePositionList.Add(root.Position);

            GenerateNodeTree(root);
        }

        private bool CanCreateNode(Vector2 position)
        {
            if (nodePositionList.Count == 0) return true;

            foreach (Vector2 nodePosition in nodePositionList)
            {
                if (Vector2.Distance(position, nodePosition) < minimumDistance)
                {
                    return false;
                }
            }

            return true;
        }

        private void GenerateNodeTree(Node node)
        {
            if (nodePositionList.Count >= 300) return;

            for (int i = 0; i < 5; i++)
            {
                Vector2 position = GetRandomSpawnPosition(node.Position);
                if (CanCreateNode(position))
                {
                    Node newNode = Instantiate(nodePrefab, position, Quaternion.identity, transform);
                    newNode.Position = position;
                    newNode.name = "Node " + newNode.Position;
                    nodePositionList.Add(newNode.Position);
                    node.AddNeighbor(newNode);
                    newNode.Parent = node;

                    Line newLine = Instantiate(linePrefab, node.Position, Quaternion.identity, node.transform);
                    newLine.SetPosition(node.transform, newNode.transform);

                    GenerateNodeTree(newNode);
                }
            }
        }

        private void Start()
        {
            // spawn a node prefab at each node
            // SpawnNode(root);

            // draw a line between each node and its parent
            // SpawnLine(root);
        }

        // private void SpawnNode(Node node)
        // {
        //     Transform nodeTransform = Instantiate(nodePrefab, node.Position, Quaternion.identity, transform);
        //     nodeTransform.name = "Node " + node.Position;
        //     for (int i = 0; i < node.GetNeighborCount(); i++)
        //     {
        //         SpawnLine(node, node.Position, node.GetNeighbor(i).Position);
        //         SpawnNode(node.GetNeighbor(i));
        //     }
        // }

        // // private void MakeLine(Vector2 startPosition, Vector2 endPosition)
        // // {
        // //     Transform lineTransform = Instantiate(linePrefab, Vector3.zero, Quaternion.identity, transform);
        // //     LineRenderer lineRenderer = lineTransform.GetComponent<LineRenderer>();
        // //     lineRenderer.SetPosition(0, startPosition);
        // //     lineRenderer.SetPosition(1, endPosition);
        // // }
        // private void OnDrawGizmos()
        // {
        //     if (root == null) return;

        //     Node currentNode = root;

        //     DrawGizmos(currentNode);
        // }

        // private void DrawGizmos(Node node)
        // {
        //     node.DrawGizmos();
        //     for (int i = 0; i < node.GetNeighborCount(); i++)
        //     {
        //         DrawGizmos(node.GetNeighbor(i));
        //     }
        // }



        private Vector2 GetRandomSpawnPosition(Vector2 position)
        {
            int dir = Random.Range(0, 4);

            if (dir == 0)
            {
                // return position + new Vector2(-1, 1) * Random.Range(minimumDistance, maximumDistance);
                return position + new Vector2(-1, 1) * 15;
            }
            else if (dir == 1)
            {
                // return position + new Vector2(1, 1) * Random.Range(minimumDistance, maximumDistance);
                return position + new Vector2(1, 1) * 15;
            }
            else if (dir == 2)
            {
                // return position + new Vector2(-1, -1) * Random.Range(minimumDistance, maximumDistance);
                return position + new Vector2(-1, -1) * 15;
            }
            else if (dir == 3)
            {
                // return position + new Vector2(1, -1) * Random.Range(minimumDistance, maximumDistance);
                return position + new Vector2(1, -1) * 15;
            }
            else
            {
                return Vector2.zero;
            }
        }

        // private Node GetNodeAtPosition(Vector2 position)
        // {
        //     if (root == null)
        //     {
        //         return null;
        //     }

        //     Node currentNode = root;

        //     return CheckNodePosition(currentNode, position);
        // }

        // private Node CheckNodePosition(Node node, Vector2 position)
        // {
        //     if (node.Position == position)
        //     {
        //         return node;
        //     }

        //     for (int i = 0; i < node.GetNeighborCount(); i++)
        //     {
        //         CheckNodePosition(node.GetNeighbor(i), position);
        //     }

        //     return null;
        // }

        // private Vector2 GetRandomPositionFromNodePositionList()
        // {
        //     return nodePositionList[Random.Range(0, nodePositionList.Count)];
        // }

        // public void SpawnLine(Transform start, Transform end)
        // {
        //     LineRenderer lineRenderer = start.GetComponent<LineRenderer>();
        //     lineRenderer.SetPosition(0, start.position);
        //     lineRenderer.SetPosition(1, end.position);
        // }
    }
}
