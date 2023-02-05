using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Akari
{
    public class ComicManager : MonoBehaviour
    {
        [SerializeField] private List<Sprite> comicPanels;
        [SerializeField] private int duration;
        private SpriteRenderer spriteRenderer;
        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
        public IEnumerator SlideshowComic()
        {
            foreach(var s in comicPanels)
            {
                spriteRenderer.sprite = s;
                yield return new WaitForSeconds(duration);
            }
        }
    }
}
