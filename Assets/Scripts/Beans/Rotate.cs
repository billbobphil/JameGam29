using UnityEngine;

namespace Beans
{
    public class Rotate : MonoBehaviour
    {
        private bool _isEnabled = true;
        private Bean _bean;
        
        private void Awake()
        {
            _bean = GetComponent<Bean>();
        }
        
        private void OnEnable()
        {
            BeanCollision.OnBeanShouldStopCollision += DisableMovement;
        }
        
        private void OnDisable()
        {
            BeanCollision.OnBeanShouldStopCollision -= DisableMovement;
        }
        
        private void DisableMovement()
        {
            _isEnabled = false;
        }
        
        private void Update()
        {
            if(!_isEnabled) return;
            
            if (Input.GetKeyDown(KeyCode.Z))
            {
                RotateAmount(90);
            }

            if (Input.GetKeyDown(KeyCode.X))
            {
                RotateAmount(-90);
            }
        }

        private void RotateAmount(float amount)
        {
            Vector2[] oldPositions = _bean.GetCurrentChildBeanPositions();

            transform.Rotate(new Vector3(0, 0, amount));

            if (_bean.IsNewPositionValidGridPosition())
            {
                _bean.UpdateGridPosition(oldPositions);
            }
            else
            {
                transform.Rotate(new Vector3(0, 0, amount * -1));
            }
        }
    }
}
