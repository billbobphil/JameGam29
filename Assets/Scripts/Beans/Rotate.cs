using UnityEngine;

namespace Beans
{
    public class Rotate : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                transform.Rotate(new Vector3(0,0,90));
            }

            if (Input.GetKeyDown(KeyCode.X))
            {
                transform.Rotate(new Vector3(0,0,-90));
            }
        }
    }
}
