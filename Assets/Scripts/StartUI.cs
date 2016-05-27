using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts
{
    class StartUI : MonoBehaviour
    {
        AndroidJavaObject ajo;

        protected virtual IEnumerator Start()
        {
            Application.targetFrameRate = 30;
            GlobalState.Instance.SceneToSwitchTo = Config.Scenes.None;

            GameObject.Find("GoButton").GetComponentInChildren<Text>().text = StringResources.GoButtonText;
            GameObject.Find("HelpButton").GetComponentInChildren<Text>().text = StringResources.HelpButtonDefaultText;
            GameObject.Find("GameButton").GetComponentInChildren<Text>().text = StringResources.GameButtonText;
            if (Config.locationBased)
                GameObject.Find("WifiButton").GetComponentInChildren<Text>().text = StringResources.WifiButtonText;
            else
                Destroy(GameObject.Find("WifiButton"));

            // Load events from API
            var thingsWww = new WWW(Config.ApiUrlThings);
            yield return thingsWww;
            GlobalState.Instance.AllThings = JsonUtility.FromJson<Things>(thingsWww.text);
            Array.Sort(GlobalState.Instance.AllThings.things);
            thingsWww.Dispose();

            var eve = GlobalState.Instance.AllThings.things;
            for (int i = 1; i <= 2; i++)
            {
                var elem = eve.ElementAtOrDefault(i - 1);
                if (elem != null)
                    GameObject.Find("Event" + i).GetComponent<Text>().text = string.Format(StringResources.AlgEvent, elem.location, elem.start - DateTime.Now.Minute, elem.thing);
                else
                    GameObject.Find("Event" + i).GetComponent<Text>().text = "";
            }

            // Load positions from API
            var positionsWww = new WWW(Config.ApiUrlPositions);
            yield return positionsWww;
            GlobalState.Instance.AllPositions = JsonUtility.FromJson<Positions>(positionsWww.text);
            positionsWww.Dispose();

            // Load questions from API.
            var questionsWww = new WWW(Config.ApiUrlQuestions);
            yield return questionsWww;
            GlobalState.Instance.AllQuestions = JsonUtility.FromJson<Questions>(questionsWww.text);
            questionsWww.Dispose();

            var ajc = new AndroidJavaClass("WifiScan");
            ajo = ajc.CallStatic<AndroidJavaObject>("getWifi");
        }

        protected virtual void Update()
        {
            if (GlobalState.Instance.NewLocation == true)
            {
                GlobalState.Instance.NewLocation = false;
                var loc = GlobalState.Instance.AllLocations.locations[GlobalState.Instance.CurrentDestination].location;
                GameObject.Find("Eventtext").GetComponent<Text>().text = "Die nächsten Events in: " + loc;
                GameObject.Find("LocationText").GetComponent<Text>().text = "Ort zurücksetzen";
                var eve = GlobalState.Instance.AllThings.things.Where(x => x.location == loc);
                for (int i = 1; i <= 2; i++)
                {
                    var elem = eve.ElementAtOrDefault(i - 1);
                    if (elem != null)
                        GameObject.Find("Event" + i).GetComponent<Text>().text = string.Format(StringResources.NextEvent, elem.start - DateTime.Now.Minute, elem.thing);
                    else
                        GameObject.Find("Event" + i).GetComponent<Text>().text = "";
                }
            }
        }

        public void OnButtonClick()
        {
            GameObject.Find("LocationText").GetComponent<Text>().text = "Orte";
            GameObject.Find("Eventtext").GetComponent<Text>().text = "Die nächsten Events";
            GlobalState.Instance.CurrentDestination = -1;
            var eve = GlobalState.Instance.AllThings.things;
            for (int i = 1; i <= 2; i++)
            {
                var elem = eve.ElementAtOrDefault(i - 1);
                if (elem != null)
                    GameObject.Find("Event" + i).GetComponent<Text>().text = string.Format(StringResources.AlgEvent, elem.location, elem.start - DateTime.Now.Minute, elem.thing);
                else
                    GameObject.Find("Event" + i).GetComponent<Text>().text = "";
            }

        }

        public void OnHelpClick()
        {
            SceneManager.LoadScene(Config.SceneName(Config.Scenes.HelpDefault));
        }

        public void OnGoClick()
        {
            SceneManager.LoadScene(Config.SceneName(Config.Scenes.Camera));
        }

        public void OnWifiClick ()
        {
            var s = StringResources.NoWifi;
            string loc = "";
            if (Config.BSSIDs.TryGetValue(ajo.Call<string>("Scan"), out loc))
            {
                if (new List<String> { "Audimax", "Audimax Brücke 1.OG", "Lesemax", "VR Lab 1.OG", "Audimax 1.OG" }.Contains(loc))
                    s = StringResources.Mediabuilding;
                else if (new List<String> { "RZ5", "Cafeteria" }.Contains(loc))
                    s = StringResources.South;
                else if (new List<String> { "Altbau ganz rechts 2.OG", "Altbau rechts 2.OG", "Altbau rechts 1.OG", "Altbau rechts", "Altbau links 2.OG", "Altbau links 1.OG", "Altbau links" }.Contains(loc))
                    s = StringResources.Old;
                else if (new List<String> { "Kreuzung HS4", "SR11" }.Contains(loc))
                    s = StringResources.Entrance;
                else if (new List<String> { "Lernboxen", "Altbau ganz links", "Netztechnik" }.Contains(loc))
                    s = StringResources.Passage;
                else if (new List<String> { "vor SR1", "vor HS2", "über HS2 1.OG", "über HS2 2.OG", "zwischen HS1 und HS2", "Untergeschoss", "HS1", "über HS1 1.OG", "über HS1 2.OG"  }.Contains(loc))
                    s = StringResources.Overbuilding;
                else if (new List<String> { "HS5", "Serverraum", "vor HS5" }.Contains(loc))
                    s = StringResources.Passage2;
                else if (new List<String> { "SR9 1.OG", "RZ2", "RZ3" }.Contains(loc))
                    s = StringResources.North;
            }
            GameObject.Find("WifiPosition").GetComponent<Text>().text = s;
        }

        public void OnGameClick()
        {
            SceneManager.LoadScene(Config.SceneName(Config.Scenes.Game));
        }
    }
}
