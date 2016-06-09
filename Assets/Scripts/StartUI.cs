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

            // Load questions from API.
            var particlesWww = new WWW(Config.ApiUrlParticles);
            yield return particlesWww;
            GlobalState.Instance.AllParticles = JsonUtility.FromJson<Particles>(particlesWww.text);
            particlesWww.Dispose();

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
            var BSSIDs = ajo.Call<String>("Scan");
            if (BSSIDs != "")
            {
                var ids = BSSIDs.Split(' ');
                List<Location> actMaxLoc = new List<Location>();
                int actMaxValue = 0;
                foreach (Location loc in GlobalState.Instance.AllLocations.locations)
                {
                    var len = loc.bssids.Length;
                    var count = 0;
                    foreach (String id in ids)
                        if (loc.bssids.Contains(id))
                            count++;
                    if (count > actMaxValue)
                    {
                        actMaxValue = count;
                        actMaxLoc.Clear();
                        actMaxLoc.Add(loc);
                    } else if (count == actMaxValue)
                        actMaxLoc.Add(loc);
                }

                if (actMaxValue > 0)
                {
                    if (actMaxLoc.Count == 1)
                        s = "Ihre aktuelle Position: " + actMaxLoc.Last().location + ". " + actMaxLoc.Last().describtion;
                    else if (actMaxLoc.Count > 1)
                    {
                        s = "Sie befinden sich zwischen den folgenden Bereichen: ";
                        foreach (Location l in actMaxLoc)
                        {
                            s = s + l.location + ", ";
                        }
                        s = s.Remove(s.Length - 2);
                    }
                } else
                    s = BSSIDs;
            }
            GameObject.Find("WifiPosition").GetComponent<Text>().text = s;
        }

        public void OnGameClick()
        {
            SceneManager.LoadScene(Config.SceneName(Config.Scenes.Game));
        }
    }
}