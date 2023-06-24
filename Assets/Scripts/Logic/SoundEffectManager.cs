using Beans;
using UnityEngine;

namespace Logic
{
    public class SoundEffectManager : MonoBehaviour
    {
        [SerializeField] private AudioSource rotateBeanAudioSource;
        [SerializeField] private AudioSource beanCanScoredAudioSource;
        [SerializeField] private AudioSource beanLostAudioSource;
        [SerializeField] private AudioSource beanPlacedAudioSource;
        [SerializeField] private AudioSource conveyorMoveAudioSource;
        [SerializeField] private AudioSource beanMoveAudioSource;
        [SerializeField] private AudioSource beanCanSpawnAudioSource;
        
        private void OnEnable()
        {
            Rotate.BeanRotated += PlayBeanRotateSound;
            Scorer.BeanCanScored += PlayBeanCanScoredSound;
            Bean.BeanEnteredDeathPlane += PlayBeanLostSound;
            Fall.OnBeanShouldStopCollision += PlayBeanPlacedSound;
            Fall.BeanMovedVertically += PlayBeanMoveSound;
            Conveyor.ConveyorMoved += PlayConveyorMoveSound;
            HorizontalMovement.BeanMovedHorizontally += PlayBeanMoveSound;
            BeanCanSpawner.BeanCanSpawned += PlayBeanCanSpawnSound;
        }
        
        private void OnDisable()
        {
            Rotate.BeanRotated -= PlayBeanRotateSound;
            Scorer.BeanCanScored -= PlayBeanCanScoredSound;
            Bean.BeanEnteredDeathPlane -= PlayBeanLostSound;
            Fall.OnBeanShouldStopCollision -= PlayBeanPlacedSound;
            Conveyor.ConveyorMoved -= PlayConveyorMoveSound;
            HorizontalMovement.BeanMovedHorizontally -= PlayBeanMoveSound;
            Fall.BeanMovedVertically -= PlayBeanMoveSound;
            BeanCanSpawner.BeanCanSpawned -= PlayBeanCanSpawnSound;
        }
        
        private void PlayBeanRotateSound()
        {
            rotateBeanAudioSource.Play();
        }
        
        private void PlayBeanCanScoredSound()
        {
            beanCanScoredAudioSource.Play();
        }
        
        private void PlayBeanLostSound()
        {
            beanLostAudioSource.Play();
        }
        
        private void PlayBeanPlacedSound()
        {
            beanPlacedAudioSource.Play();
        }
        
        private void PlayConveyorMoveSound()
        {
            conveyorMoveAudioSource.Play();
        }
        
        private void PlayBeanMoveSound()
        {
            beanMoveAudioSource.Play();
        }
        
        private void PlayBeanCanSpawnSound()
        {
            beanCanSpawnAudioSource.Play();
        }
    }
}