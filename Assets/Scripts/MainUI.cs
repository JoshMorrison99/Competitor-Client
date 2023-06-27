using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainUI : MonoBehaviour
{
    public GameObject TopPanel;
    public GameObject TabBar;
    public GameObject Activity;
    public GameObject Run;
    public GameObject ActiveRun;
    public GameObject Leaderboards;
    public GameObject Profile;
    public GameObject Settings;

    public TMP_Text activity_text;
    public TMP_Text run_text;
    public TMP_Text rank_text;

    public int red = 228;     // Red component (0-255)
    public int green = 151;     // Green component (0-255)
    public int blue = 0;      // Blue component (0-255)



    public void Start()
    {
        TopPanel.SetActive(true);
        TabBar.SetActive(true);

        OnActivityClicked();
        
    }

    public void OnActivityClicked()
    {
        Activity.SetActive(true);

        Run.SetActive(false);
        ActiveRun.SetActive(false);
        Leaderboards.SetActive(false);
        Profile.SetActive(false);
        Settings.SetActive(false);

        // Update text color
        activity_text.color = new Color32((byte)red, (byte)green, (byte)blue, 255);
        run_text.color = Color.white;
        rank_text.color = Color.white;
    }

    public void OnLeaderboardsClicked()
    {
        Leaderboards.SetActive(true);

        Run.SetActive(false);
        ActiveRun.SetActive(false);
        Activity.SetActive(false);
        Profile.SetActive(false);
        Settings.SetActive(false);

        // Update text color
        activity_text.color = Color.white;
        run_text.color = Color.white;
        rank_text.color = new Color32((byte)red, (byte)green, (byte)blue, 255);
    }

    public void OnProfileClicked()
    {
        Profile.SetActive(true);

        Run.SetActive(false);
        ActiveRun.SetActive(false);
        Leaderboards.SetActive(false);
        Activity.SetActive(false);
        Settings.SetActive(false);
    }


    public void OnRunClicked()
    {
        Run.SetActive(true);

        Activity.SetActive(false);
        ActiveRun.SetActive(false);
        Leaderboards.SetActive(false);
        Profile.SetActive(false);
        Settings.SetActive(false);

        // Update text color
        activity_text.color = Color.white;
        run_text.color = new Color32((byte)red, (byte)green, (byte)blue, 255);
        rank_text.color = Color.white;
    }

    public void OnSettingsClicked()
    {
        Settings.SetActive(true);

        Run.SetActive(false);
        ActiveRun.SetActive(false);
        Leaderboards.SetActive(false);
        Profile.SetActive(false);
        Activity.SetActive(false);
    }

    public void OnSoloRunClicked()
    {
        ActiveRun.SetActive(true);
        TopPanel.SetActive(false);
        TabBar.SetActive(false);

        Run.SetActive(false);
        Settings.SetActive(false);
        Leaderboards.SetActive(false);
        Profile.SetActive(false);
        Activity.SetActive(false);
    }

    public void OnStopSoloRunClicked()
    {
        Debug.Log("Display Run Stats");
    }
    public void OnResumeSoloRunClicked()
    {
        Debug.Log("Continue Run");
    }

    public void OnPauseSoloRunClicked()
    {
        Debug.Log("Pause Run");
    }
}
