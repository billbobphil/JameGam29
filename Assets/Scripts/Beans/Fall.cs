using UnityEngine;
using UnityEngine.Events;

namespace Beans
{
   public class Fall : MonoBehaviour
   {
      [SerializeField] private float speed = 1f;
      [SerializeField] private float softFallSpeed = 1f;
      [SerializeField] private float hardFallSpeed = 1f;
      [SerializeField] private bool isFalling = false;
      [SerializeField] private float moveDelay = 1f;
      private float _timer = 0f;
      private float _startingSpeed;
      private Bean _bean;
      
      public static UnityAction OnBeanShouldStopCollision;
      
      private void Awake()
      {
         _bean = GetComponent<Bean>();
      }
      
      private void Start()
      {
         _startingSpeed = speed;
      }

      [ContextMenu("Start Falling")]
      public void StartFalling()
      {
         isFalling = true;
      }
   
      [ContextMenu("Stop Falling")]
      public void StopFalling()
      {
         isFalling = false;
      }

      private void Update()
      { 
         if(!isFalling) return;
         
         _timer += Time.deltaTime;

         if (Input.GetKey(KeyCode.DownArrow))
         {
            speed = softFallSpeed;
         }
         else if (Input.GetKey(KeyCode.Space))
         {
            speed = hardFallSpeed;
         }
         else
         {
            speed = _startingSpeed;
         }

         if (_timer >= moveDelay)
         {
            Vector2[] oldPositions = _bean.GetCurrentChildBeanPositions();

            // transform.Translate(Vector3.down * speed);
            transform.position += Vector3.down * speed;
            
            if (_bean.IsNewPositionValidGridPosition())
            {
               _bean.UpdateGridPosition(oldPositions);
            }
            else
            {
               transform.Translate(Vector3.up * speed);
               transform.position += Vector3.up * speed;
               OnBeanShouldStopCollision?.Invoke();
               StopFalling();
            }
            
            _timer = 0f;
         }
      }
   }
}
