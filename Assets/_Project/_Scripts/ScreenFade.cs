using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Akari
{
    public enum FadeColor
    {
        Black,
        White
    }



    /// <summary>
    /// Fade screen to black.
    /// </summary>
    public class ScreenFade : MonoBehaviour
    {
        //==============================================================================
        // Variables
        //==============================================================================
        private static Image imageComponent;
        private static CanvasGroup canvasGroupComponent;
        public static bool IsFading = false;



        //==============================================================================
        // Functions
        //==============================================================================
        #region MonoBehaviour Method
        private void Start()
        {
            imageComponent = gameObject.GetComponent<Image>();
            canvasGroupComponent = gameObject.GetComponent<CanvasGroup>();

            StartCoroutine(FadeOut(1f, FadeColor.Black));
        }
        #endregion



        #region JenangManis Method
        /// <summary>
        /// Fade screen in.
        /// </summary>
        /// <param name="fadeTime"></param>
        public static IEnumerator FadeOut(float fadeTime, FadeColor fadeColor)
        {
            IsFading = true;

            imageComponent.enabled = true;
            canvasGroupComponent.alpha = 1;
            canvasGroupComponent.interactable = true;
            canvasGroupComponent.blocksRaycasts = true;

            Color imageColor = ChangeColor(fadeColor);
            imageComponent.color = new Color(imageColor.r, imageColor.g, imageColor.b, 1);

            while (imageComponent.color.a > 0)
            {
                float fadeAmount = imageComponent.color.a - (fadeTime * Time.unscaledDeltaTime);

                imageColor.a = fadeAmount;
                imageComponent.color = imageColor;

                yield return null;
            }

            imageComponent.enabled = false;
            canvasGroupComponent.alpha = 0;
            canvasGroupComponent.interactable = false;
            canvasGroupComponent.blocksRaycasts = false;

            IsFading = false;
        }


        /// <summary>
        /// Fade screen out
        /// </summary>
        /// <param name="fadeTime"></param>
        public static IEnumerator FadeIn(float fadeTime, FadeColor fadeColor)
        {
            IsFading = true;

            imageComponent.enabled = true;
            canvasGroupComponent.alpha = 1;
            canvasGroupComponent.interactable = true;
            canvasGroupComponent.blocksRaycasts = true;

            Color imageColor = ChangeColor(fadeColor);
            imageComponent.color = new Color(imageColor.r, imageColor.g, imageColor.b, 0);

            while (imageComponent.color.a < 1)
            {
                float fadeAmount = imageComponent.color.a + (fadeTime * Time.unscaledDeltaTime);

                imageColor.a = fadeAmount;
                imageComponent.color = imageColor;

                yield return null;
            }

            IsFading = false;
        }


        private static Color ChangeColor(FadeColor fadeColor)
        {
            Color color = Color.black;

            switch (fadeColor)
            {
                case FadeColor.Black:
                    color = Color.black;
                    break;

                case FadeColor.White:
                    color = Color.white;
                    break;
            }

            return color;
        }
        #endregion
    }
}
