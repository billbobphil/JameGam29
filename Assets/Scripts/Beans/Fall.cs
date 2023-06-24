using UnityEngine;
using UnityEngine.Events;

namespace Beans
{
   public class Fall : MonoBehaviour
   {
      [SerializeField] private float speed = 1f;
      [SerializeField] private float softMoveDelay = 1f;
      [SerializeField] private float hardMoveDelay = 1f;
      [SerializeField] private bool isFalling = false;
      [SerializeField] private float moveDelay = 1f;
      private float _timer = 0f;
      private float _startingMoveDelay;
      private Bean _bean;
      
      public static UnityAction OnBeanShouldStopCollision;
      public static UnityAction BeanMovedVertically;
      
      private void Awake()
      {
         _bean = GetComponent<Bean>();
      }
      
      private void Start()
      {
         _startingMoveDelay = moveDelay;
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
            moveDelay = softMoveDelay;
         }
         else if (Input.GetKey(KeyCode.Space))
         {
            moveDelay = hardMoveDelay;
         }
         else
         {
            moveDelay = _startingMoveDelay;
         }

         if (_timer >= moveDelay)
         {
            Vector2[] oldPositions = _bean.GetCurrentChildBeanPositions();

            transform.position += Vector3.down * speed;
            
            if (_bean.IsNewPositionValidGridPosition())
            {
               _bean.UpdateGridPosition(oldPositions);
               BeanMovedVertically?.Invoke();
            }
            else
            {
               transform.position += Vector3.up * speed;
               OnBeanShouldStopCollision?.Invoke();
               StopFalling();
            }
            
            _timer = 0f;
         }
      }
   }
}
