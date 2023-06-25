using UnityEngine;

namespace Polish
{
    public class WiggleImage : MonoBehaviour
    {
        public RectTransform targetRectTransform;
        public float totalMoveTime = 1f;
        public float maxHeight = 100f;
    
        private bool _movingUp = true;
        private float _timer = 0f;
        private Vector2 _initialPosition;
        private float _startDelay = 0;
        private bool _delayComplete;

        private void Start()
        {
            // Store the initial position of the RectTransform
            _initialPosition = targetRectTransform.anchoredPosition;

            _startDelay = Random.Range(0f, 3f);
        }

        private void Update()
        {
            if (!_delayComplete)
            {
                if (_timer >= _startDelay)
                {
                    _delayComplete = true;
                    _timer = 0f;
                }
                else
                {
                    _timer += Time.deltaTime;
                    return;
                }
            }
            
            // Update the timer
            _timer += Time.deltaTime;
        
            if (_movingUp)
            {
                // Calculate the current position based on the timer and maxHeight
                float newY = Mathf.Lerp(_initialPosition.y, _initialPosition.y + maxHeight, _timer / totalMoveTime);
                targetRectTransform.anchoredPosition = new Vector2(_initialPosition.x, newY);
            
                // If we reached the maxHeight, start moving down
                if (newY >= _initialPosition.y + maxHeight)
                {
                    _movingUp = false;
                    _timer = 0f;
                }
            }
            else
            {
                // Calculate the current position based on the timer and maxHeight
                float newY = Mathf.Lerp(_initialPosition.y + maxHeight, _initialPosition.y, _timer / totalMoveTime);
                targetRectTransform.anchoredPosition = new Vector2(_initialPosition.x, newY);
            
                // If we reached the initial position, start moving up again
                if (newY <= _initialPosition.y)
                {
                    _movingUp = true;
                    _timer = 0f;
                }
            }
        }   
    }
}
