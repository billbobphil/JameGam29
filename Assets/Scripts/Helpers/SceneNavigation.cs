using UnityEngine;
using UnityEngine.SceneManagement;

namespace Helpers
{
    public class SceneNavigation : MonoBehaviour
    {
        public void PlayGame()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(3);
        }

        public void LoadNarrative()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(2);
        }

        public void Menu()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(0);
        }

        public void Credits()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(1);
        }
    }
}
