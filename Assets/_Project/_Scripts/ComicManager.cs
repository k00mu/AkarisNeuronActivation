using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Akari
{
    public class ComicManager : MonoBehaviour
    {
        [SerializeField] private List<Sprite> comicPanels;
        [SerializeField] private int duration;
        private Image imageComponent;
        private void Awake()
        {
            imageComponent = GetComponent<Image>();
        }

        private void Start()
        {
            StartCoroutine(SlideshowComic());
        }

        public IEnumerator SlideshowComic()
        {
            foreach (var s in comicPanels)
            {
                ScreenFade.FadeIn(0.5f, FadeColor.Black);
                yield return new WaitForSeconds(0.5f);
                ScreenFade.FadeOut(0.5f, FadeColor.Black);

                imageComponent.sprite = s;
                yield return new WaitForSeconds(duration);
            }

            ScreenFade.FadeIn(2f, FadeColor.Black);

            Camera mainCamera = Camera.main;
            AudioSource cameraAudioSource = mainCamera.GetComponent<AudioSource>();
            while (cameraAudioSource.volume > 0)
            {
                cameraAudioSource.volume -= 0.1f;
                yield return new WaitForSeconds(0.05f);
            }

            SceneManager.LoadScene(1);
        }
    }
}
