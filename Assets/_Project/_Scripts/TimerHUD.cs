using TMPro;
using UnityEngine;

namespace Akari
{
    public class TimerHUD : MonoBehaviour
    {
        [SerializeField] Timer timer;
        [SerializeField] TextMeshProUGUI timerText;

        private void Update()
        {
            timerText.text = timer.TimeLeft.ToString("F2");
        }
    }
}
