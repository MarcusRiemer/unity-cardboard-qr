using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using UnityEngine;

namespace Assets.Scripts {

    public class Tracker {


        /**public void scan() {
            while (GlobalState.Instance.getTracker())
            {
                AndroidJavaClass ajc = new AndroidJavaClass("WifiScan");
                AndroidJavaObject ajo = ajc.CallStatic<AndroidJavaObject>("getWifi");
                GlobalState.Instance.setBSSID(ajo.Call<string>("getStatus"));
                //GlobalState.Instance.setBSSID(GlobalState.Instance.getWifi().Call<String>("Scan"));
                Thread.Sleep(10000);
            }
        }*/
    }
}
