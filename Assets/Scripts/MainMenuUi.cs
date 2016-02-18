﻿using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class MainMenuUi : MonoBehaviour
    {
        // Use this for initialization
        protected virtual IEnumerator Start()
        {
            var questionsWww = new WWW(Config.ApiUrlQuestions);

            // Wait for download to complete
            yield return questionsWww;

            GlobalState.Instance.AllQuestions = JsonUtility.FromJson<Questions>(questionsWww.text);

            questionsWww.Dispose();

            GameObject.Find("QuestionProgressText").GetComponent<Text>().text = string.Format("Fragen: {0}/{1}",
                GlobalState.Instance.UnlockedCoinCount(), GlobalState.Instance.AllQuestions.questions.Length);
            GameObject.Find("CoinProgressText").GetComponent<Text>().text = string.Format("Münzen: {0}/{1}",
                GlobalState.Instance.CollectedCoinCount(), GlobalState.Instance.AllQuestions.questions.Length);

            // TODO: show win screen
        }

        // Update is called once per frame
        protected virtual void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                GlobalState.Save();
                Application.Quit();
            }
        }

        public void OnGoClick()
        {
            SceneManager.LoadScene(Config.CameraScene);
        }

        public void OnHelpClick()
        {
            // TODO: consider help
            Debug.Log("Help");
        }
    }
}