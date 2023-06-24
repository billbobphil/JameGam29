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
        private int _timesToMove = 0;

        private void OnEnable()
        {
            BeanSpawner.BeanSpawned += DecideIfConveyorShouldMove;
        }
        
        private void OnDisable()
        {
            BeanSpawner.BeanSpawned -= DecideIfConveyorShouldMove;
        }

        private void Awake()
        {
            _grid = GameObject.FindWithTag("PlayGrid").GetComponent<PlayGrid>();
            _keeperTracker = GameObject.FindWithTag("Overseer").GetComponent<KeeperTracker>();
        }

        private void DecideIfConveyorShouldMove()
        {
            if (_timesToMove > 0)
            {
                MoveConveyor();
                _timer = 0f;
                _timesToMove = 0;
            }
        }

        private void Update()
        {
            _timer += Time.deltaTime;

            if (_timer >= moveDelay)
            {
                _timesToMove++;
                _timer = 0f;
            }
        }

        [ContextMenu("Move Conveyor")]
        private void MoveConveyor()
        {
            int distanceToMove = moveDistance * _timesToMove;
            
            foreach (Bean bean in _keeperTracker.beans)
            {
                bean.MoveRight(distanceToMove);
            }
                
            foreach(BeanCan beanCan in _keeperTracker.beanCans)
            {
                beanCan.MoveRight(distanceToMove);
            }
        }
    }
}
