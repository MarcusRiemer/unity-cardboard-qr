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
            GameObject.Find("LocationText").GetComponent<Text>().text = "Orte";
            GameObject.Find("Eventtext").GetComponent<Text>().text = "Die nächsten Events";
            GameObject.Find("WifiPosition").GetComponent<Text>().text = "";

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
                var loc = GlobalState.Instance.AllLocations.locations[GlobalState.Instance.CurrentDestination];
                GameObject.Find("Eventtext").GetComponent<Text>().text = "Die nächsten Events in: " + loc.location;
                GameObject.Find("LocationText").GetComponent<Text>().text = "Ort zurücksetzen";
                GameObject.Find("WifiPosition").GetComponent<Text>().text = loc.describtion;
                var eve = GlobalState.Instance.AllThings.things.Where(x => x.location == loc.location);
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

        public void SaveStateAndCloseApplication()
        {
            GlobalState.Save();
            Application.Quit();
        }

        public void OnButtonClick()
        {
            GameObject.Find("LocationText").GetComponent<Text>().text = "Orte";
            GameObject.Find("Eventtext").GetComponent<Text>().text = "Die nächsten Events";
            GameObject.Find("WifiPosition").GetComponent<Text>().text = "";
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
            Location act = null;
            Location sec = null;

            var actBSSID = ajo.Call<String>("Scan");

            if (actBSSID != "empty")
            {
                var secBSSID = ajo.Call<String>("getSecBSSID");
                foreach (Location loc in GlobalState.Instance.AllLocations.locations)
                {
                    if (loc.bssids.Contains(actBSSID))
                        act = loc;

                    if (secBSSID != "empty" && loc.bssids.Contains(secBSSID))
                        sec = loc;

                    if (act != null && sec != null)
                        break;
                }
                if (act != null && sec != null)
                    if (act == sec)
                        s = "Aktuell Position: " + act.location + ". " + act.describtion;
                    else
                        s = "Aktuelle Position: zwischen " + act.location + " und " + sec.location;
                else if (act != null)
                    s = "Wahrscheinliche aktuelle Position: " + act.location + ". " + act.describtion;
                else if (sec != null)
                    s = "Wahrscheinliche aktuelle Position: " + sec.location + ". " + sec.describtion;
            }
            GameObject.Find("WifiPosition").GetComponent<Text>().text = s;
        }

        public void OnGameClick()
        {
            SceneManager.LoadScene(Config.SceneName(Config.Scenes.Game));
        }
    }
}