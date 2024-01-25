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
    public LocationInfo gps_locationA, gps_locationB;
    public float gps_distance = 0;

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


    public static float Haversine(LocationInfo pointA, LocationInfo pointB)
    {
        float R = 6371e3f; // Earth's radius in meters
        float Lat1 = pointA.latitude * Mathf.Deg2Rad;  // convert lat to radians
        float Lat2 = pointB.latitude * Mathf.Deg2Rad;  // convert lat to radians
        float deltaLat = (pointB.latitude - pointA.latitude) * Mathf.Deg2Rad; // convert delta lat to radians
        float deltaLon = (pointB.longitude - pointA.longitude) * Mathf.Deg2Rad; // convert delta long to radians

        float a = Mathf.Sin(deltaLat / 2) * Mathf.Sin(deltaLat / 2) +
            Mathf.Cos(Lat1) * Mathf.Cos(Lat2) *
            Mathf.Sin(deltaLon / 2) * Mathf.Sin(deltaLon / 2);
        float c = 2 * Mathf.Atan2(Mathf.Sqrt(a), Mathf.Sqrt(1 - a));


        return R * c;
    }

    public static float Haversine(Vector2 pointA, Vector2 pointB)
    {
        float R = 6371e3f; // Earth's radius in meters
        float Lat1 = pointA.x * Mathf.Deg2Rad;  // convert lat to radians
        float Lat2 = pointB.x * Mathf.Deg2Rad;  // convert lat to radians
        float deltaLat = (pointB.x - pointA.x) * Mathf.Deg2Rad; // convert delta lat to radians
        float deltaLon = (pointB.y - pointA.y) * Mathf.Deg2Rad; // convert delta long to radians

        float a = Mathf.Sin(deltaLat / 2) * Mathf.Sin(deltaLat / 2) +
            Mathf.Cos(Lat1) * Mathf.Cos(Lat2) *
            Mathf.Sin(deltaLon / 2) * Mathf.Sin(deltaLon / 2);
        float c = 2 * Mathf.Atan2(Mathf.Sqrt(a), Mathf.Sqrt(1 - a));

        return R * c;
    }

}
