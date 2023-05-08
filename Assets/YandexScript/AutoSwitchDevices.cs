using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoSwitchDevices : MonoBehaviour
{
    public GameObject TouchControls;
    private int i = 0;

    private void Update()
    {
            CheckDevice(); 
    }

    public void CheckDevice()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        if(WebGLPluginJS.GetTypeDevice() == "yes")
        {
            //desktop
            TouchControls.SetActive(false);
        }
        else
        {
            //mobile, tablet, TV
            //TouchControls.SetActive(true);
        }
#endif
    }
}
