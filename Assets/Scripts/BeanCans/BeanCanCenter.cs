using UnityEngine;
using UnityEngine.Events;

namespace BeanCans
{
    public class BeanCanCenter : MonoBehaviour
    {
        private BeanCan _beanCan;
        public static UnityAction BeanCanSpawnTrigger;
        public static UnityAction<BeanCan> BeanCanAbductionTrigger;

        private void Awake()
        {
            _beanCan = GetComponentInParent<BeanCan>();
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("CanAbductionZone"))
            {
                Debug.Log("Time to Abduct Bean Can");
                BeanCanAbductionTrigger?.Invoke(_beanCan);
            }

            if (other.CompareTag("CanSpawnZone"))
            {
                Debug.Log("Spawn Next Bean Can");
                BeanCanSpawnTrigger?.Invoke();
            }
        }
    }
}
