using Logic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using Utilities;

namespace Beans
{
    public class HorizontalMovement : MonoBehaviour
    {
        [SerializeField] private float speed = 1f;
        private bool _isEnabled = true;
        private Bean _bean;

        public static UnityAction BeanMovedHorizontally;

        private void Awake()
        {
            _bean = GetComponent<Bean>();
        }
        
        private void OnEnable()
        {
            Fall.OnBeanShouldStopCollision += DisableMovement;
            Timer.TimerExpired += DisableMovement;
        }
        
        private void OnDisable()
        {
            Fall.OnBeanShouldStopCollision -= DisableMovement;
            Timer.TimerExpired -= DisableMovement;
        }
        
        private void DisableMovement()
        {
            _isEnabled = false;
        }

        private void Update()
        {
            if(!_isEnabled) return;
            
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                MoveDirection(Vector3.left);
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
              MoveDirection(Vector3.right);
            }
        }
        
        private void MoveDirection(Vector3 direction)
        {
            Vector2[] oldPositions = _bean.GetCurrentChildBeanPositions();

            transform.position += direction * speed;
            
            if (_bean.IsNewPositionValidGridPosition())
            {
                _bean.UpdateGridPosition(oldPositions);
                BeanMovedHorizontally?.Invoke();
            }
            else
            {
                transform.position += -direction * speed;
            }
        }
        
        
    }
}
