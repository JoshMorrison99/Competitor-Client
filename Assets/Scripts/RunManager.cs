using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RunManager : MonoBehaviour
{

    public int time;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GPSLocation());
    }

    IEnumerator GPSLocation()
    {
        Debug.Log("[*] Starting GPS Service: " + Input.location.status);
        // Check if user has location service enabled
        if (!Input.location.isEnabledByUser)
        {
            Debug.LogError("[!] Location Service is Not Enabled");
            yield break;
        }

        // Start the location service
        Input.location.Start();

        // Wait until service is initialized
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0) 
        {
            yield return new WaitForSeconds(1);
            Debug.Log("[!] Wait time: " + maxWait);
            maxWait--;
        }

        // Service didn't initialize in 20 seconds
        if(maxWait < 1)
        {
            Debug.LogWarning("[!] Timed Out");
            yield break;
        }

        // Connection failed
        if(Input.location.status == LocationServiceStatus.Failed)
        {
            Debug.LogWarning("[!] Unable to determine device location");
            yield break;
        }
        else
        {
            // Connection success
            InvokeRepeating("UpdateGPS", 0.5f, 1f);
        }
    }

    private void UpdateGPS()
    {
        if (Input.location.status == LocationServiceStatus.Running)
        {
            Debug.Log("[*] Run Started");

            // Format the time into hours, minutes, and seconds
            int hours = Mathf.FloorToInt(time / 3600f);
            int minutes = Mathf.FloorToInt((time - (hours * 3600f)) / 60f);
            int seconds = Mathf.FloorToInt(time - (hours * 3600f) - (minutes * 60f));

            // Display the time in the "hh:mm:ss" format
            //text_time.text = hours.ToString("00") + ":" + minutes.ToString("00") + ":" + seconds.ToString("00");
        }
        else
        {
            // GPS Location Service Stopped 
            Debug.Log("[*] Run Stopped");
        }
    }
}
