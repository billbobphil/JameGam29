using UnityEngine;

namespace Logic
{
    public class PlayGrid : MonoBehaviour
    {
        public int width = 56;
        public int height = 32;
        public Transform[,] Grid;

        private void Awake()
        {
            Grid = new Transform[width, height];
        }
    }
}
