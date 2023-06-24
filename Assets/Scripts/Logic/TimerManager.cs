using UnityEngine;
using Utilities;

namespace Logic
{
    public class TimerManager : MonoBehaviour
    {
        [SerializeField] private Timer timer;
        
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
        }
    }
}