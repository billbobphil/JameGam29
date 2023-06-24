using Helpers;
using Logic;
using UnityEngine;
using UnityEngine.Serialization;

namespace BeanCans
{
    public class BeanCan : MonoBehaviour
    {
        private PlayGrid _playGrid;
        public int width;
        public int height;
        
        private void Awake()
        {
            _playGrid = GameObject.FindWithTag("PlayGrid").GetComponent<PlayGrid>();
        }

        private void Start()
        {
            RegisterWithGrid();
        }

        private void RegisterWithGrid()
        {
            foreach (Transform child in transform)
            {
                if(child.CompareTag("Lid")) continue;
                
                foreach (Transform baseBlock in child)
                {
                    Vector2 roundedPosition = RoundVector2.Round(baseBlock.position);
                    _playGrid.Grid[(int) roundedPosition.x, (int) roundedPosition.y] = baseBlock;
                }
            }
        }
        
        private void UnregisterWithGrid()
        {
            foreach (Transform child in transform)
            {
                if(child.CompareTag("Lid")) continue;
                
                foreach (Transform baseBlock in child)
                {
                    Vector2 roundedPosition = RoundVector2.Round(baseBlock.position);
                    _playGrid.Grid[(int) roundedPosition.x, (int) roundedPosition.y] = null;
                }
            }
        }

        public void MoveRight(int distance)
        {
            UnregisterWithGrid();
            transform.position += Vector3.right * distance;
            RegisterWithGrid();
        }
    }
}
