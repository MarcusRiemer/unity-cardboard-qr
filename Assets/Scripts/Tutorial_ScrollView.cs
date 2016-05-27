using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using System;
using System.Linq;

public class Tutorial_ScrollView : MonoBehaviour {

	public GameObject Button_Template;
	private List<string> LocationList = new List<string>();


	// Use this for initialization
	protected virtual IEnumerator Start () {
        // Load Locations from API
        var locationsWww = new WWW(Config.ApiUrlLocations);
        yield return locationsWww;
        GlobalState.Instance.AllLocations = JsonUtility.FromJson<Locations>(locationsWww.text);
        Array.Sort(GlobalState.Instance.AllLocations.locations);
        locationsWww.Dispose();

        foreach (Location loc in GlobalState.Instance.AllLocations.locations)
        {
            LocationList.Add(loc.location);
        }

		foreach(string str in LocationList)
		{
			GameObject go = Instantiate(Button_Template) as GameObject;
			go.SetActive(true);
			Tutorial_Button TB = go.GetComponent<Tutorial_Button>();
			TB.SetName(str);
			go.transform.SetParent(Button_Template.transform.parent);
		}

	}
	
	public void ButtonClicked(string str)
	{
        int x = 0;
        foreach (Location l in GlobalState.Instance.AllLocations.locations)
        {
            if (l.location == str)
            {
                GlobalState.Instance.CurrentDestination = x;
                GlobalState.Instance.NewLocation = true;
                break;

            }
            x = x + 1;
        }

    }
}
