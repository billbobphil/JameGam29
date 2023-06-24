using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Beans;
using Helpers;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using Vector2 = UnityEngine.Vector2;

namespace Logic
{
    public class BeanSpawner : MonoBehaviour
    {
        [SerializeField]
        private List<GameObject> beanPrefabs;
        public static UnityAction BeanSpawned;
        public Conveyor conveyor;
        
        private int _xPositionRounded = 0;
        private int _yPositionRounded = 0;
    
        private void OnEnable()
        {
            Fall.OnBeanShouldStopCollision += Spawn;
            Bean.BeanEnteredDeathPlane += Spawn;
        }
        
        private void OnDisable()
        {
            Fall.OnBeanShouldStopCollision -= Spawn;
            Bean.BeanEnteredDeathPlane -= Spawn;
        }
        
        private void Start()
        {
            Vector2 roundedPosition = RoundVector2.Round(transform.position);
            _xPositionRounded = (int) roundedPosition.x;
            _yPositionRounded = (int) roundedPosition.y;
            
            Spawn();
        }
    
        private void Spawn() 
        {
            GameObject bean = Instantiate(beanPrefabs[Random.Range(0, beanPrefabs.Count)]);
            bean.transform.position = new Vector2(_xPositionRounded, _yPositionRounded);
            StartCoroutine(WaitToTriggerSpawnBehavior(bean));
        }
        
        private IEnumerator WaitToTriggerSpawnBehavior(GameObject createdBean)
        {
            yield return new WaitForSeconds(1f);
            BeanSpawned?.Invoke();
            createdBean.GetComponent<Fall>().StartFalling();
        }
    }
}
