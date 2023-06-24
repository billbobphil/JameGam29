using System.Collections.Generic;
using Beans;
using UnityEngine;

namespace Logic
{
    public class SpawnBean : MonoBehaviour
    {
        [SerializeField]
        private List<GameObject> beanPrefabs;
    
        private void OnEnable()
        {
            BeanCollision.OnBeanShouldStopCollision += Spawn;
        }
        
        private void OnDisable()
        {
            BeanCollision.OnBeanShouldStopCollision -= Spawn;
        }
        
        private void Start()
        {
            Spawn();
        }
    
        private void Spawn() 
        {
            GameObject bean = Instantiate(beanPrefabs[Random.Range(0, beanPrefabs.Count)]);
            bean.transform.position = transform.position;
            bean.GetComponent<Fall>().StartFalling();
        }
    }
}
