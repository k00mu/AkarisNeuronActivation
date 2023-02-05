using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Akari
{
    public class Expression : MonoBehaviour
    {
        [SerializeField] private Sprite NormalSprite;
        [SerializeField] private Sprite NegativeSprite;
        [SerializeField] private Sprite PositiveSprite;

        private SpriteRenderer spriteRenderer;

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void SetExpression(NodeType expression)
        {
            switch (expression)
            {
                case NodeType.Normal:
                    break;
                case NodeType.Bad:
                    Debug.Log("Awikwok");
                    StartCoroutine(PlayExpression(NegativeSprite));
                    break;
                case NodeType.Destination:
                    StartCoroutine(PlayExpression(PositiveSprite));
                    break;
                default:
                    break;
            }
        }

        private IEnumerator PlayExpression(Sprite sprite)
        {
            spriteRenderer.sprite = sprite;
            yield return new WaitForSeconds(2f);
            spriteRenderer.sprite = NormalSprite;
        }
    }
}
