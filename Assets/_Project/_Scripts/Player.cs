using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Akari
{
    public class Player : MonoBehaviour
    {
        #region singleton
        private static Player instance;
        public static Player Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<Player>();
                }
                return instance;
            }
        }
        #endregion

        public Vector2 Position { get; private set; }

        private Expression akariExpression;

        [SerializeField] private GameObject dustFX;

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


            akariExpression = GetComponentInChildren<Expression>();
        }

        private void Start()
        {
            Position = NerveSystem.Instance.GetRandomPositionFromNodePositionList();
            transform.position = Position;
        }

        private void Update()
        {
            if (NerveSystem.Instance.GameOver)
            {
                if (Input.GetKeyDown(KeyCode.R))
                {
                    NerveSystem.Instance.Restart();
                }
                return;
            }

            if (Input.GetMouseButtonDown(0))
            {
                TryToMove();
            }

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

        // TODO: get mouse position and convert it to one of the 4 directions
        private void TryToMove()
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = mousePosition - Position;
            direction.Normalize();

            if (direction.x > 0 && direction.y > 0)
            {
                TryToMove(1, 1);
            }
            else if (direction.x > 0 && direction.y < 0)
            {
                TryToMove(1, -1);
            }
            else if (direction.x < 0 && direction.y > 0)
            {
                TryToMove(-1, 1);
            }
            else if (direction.x < 0 && direction.y < 0)
            {
                TryToMove(-1, -1);
            }
        }


        private void TryToMove(int x, int y)
        {
            Vector2 newPosition = new Vector2(Position.x + x * NerveSystem.Instance.TravelDistance, Position.y + y * NerveSystem.Instance.TravelDistance);
            //Debug.Log(newPosition);

            Node nextNode = NerveSystem.Instance.GetNodeAtPositionV2(newPosition);
            Node currentNode = NerveSystem.Instance.GetNodeAtPositionV2(Position);
            if (nextNode == null || currentNode == null || !currentNode.CanMoveTo(nextNode))
            {
                if (nextNode == null)
                {
                    Debug.Log("nextNode is null");
                }

                if (currentNode == null)
                {
                    Debug.Log("currentNode is null");
                    return;
                }

                if (!currentNode.CanMoveTo(nextNode))
                {
                    Debug.Log("currentNode can't move to nextNode");
                }

                return;
            }

            if (dustFX != null)
            {
                Vector2 dir = newPosition - Position;
                GameObject dust = Instantiate(dustFX, Position, Quaternion.identity);
                StartCoroutine(DestroyDust(dust, 10));
                Debug.Log(dir + " Angle " + AngleBetweenVector2(Vector2.right, dir));
                dust.transform.RotateAround(Position, Vector3.forward, AngleBetweenVector2(Vector2.right, dir));

            }

            Position = newPosition;
            transform.position = Position;

            AudioManager.Instance.PlaySFX("Move");

            NodeType expression = nextNode.DoSomethingToPlayerBasedOnNodeType();

            akariExpression.SetExpression(expression);
        }

        private IEnumerator DestroyDust(GameObject dust, int afterSecond)
        {
            yield return new WaitForSeconds(afterSecond);
            Destroy(dust);
        }

        float AngleBetweenVector2(Vector2 vec1, Vector2 vec2)
        {
            Vector2 vec1Rotated90 = new Vector2(-vec1.y, vec1.x);
            float sign = (Vector2.Dot(vec1Rotated90, vec2) < 0) ? -1.0f : 1.0f;
            return Vector2.Angle(vec1, vec2) * sign;
        }
    }
}
