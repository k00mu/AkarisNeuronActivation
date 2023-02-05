using TMPro;
using UnityEngine;

namespace Akari
{
    public class ScoreHUD : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scoreText;

        private void Update()
        {
            scoreText.text = NerveSystem.Instance.Score.ToString();
        }
    }
}
