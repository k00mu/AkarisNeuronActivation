using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GlobalGameJam
{
    public class UnitTest : MonoBehaviour
    {
        [SerializeField] private Transform nodePrefab;
        [SerializeField] private float minimumDistance = 5f;
        private Node root;

        public static List<Vector2> nodePositionList = new List<Vector2>();

        private void Awake()
        {
            root = new Node(Vector2.zero);
            nodePositionList.Add(root.position);

            while (nodePositionList.Count < 100)
            {
                Node currentNode = root;
                for (int i = 0; i < 100; i++)
                {

                    Vector2 neighborPosition = Random.insideUnitCircle * 10f;
                    int neighborCount = Random.Range(0, 3);
                    if (CanCreateNode(neighborPosition) || currentNode.GetNeighborCount() < neighborCount)
                    {
                        currentNode.AddNeighbor(new Node(neighborPosition));
                        nodePositionList.Add(neighborPosition);
                    }
                    else
                    {
                        break;
                    }
                }
                currentNode = root.GetNeighbor(Random.Range(0, root.GetNeighborCount()));
            }
        }

        private void Start()
        {
            // spawn a node prefab at each node
            SpawnNode(root);
        }

        private void SpawnNode(Node node)
        {
            Transform nodeTransform = Instantiate(nodePrefab, node.position, Quaternion.identity);
            nodeTransform.name = "Node " + node.position;
            for (int i = 0; i < node.GetNeighborCount(); i++)
            {
                SpawnNode(node.GetNeighbor(i));
            }
        }

        private void OnDrawGizmos()
        {
            if (root == null) return;

            Node currentNode = root;

            while (currentNode.GetParent() != null)
            {
                currentNode = currentNode.GetParent();
            }

            DrawGizmos(currentNode);
        }

        private void DrawGizmos(Node node)
        {
            node.DrawGizmos();
            for (int i = 0; i < node.GetNeighborCount(); i++)
            {
                DrawGizmos(node.GetNeighbor(i));
            }
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
    }
}
