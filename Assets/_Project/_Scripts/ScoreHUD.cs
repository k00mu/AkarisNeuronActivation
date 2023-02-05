using TMPro;
using UnityEngine;

namespace Akari
{
    public class ScoreHUD : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scoreText;

        private void Update()
        {
            if(NerveSystem.Instance.Score != null)
                scoreText.text = NerveSystem.Instance.Score.ToString();
        }
    }
}
