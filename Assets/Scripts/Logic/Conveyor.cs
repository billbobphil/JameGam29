using System;
using BeanCans;
using Beans;
using UnityEngine;

namespace Logic
{
    public class Conveyor : MonoBehaviour
    {
        [SerializeField] private float moveDelay = 1f;
        [SerializeField] private int moveDistance = 1;
        private float _timer = 0f;
        private PlayGrid _grid;
        private KeeperTracker _keeperTracker;
        private bool _shouldMove;

        private void OnEnable()
        {
            Fall.OnBeanShouldStopCollision += SetShouldMoveConveyor;
        }
        
        private void OnDisable()
        {
            Fall.OnBeanShouldStopCollision -= SetShouldMoveConveyor;
        }

        private void Awake()
        {
            _grid = GameObject.FindWithTag("PlayGrid").GetComponent<PlayGrid>();
            _keeperTracker = GameObject.FindWithTag("Overseer").GetComponent<KeeperTracker>();
        }

        private void SetShouldMoveConveyor()
        {
            _shouldMove = true;
        }

        private void Update()
        {
            _timer += Time.deltaTime;

            if (_timer >= moveDelay && _shouldMove)
            {
               MoveConveyor();

                _timer = 0f;
                _shouldMove = false;
            }
        }

        [ContextMenu("Move Conveyor")]
        private void MoveConveyor()
        {
            foreach (Bean bean in _keeperTracker.beans)
            {
                bean.MoveRight(moveDistance);
            }
                
            foreach(BeanCan beanCan in _keeperTracker.beanCans)
            {
                beanCan.MoveRight(moveDistance);
            }
        }
    }
}
