using UnityEngine;
using UnityEngine.Serialization;

namespace Beans
{
    public class HorizontalMovement : MonoBehaviour
    {
        [SerializeField] private float speed = 1f;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                transform.position += Vector3.left * speed;
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                transform.position += Vector3.right * speed;
            }
        }
    }
}
