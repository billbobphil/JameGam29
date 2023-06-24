using System;
using Helpers;
using Logic;
using UnityEngine;

namespace Beans
{
    public class Bean : MonoBehaviour
    {
        private PlayGrid _gridBrain;
        private bool _isActiveBean = true;

        private void OnEnable()
        {
            Fall.OnBeanShouldStopCollision += StoreBeanReference;
            Fall.OnBeanShouldStopCollision += MarkBeanInactive;
        }
        
        private void OnDisable()
        {
            Fall.OnBeanShouldStopCollision -= StoreBeanReference;
            Fall.OnBeanShouldStopCollision -= MarkBeanInactive;
        }
        
        private void StoreBeanReference()
        {
            if (!_isActiveBean) return;
            
            GameObject.FindWithTag("Overseer").GetComponent<KeeperTracker>().beans.Add(this);
        }

        private void MarkBeanInactive()
        {
            _isActiveBean = false;
        }

        private void Awake()
        {
            _gridBrain = GameObject.FindWithTag("PlayGrid").GetComponent<PlayGrid>();
        }

        public bool IsNewPositionValidGridPosition()
        {
            foreach (Transform child in transform)
            {
                Vector2 tilePosition = RoundVector2.Round(child.position);
                int x = (int) tilePosition.x;
                int y = (int) tilePosition.y;
                
                if(_gridBrain.Grid[x, y] != null && _gridBrain.Grid[x, y].parent != transform)
                {
                    return false;
                }
            }

            return true;
        }

        public void MoveRight(int distance)
        {
            Vector2[] oldPositions = GetCurrentChildBeanPositions();
            transform.position += Vector3.right * distance;
            UpdateGridPosition(oldPositions);
        }

        public void UpdateGridPosition(Vector2[] oldPositions)
        {
            foreach (Vector2 oldPosition in oldPositions)
            {
                Vector2 oldPositionRounded = RoundVector2.Round(oldPosition);
                _gridBrain.Grid[(int)oldPositionRounded.x, (int)oldPositionRounded.y] = null;
            }
            
            foreach (Transform child in transform)
            {
                Vector2 newPositionRounded = RoundVector2.Round(child.position);
                _gridBrain.Grid[(int)newPositionRounded.x, (int)newPositionRounded.y] = child;
            }
        }

        public Vector2[] GetCurrentChildBeanPositions()
        {
            Vector2[] positions = new Vector2[transform.childCount];
            for(int i = 0; i < transform.childCount; i++)
            {
                positions[i] = RoundVector2.Round(transform.GetChild(i).position);
            }

            return positions;
        }
    }
}
