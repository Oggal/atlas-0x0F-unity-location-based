using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosUpdater : MonoBehaviour
{
    public Vector2 GPSCords;
    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<gps_pinger>().OnUpdateCoords.AddListener(OnUpdateCoords);
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    void OnUpdateCoords()
    {
        var pos = GPSEncoder.GPSToUCS(GPSCords);
        transform.localPosition = new Vector3(pos.x, 0, pos.z);
    }
}
