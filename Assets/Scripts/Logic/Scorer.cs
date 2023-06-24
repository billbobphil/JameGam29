using System;
using System.Collections.Generic;
using BeanCans;
using Beans;
using Helpers;
using UnityEngine;

namespace Logic
{
    public class Scorer : MonoBehaviour
    {
        private PlayGrid _playGrid;
        private KeeperTracker _keeperTracker;
        
        private void Awake()
        {
            _playGrid = GameObject.FindWithTag("PlayGrid").GetComponent<PlayGrid>();
            _keeperTracker = GameObject.FindWithTag("Overseer").GetComponent<KeeperTracker>();
        }
        
        private void OnEnable()
        {
            BeanCanCenter.BeanCanAbductionTrigger += ScoreBeanCan;
        }
        
        private void OnDisable()
        {
            BeanCanCenter.BeanCanAbductionTrigger -= ScoreBeanCan;
        }
        
        private void ScoreBeanCan(BeanCan beanCan)
        {
            int totalPossibleTiles = (beanCan.width - 2) * (beanCan.height - 1);
            int totalTilesWithBeans = 0;
            
            Debug.Log("Scoring Bean Can");
            Vector2 beanCanPositionRounded = RoundVector2.Round(beanCan.transform.position);
            int beanCanXPos = (int) beanCanPositionRounded.x;
            int beanCanYPos = (int) beanCanPositionRounded.y;
            
            Debug.Log($"Bean Can Position Rounded: {beanCanPositionRounded}");

            List<Bean> beansWithinCan = new List<Bean>();
            
            //This loop makes me want to die, but it works and I don't have time to refactor it and the surrounding data structures
            for (int y = beanCanYPos + 1; y <= beanCanYPos + beanCan.height - 1; y++)
            {
                for (int x = beanCanXPos + 1; x <= beanCanXPos + beanCan.width - 2; x++)
                {
                    if(_playGrid.Grid[x, y] != null && _playGrid.Grid[x, y].gameObject.CompareTag("BeanTile"))
                    {
                        totalTilesWithBeans++;
                    }
                    
                    foreach (Bean bean in _keeperTracker.beans)
                    {
                        Vector2 beanRoundedPosition = RoundVector2.Round(bean.transform.position);
                        if(beanRoundedPosition == new Vector2(x, y))
                        {
                            beansWithinCan.Add(bean);
                        }
                    }
                }
            }
            
            Debug.Log($"Total Tiles With Beans: {totalTilesWithBeans}");
            Debug.Log($"Total Possible Tiles: {totalPossibleTiles}");
            
            int score = (int) Math.Round((double) totalTilesWithBeans / totalPossibleTiles * 100);
            
            //==== Get rid of the beans and the can =====
            
            //Remove bean can references from grid
            foreach (Transform child in beanCan.transform)
            {
                if(child.CompareTag("Lid")) continue;
                
                foreach (Transform baseBlock in child)
                {
                    Vector2 roundedPosition = RoundVector2.Round(baseBlock.position);
                    
                    _playGrid.Grid[(int) roundedPosition.x, (int) roundedPosition.y] = null;
                }
            }
            
            //then for each bean we need to get each of it's children and null it on the grid
            //then remove the bean from the keeper tracker
            //then destroy the bean
            foreach(Bean bean in beansWithinCan)
            {
                foreach (Transform child in bean.transform)
                {
                    Vector2 roundedPosition = RoundVector2.Round(child.position);
                    
                    _playGrid.Grid[(int) roundedPosition.x, (int) roundedPosition.y] = null;
                }
                
                _keeperTracker.beans.Remove(bean);
                Destroy(bean.gameObject);
            }
            
            _keeperTracker.beanCans.Remove(beanCan);
            Destroy(beanCan.gameObject);
        }
    }
}
