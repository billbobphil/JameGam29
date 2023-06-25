using UnityEngine;

namespace Polish
{
    public class ButtonSoundEffects : MonoBehaviour
    {
        [SerializeField] private AudioSource buttonClickSound;
        [SerializeField] private AudioSource buttonHoverSound;

        public void PlayButtonClickSound()
        {
            buttonClickSound.Play();
        }
        
        public void PlayButtonHoverSound()
        {
            buttonHoverSound.Play();
        }
    }
}
