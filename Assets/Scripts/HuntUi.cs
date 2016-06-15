using System;
using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts
{

    /// <summary>
    /// Controls the main menu behavior. Is attached to Unity MainMenuScene.Canvas.
    /// </summary>
    class HuntUi : MonoBehaviour
    {
        protected virtual void Start()
        {
            // Initialize all text fields and button texts.
            GameObject.Find("QuestionProgressText").GetComponent<Text>().text = string.Format(StringResources.QuestionProgressText, "?", "?");
            GameObject.Find("CoinProgressText").GetComponent<Text>().text = string.Format(StringResources.CoinProgressText, "?", "?");
            GameObject.Find("TitleText").GetComponent<Text>().text = StringResources.GameHeading;

            GameObject.Find("GoButton").GetComponentInChildren<Text>().text = StringResources.GoButtonText;
            GameObject.Find("HelpButton").GetComponentInChildren<Text>().text = StringResources.HelpButtonText;
            GameObject.Find("BackButton").GetComponentInChildren<Text>().text = StringResources.BackButtonText;



            // Update progress texts.
            GameObject.Find("QuestionProgressText").GetComponent<Text>().text = string.Format(StringResources.QuestionProgressText,
                GlobalState.Instance.UnlockedCoinCount(), GlobalState.Instance.AllQuestions.questions.Length);
            GameObject.Find("CoinProgressText").GetComponent<Text>().text = string.Format(StringResources.CoinProgressText,
                GlobalState.Instance.CollectedCoinCount(), GlobalState.Instance.AllQuestions.questions.Length);
        }

        protected virtual void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
				SaveStateAndCloseApplication();
            }
        }

		public void SaveStateAndCloseApplication() 
		{
			GlobalState.Save();
			Application.Quit();
		}

        /// <summary>
        /// Switches to CameraScene. Is attached to Unity MainScene.GoButton.
        /// </summary>
        public void OnGoClick()
        {
            SceneManager.LoadScene(Config.SceneName(Config.Scenes.Camera));
        }

        /// <summary>
        /// Switches to HelpScene. Is attached to Unity MainScene.HelpButton.
        /// </summary>
        public void OnHelpClick()
        {
			SceneManager.LoadScene(Config.SceneName(Config.Scenes.Help));
        }

        /// <summary>
        /// Switches to NavigationScene. Is attached to Unity MainScene.NaviButton.
        /// </summary>
        public void OnBackClick()
        {
            SceneManager.LoadScene(Config.SceneName(Config.Scenes.Start));
        }
    }
}