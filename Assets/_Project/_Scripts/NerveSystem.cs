using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Akari
{
    public class NerveSystem : MonoBehaviour
    {
        #region singleton
        private static NerveSystem instance;
        public static NerveSystem Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<NerveSystem>();
                }
                return instance;
            }
        }
        #endregion

        #region events
        public Action<Node> onDestinationGenerated;
        #endregion

        [SerializeField] private Node nodePrefab;
        [SerializeField] private Line linePrefab;
        [SerializeField] private float minimumDistance = 5f;
        [SerializeField] private float travelDistance = 6f;
        public float TravelDistance { get { return travelDistance; } }

        public int Score;

        [SerializeField] private Timer timer;

        private BoxCollider[] boxColliders;
        private Node root;

        public bool GameOver { get; private set; }

        public List<Vector2> nodePositionList = new List<Vector2>();

        private Node destinationNode;
        private List<Node> badNodeList = new List<Node>();
        [SerializeField] private int badNodeCount = 5;
        private void Awake()
        {
            Score = 0;

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

        #region events
        public void NotifyDestinationGenerated(Node node)
        {
            onDestinationGenerated?.Invoke(node);
        }
        #endregion

        private void Start()
        {
            timer.StartTimer(30f);
            AudioManager.Instance.PlayBGM();

            Restart();
        }

        private void Update()
        {
            if (timer.TimeLeft <= 0)
            {
                timer.StopTimer();
                GameOver = true;
            }

            if (GameOver)
            {
                if (Input.GetKeyDown(KeyCode.R))
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
            }
        }

        public void Restart()
        {
            ResetBadNodes();
            GenerateBadNodes();

            Node newDestination = GenerateDestination();

            NotifyDestinationGenerated(newDestination);
        }

        private void ResetBadNodes()
        {
            foreach (Node badNode in badNodeList)
            {
                badNode.SetType(NodeType.Normal);
            }
        }

        public void IncreaseTimer()
        {
            timer.IncreaseTimer(CountTimer(GetNodeAtPositionV2(Player.Instance.Position), destinationNode));
        }

        public void IncreaseTimer(float amount)
        {
            timer.IncreaseTimer(amount);
        }

        public void ReduceTimer(float amount)
        {
            timer.ReduceTimer(amount);
        }

        private List<Node> GenerateBadNodes()
        {
            badNodeList = new List<Node>();

            for (int i = 0; i < badNodeCount; i++)
            {
                Vector2 index = GetRandomPositionFromNodePositionList();
                Node badNode = GetNodeAtPositionV2(index);
                if (badNode == destinationNode || badNode == null) continue;
                badNode.SetType(NodeType.Bad);
                badNode.name = "Node " + badNode.Position + " (Bad)";
                badNodeList.Add(badNode);
            }

            return badNodeList;
        }

        private Node GenerateDestination(bool retry = false)
        {
            Vector2 index = GetRandomPositionFromNodePositionList();
            // Node destinationNodeV1 = GetNodeAtPosition(index);
            if (destinationNode) destinationNode.SetType(NodeType.Normal);

            destinationNode = GetNodeAtPositionV2(index);

            if (destinationNode == null && !retry) GenerateDestination(true);

            else if (destinationNode == null && retry) return null;

            destinationNode.SetType(NodeType.Destination);

            destinationNode.name = "Node " + destinationNode.Position + " (Destination)";

            return destinationNode;
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
                    newNode.SetType(NodeType.Normal);
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
            int dir = UnityEngine.Random.Range(0, 4);

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

        public Node GetNodeAtPositionV2(Vector2 position)
        {
            Collider[] colliders = Physics.OverlapSphere(position, minimumDistance);
            if (colliders.Length > 0)
            {
                // return nearest node
                Node nearestNode = null;
                float nearestDistance = Mathf.Infinity;
                foreach (Collider collider in colliders)
                {
                    Node node = collider.GetComponent<Node>();
                    if (node)
                    {
                        float distance = Vector2.Distance(node.Position, position);
                        if (distance < nearestDistance)
                        {
                            nearestDistance = distance;
                            nearestNode = node;
                        }
                    }
                }
                return nearestNode;
            }
            return null;
        }

        public Vector2 GetRandomPositionFromNodePositionList()
        {
            return nodePositionList[UnityEngine.Random.Range(0, nodePositionList.Count)];
        }

        private int CountTimer(Node startNode, Node endNode)
        {
            Vector3 startPosition = startNode.Position;
            Vector3 endPosition = endNode.Position;

            float distance = Vector3.Distance(startPosition, endPosition);

            return Mathf.RoundToInt((distance * 2) / travelDistance);
        }
    }
}
