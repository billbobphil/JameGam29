using System.Collections.Generic;
using BeanCans;
using Helpers;
using UnityEngine;
using UnityEngine.Events;

namespace Logic
{
    public class BeanCanSpawner : MonoBehaviour
    {
        [SerializeField] private List<GameObject> beanCanPrefabs;
        private int _roundedSpawnerPositionX;
        private int _roundedSpawnerPositionY;

        public static UnityAction BeanCanSpawned;
        
        private void OnEnable()
        {
            BeanCanCenter.BeanCanSpawnTrigger += SpawnBeanCan;
        }
        
        private void OnDisable()
        {
            BeanCanCenter.BeanCanSpawnTrigger -= SpawnBeanCan;
        }
        
        private void Start()
        {
            Vector2 roundedPosition = RoundVector2.Round(transform.position);
            _roundedSpawnerPositionX = (int) roundedPosition.x;
            _roundedSpawnerPositionY = (int) roundedPosition.y;
            
            SpawnBeanCan();
        }

        private void SpawnBeanCan()
        {
            GameObject selectedPrefab = beanCanPrefabs[Random.Range(0, beanCanPrefabs.Count)];
            GameObject beanCan = Instantiate(selectedPrefab, new Vector3(_roundedSpawnerPositionX, _roundedSpawnerPositionY, 0), Quaternion.identity);
            GameObject.FindWithTag("Overseer").GetComponent<KeeperTracker>().beanCans.Add(beanCan.GetComponent<BeanCan>());
            BeanCanSpawned?.Invoke();
        }
    }
}
