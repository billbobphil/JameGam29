using System;
using System.Collections.Generic;
using BeanCans;
using Beans;
using UnityEngine;
using UnityEngine.Events;

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
        [SerializeField] private SpriteRenderer conveyorOneSpriteRenderer;
        [SerializeField] private SpriteRenderer conveyorTwoSpriteRenderer;
        private int _conveyorSpriteIndex = 1;
        [SerializeField] private List<Sprite> conveyorSprites;
        
        public static UnityAction ConveyorMoved;

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
            ConveyorMoved?.Invoke();
            
            if (_conveyorSpriteIndex == 0)
            {
                conveyorOneSpriteRenderer.sprite = conveyorSprites[0];
                conveyorTwoSpriteRenderer.sprite = conveyorSprites[0];
                _conveyorSpriteIndex = 1;
            }
            else
            {
                conveyorOneSpriteRenderer.sprite = conveyorSprites[1];
                conveyorTwoSpriteRenderer.sprite = conveyorSprites[1];
                _conveyorSpriteIndex = 0;
            }
            
            int distanceToMove = moveDistance * _timesToMove;
            
            foreach(BeanCan beanCan in _keeperTracker.beanCans)
            {
                beanCan.MoveRight(distanceToMove);
            }
            
            foreach (Bean bean in _keeperTracker.beans)
            {
                bean.MoveRight(distanceToMove);
            }
        }
    }
}
