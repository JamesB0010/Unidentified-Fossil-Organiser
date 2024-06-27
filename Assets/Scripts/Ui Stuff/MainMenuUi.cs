using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEditor;

namespace UFO_UI
{
    
    public class MainMenuUi : MonoBehaviour
    {

        [SerializeField] private AudioClip buttonPressClip;
        private AudioSource audioSource;
        public TMP_InputField InitialsInput;
        public TMP_Text placeholderTxt;

        private void Start()
        {
            
            Destroy(FindObjectOfType<EasterEggManager>());
            this.audioSource = GetComponent<AudioSource>();

            var keyboardButtons = FindObjectsOfType<KeyboardButton>();
            foreach (var button in keyboardButtons)
            {
                button.OnButtonPressed += () =>
                {
                    this.audioSource.clip = this.buttonPressClip; this.audioSource.Play(); Debug.Log("Play press sound"); };
            }

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        public void StartGame()
        {
            canvasManager.timeRun = 0;

            string initials = InitialsInput.text;

            Debug.Log(InitialsInput.text);

            if (initials.Length < 3 && initials != null && initials != "" || initials == "RESETLEADERBOARD")
            {
                PlayerPrefs.SetString("PlayerCurrentName", initials);

                SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);
            }
            else
            {
                placeholderTxt.text = "Enter Your 2 Character Name";
                placeholderTxt.color = Color.red;
                placeholderTxt.fontStyle = FontStyles.Bold;
            }
        }

        public void Credits()
        {
            SceneManager.LoadScene(3, LoadSceneMode.Single);
        }
}

}
