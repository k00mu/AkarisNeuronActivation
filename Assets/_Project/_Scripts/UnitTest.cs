using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GlobalGameJam
{
    public class UnitTest : MonoBehaviour
    {
        #region singleton
        private static UnitTest instance;
        public static UnitTest Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<UnitTest>();
                }
                return instance;
            }
        }
        #endregion

        [SerializeField] private Node nodePrefab;
        [SerializeField] private Line linePrefab;
        [SerializeField] private float minimumDistance = 5f;
        [SerializeField] private float travelDistance = 6f;
        public float TravelDistance { get { return travelDistance; } }

        private BoxCollider[] boxColliders;
        Node root;

        public List<Vector2> nodePositionList = new List<Vector2>();

        private Node destination;
        private List<Node> badNodes = new List<Node>();

        private void Awake()
        {
            #region singleton
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
            #endregion

            boxColliders = GetComponents<BoxCollider>();

            root = Instantiate(nodePrefab, Vector2.zero, Quaternion.identity, transform);
            root.Position = Vector2.zero;

            root.name = "Node " + root.Position;
            nodePositionList.Add(root.Position);

            GenerateNodeTree(root);
        }

        private void Start()
        {
            GenerateDestination();
            ResetBadNodes();
            GenerateBadNodes();
        }

        private void ResetBadNodes()
        {
            foreach (Node badNode in badNodes)
            {
                badNode.SetType(Node.NodeType.Normal);
            }
        }

        private List<Node> GenerateBadNodes()
        {
            List<Node> badNodes = new List<Node>();

            for (int i = 0; i < 10; i++)
            {
                Vector2 index = GetRandomPositionFromNodePositionList();
                Node badNode = GetNodeAtPositionV2(index);
                badNode.SetType(Node.NodeType.Bad);
                badNode.name = "Node " + badNode.Position + " (Bad)";
                badNodes.Add(badNode);
            }

            return badNodes;
        }

        private Node GenerateDestination()
        {
            Vector2 index = GetRandomPositionFromNodePositionList();
            // Node destinationNodeV1 = GetNodeAtPosition(index);
            if (destination) destination.SetType(Node.NodeType.Normal);

            destination = GetNodeAtPositionV2(index);
            destination.SetType(Node.NodeType.Destination);
            destination.name = "Node " + destination.Position + " (Destination)";
            return destination;
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

            // if (position.x < boxColliderComponent.bounds.min.x || position.x > boxColliderComponent.bounds.max.x ||
            //     position.y < boxColliderComponent.bounds.min.y || position.y > boxColliderComponent.bounds.max.y)
            // {
            //     return false;
            // }

            foreach (BoxCollider boxCollider in boxColliders)
            {
                if (boxCollider.bounds.Contains(position))
                {
                    return true;
                }
            }

            return false;
        }

        private void GenerateNodeTree(Node node)
        {
            if (nodePositionList.Count >= 500) return;

            for (int i = 0; i < 4; i++)
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



        public Vector2 GetRandomSpawnPosition(Vector2 position)
        {
            int dir = Random.Range(0, 4);

            if (dir == 0)
            {
                // return position + new Vector2(-1, 1) * Random.Range(minimumDistance, maximumDistance);
                return position + new Vector2(-1, 1) * travelDistance;
            }
            else if (dir == 1)
            {
                // return position + new Vector2(1, 1) * Random.Range(minimumDistance, maximumDistance);
                return position + new Vector2(1, 1) * travelDistance;
            }
            else if (dir == 2)
            {
                // return position + new Vector2(-1, -1) * Random.Range(minimumDistance, maximumDistance);
                return position + new Vector2(-1, -1) * travelDistance;
            }
            else if (dir == 3)
            {
                // return position + new Vector2(1, -1) * Random.Range(minimumDistance, maximumDistance);
                return position + new Vector2(1, -1) * travelDistance;
            }
            else
            {
                return Vector2.zero;
            }
        }

        public Node GetNodeAtPositionV1(Vector2 position)
        {
            return GameObject.Find("Node " + position).GetComponent<Node>();
        }

        public Node GetNodeAtPositionV2(Vector2 position)
        {
            return Physics.OverlapSphere(position, minimumDistance / 2)[0].GetComponent<Node>();
        }

        public Vector2 GetRandomPositionFromNodePositionList()
        {
            return nodePositionList[Random.Range(0, nodePositionList.Count)];
        }
    }
}
