using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIController : MonoBehaviour
{

    public UIDocument gui;
    public gps_pinger gps;
    // Start is called before the first frame update
    void Start()
    {
        if(gui == null)
        {
            gui = GetComponent<UIDocument>();
        }
        
        gui.rootVisualElement.Query<Label>("Output").ForEach(L => L.text = "0");
        gui.rootVisualElement.Add(new Button(() => { ping_GPS(); }) { text = "PING!" });
        if(gps != null)
        {
            gps.OnUpdateGPS.AddListener(UpdateUI_GPS);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ping_GPS()
    {
        if(gps == null)
        {
            gps = GetComponent<gps_pinger>();
            if(gps == null)
            {
                Debug.Log("No GPS Pinger found");
                return;
            }
        }
        StartCoroutine(gps.Init_GPS());
    }

    void UpdateUI_GPS()
    {
        if(gps == null)
        {
            return;
        }
        Label gps_lat = gui.rootVisualElement.Q<VisualElement>("Latitude").Q<Label>("Output");
        Label gps_long = gui.rootVisualElement.Q<VisualElement>("Longitude").Q<Label>("Output");
        Label gps_alt = gui.rootVisualElement.Q<VisualElement>("Altitude").Q<Label>("Output");
        Label gps_distance = gui.rootVisualElement.Q<VisualElement>("Distance").Q<Label>("Output");
        gps_lat.text = gps.gps_latitude.ToString();
        gps_long.text = gps.gps_longitude.ToString();
        gps_alt.text = gps.gps_altitude.ToString();
        gps_distance.text = gps_pinger.Haversine(new Vector2(gps.gps_latitude, gps.gps_longitude), Vector2.up * gps.gps_longitude).ToString();

    }
}

