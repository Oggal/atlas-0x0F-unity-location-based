using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class gps_pinger : MonoBehaviour
{
    public int gps_timeout = 20;
    public Vector3 gps_location;
    public float gps_accuracy;
    public float gps_altitude;
    public float gps_latitude;
    public float gps_longitude;

    public UnityEvent OnUpdateGPS;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
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
        
        gps_altitude = Input.location.lastData.altitude;
        gps_latitude = Input.location.lastData.latitude;
        gps_longitude = Input.location.lastData.longitude;
        Debug.Log("Altitude: " + gps_altitude.ToString());
        Debug.Log("Latitude: " + gps_latitude.ToString());
        Debug.Log("Longitude: " + gps_longitude.ToString());
        OnUpdateGPS.Invoke();
    
        Input.location.Stop();
    }
}
