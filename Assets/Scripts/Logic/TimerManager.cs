using TMPro;
using UnityEngine;
using Utilities;

namespace Logic
{
    public class TimerManager : MonoBehaviour
    {
        [SerializeField] private Timer timer;
        //Putting these here is wrong wrong wrong
        [SerializeField] private GameObject endGamePanel;
        [SerializeField] private TextMeshProUGUI endGameScoreText;

        private void Awake()
        {
            endGamePanel.SetActive(false);
            Time.timeScale = 1;
        }
        
        private void OnEnable()
        {
            Timer.TimerExpired += TimerExpired;
        }
        
        private void OnDisable()
        {
            Timer.TimerExpired -= TimerExpired;
        }

        private void Start()
        {
            timer.StartTimer();
        }

        private void TimerExpired()
        {
            timer.StopTimer();
            Debug.Log("Timer expired");
            endGamePanel.SetActive(true);
            Time.timeScale = 0;
            endGameScoreText.text = GetComponent<Scorer>().GetCurrentScore().ToString();
        }
    }
}