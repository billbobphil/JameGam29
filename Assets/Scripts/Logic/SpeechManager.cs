using System.Collections.Generic;
using Helpers;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Logic
{
    public class SpeechManager : MonoBehaviour
    {
        [SerializeField] private List<string> textLines;
        [SerializeField] private TextMeshProUGUI speechText;
        private int _currentLine = 0;
        [SerializeField] private SceneNavigation sceneNavigation;
        [SerializeField] private AudioSource nextLineSound;
        
        private void Start()
        {
            DisplayLine();
        }
        
        private void NextLine()
        {
            _currentLine++;

            if (_currentLine >= textLines.Count)
            {
                sceneNavigation.PlayGame();        
            }
            else
            {
                DisplayLine(); 
            }
        }

        private void DisplayLine()
        {
            nextLineSound.Play();
            speechText.text = textLines[_currentLine];
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                NextLine();
            }
        }
    }
}
