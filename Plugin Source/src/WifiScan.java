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

	public static WifiScan getWifi() {
		if (wifiScan == null) {
			wifiScan = new WifiScan();
			wifiScan.wifi.startScan();
		}
		return wifiScan;
	}

	public String Scan() {
		String BSSID = "";
		List<ScanResult> results = wifi.getScanResults();
		wifi.startScan();
		try {
			if (results != null) 
				for (ScanResult sc : results) 
					if (sc.level >= -80)
						BSSID = BSSID + " " + sc.BSSID;

		} catch (Exception e) {
			Log.e("GetWifis", "Error processing scan results", e);
		}
		return BSSID;
	}
}
