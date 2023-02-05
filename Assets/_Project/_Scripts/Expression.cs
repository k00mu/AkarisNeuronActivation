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
                    StartCoroutine(PlayExpression(NegativeSprite));
                    AudioManager.Instance.PlaySFX("Negative");
                    NerveSystem.Instance.ReduceTimer(5f);
                    break;
                case NodeType.Destination:
                    StartCoroutine(PlayExpression(PositiveSprite));
                    AudioManager.Instance.PlaySFX("Positive");
                    NerveSystem.Instance.Score++;
                    NerveSystem.Instance.IncreaseTimer();
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
