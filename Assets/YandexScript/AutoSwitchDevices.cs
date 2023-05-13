using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoSwitchDevices : MonoBehaviour
{
    public GameObject TouchControls;
    //private int i = 0;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("desktop")) 
        {
            PlayerPrefs.SetInt("desktop", 0);
            CheckDevice();
        }

    }

    private void Update()
    {
        if(PlayerPrefs.GetInt("desktop") == 1) TouchControls.SetActive(false);
    }

    public void CheckDevice()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        if(WebGLPluginJS.GetTypeDevice() == "yes")
        {
            //desktop
            TouchControls.SetActive(false);
            PlayerPrefs.SetInt("desktop", 1);

        }
        else
        {
            //mobile, tablet, TV
            //TouchControls.SetActive(true);
        }
#endif
    }
}
