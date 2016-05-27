import java.util.List;

import com.unity3d.player.UnityPlayer;

import android.content.Context;
import android.net.wifi.ScanResult;
import android.net.wifi.WifiManager;
import android.util.Log;

public class WifiScan {

	private static WifiScan wifiScan = null;

	private Context context = UnityPlayer.currentActivity.getBaseContext();
	private WifiManager wifi = (WifiManager) context
			.getSystemService(Context.WIFI_SERVICE);
	private String actBSSID = "initial-JavaString";
	private int actLevel;

	public static WifiScan getWifi() {
		if (wifiScan == null) {
			wifiScan = new WifiScan();
			wifiScan.wifi.startScan();
		}
		return wifiScan;
	}

	public String Scan() {
		actBSSID = "empty";
		actLevel = 0;
		List<ScanResult> results = wifi.getScanResults();
		wifi.startScan();
		try {
			if (results != null) {
				for (ScanResult sc : results) {
					if (sc.SSID == "FH-Visitor"
							&& (actBSSID == "empty" || sc.level > actLevel)) {
						actBSSID = sc.BSSID;
						actLevel = sc.level;
					}
				}
			}
		} catch (Exception e) {
			Log.e("GetWifis", "Error processing scan results", e);
		}
		return actBSSID;
	}
}
