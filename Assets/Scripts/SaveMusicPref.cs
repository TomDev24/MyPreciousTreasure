using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveMusicPref : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        bool musSettings = intToBool(PlayerPrefs.GetInt("Music", 1));
        GetComponent<Toggle>().isOn = musSettings;
        GameManager.instance.MusicToggle(musSettings);
    }

    // Update is called once per frame
    void Update()
    {
        if (intToBool(PlayerPrefs.GetInt("Music", 1)))
            GetComponent<Toggle>().isOn = true;
        else
            GetComponent<Toggle>().isOn = false;
    }

    bool intToBool(int val)
    {
        if (val != 0)
            return true;
        else
            return false;
    }
}
