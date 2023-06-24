using Logic;
using TMPro;
using UnityEngine;

namespace Polish
{
    public class ScoreAddedSpawner : MonoBehaviour
    {
        [SerializeField] GameObject scoreAddedPrefab;
        
        private void OnEnable()
        {
            Scorer.ScoreUpdated += SpawnScoreAdded;
        }
        
        private void OnDisable()
        {
            Scorer.ScoreUpdated -= SpawnScoreAdded;
        }
        
        private void SpawnScoreAdded(int score)
        {
            GameObject scoreAdded = Instantiate(scoreAddedPrefab, transform.position, Quaternion.identity);
            scoreAdded.transform.SetParent(transform);
            scoreAdded.GetComponentInChildren<TextMeshProUGUI>().text = $"+ {score.ToString()}";
        }
    }
}
