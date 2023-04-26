using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bike_Selection_Audio : MonoBehaviour
{
    public AudioSource Bike_Selection_BG_Audio;
    public GameObject Panels;
    
    // Update is called once per frame
    void Update()
    {
        if(Panels.activeSelf == true)
        {
            Bike_Selection_BG_Audio.Play();
        }
    }
}
