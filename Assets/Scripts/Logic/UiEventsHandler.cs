using TMPro;
using UnityEngine;

namespace Logic
{
    public class UiEventsHandler : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scoreText;
        
        private void OnEnable()
        {
            Scorer.ScoreUpdated += UpdateScoreText;
        }
        
        private void OnDisable()
        {
            Scorer.ScoreUpdated -= UpdateScoreText;
        }
        
        private void UpdateScoreText(int score)
        {
            scoreText.text = score.ToString();
        }
        
    }
}
