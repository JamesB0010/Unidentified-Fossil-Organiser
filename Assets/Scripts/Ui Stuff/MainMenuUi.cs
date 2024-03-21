using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

namespace UFO_UI
{
    
    public class MainMenuUi : MonoBehaviour
    {

        public TMP_InputField InitialsInput;
        public TMP_Text placeholderTxt;
        public void StartGame()
        {
            canvasManager.timeRun = 0;

            string initials = InitialsInput.text;

            Debug.Log(InitialsInput.text);

            if (initials.Length < 3 && initials != null && initials != "" || initials == "ResetLeaderboard")
            {
                PlayerPrefs.SetString("PlayerCurrentName", initials);

                SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);
            }
            else
            {
                placeholderTxt.text = "Please Enter Your Initials...";
                placeholderTxt.color = Color.red;
                placeholderTxt.fontStyle = FontStyles.Bold;
            }
        }

        public void QuitGame()
        {
            Application.Quit();
        }
}

}
