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
            return;
        var gps_lat = gui.rootVisualElement.Q<VisualElement>("Latitude").Q<Label>("Output");
        gps_lat.text = gps.gps_latitude.ToString();

    }
}

