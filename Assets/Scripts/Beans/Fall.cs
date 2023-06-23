using UnityEngine;

namespace Beans
{
   public class Fall : MonoBehaviour
   {
      [SerializeField] private float speed = .3f;
      [SerializeField] private float softFallSpeed = .3f;
      [SerializeField] private float hardFallSpeed = .3f;
      [SerializeField] private bool isFalling = false;
      private float _startingSpeed;
      
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
         if (Input.GetKey(KeyCode.DownArrow))
         {
            speed = softFallSpeed;
            return;
         }

         if (Input.GetKey(KeyCode.Space))
         {
            speed = hardFallSpeed;
            return;
         }

         speed = _startingSpeed;
      }
   
      private void FixedUpdate()
      {
         if(!isFalling) return;
         // transform.position += Vector3.down * speed;
         GetComponent<Rigidbody2D>().MovePosition(GetComponent<Rigidbody2D>().position + (Vector2.down * speed));
      }
   }
}
