using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class gps_pinger : MonoBehaviour
{
    public int gps_timeout = 20;
    public float gps_altitude = 0;
    public float gps_latitude = 0;
    public float gps_longitude = 0;

    public UnityEvent OnUpdateGPS;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnGUI()
    {
        GUIStyle style = new GUIStyle();
        style.normal.textColor = Color.white;
        GUILayout.Label("GPS Longitude: " + gps_longitude.ToString(),style);
        GUILayout.Label("GPS Latitude: " + gps_latitude.ToString(),style);
        GUILayout.Label("GPS Altitude: " + gps_altitude.ToString(),style);
        GUILayout.Label(string.Format("GPS Status: {0}", Input.location.status),style);
    }

    public IEnumerator Init_GPS()
    {
        Input.location.Start();
        int timeout = gps_timeout;
        while (Input.location.status == LocationServiceStatus.Initializing && timeout > 0)
        {
            Debug.Log(string.Format("GPS Initializing...{0}", timeout.ToString()));
            yield return new WaitForSeconds(1);
            timeout--;
        }
        if (timeout < 1)
        {
            Debug.Log("GPS Timed out");
            yield break;
        }
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            Debug.Log("Unable to determine device location");
            yield break;
        }
        
        if(Input.location.status != LocationServiceStatus.Running)
        {
            Debug.Log("GPS not running");
            yield break;
        }
        Debug.Log(Input.location.status.ToString());
        gps_altitude = Input.location.lastData.altitude;
        gps_latitude = Input.location.lastData.latitude;
        gps_longitude = Input.location.lastData.longitude;
        Debug.Log("Altitude: " + gps_altitude.ToString("F3.4"));
        Debug.Log("Latitude: " + gps_latitude.ToString("F3.4"));
        Debug.Log("Longitude: " + gps_longitude.ToString("F3.4"));

        OnUpdateGPS.Invoke();
        Input.location.Stop();
    }
}
