using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public bool soundOn = true;

    public void Sounds_On()
    {
        AudioListener.volume = 1;
        soundOn = true;
        MainManager.Instance.GameSounds = true;
    }

    public void Sounds_Off()
    {
        AudioListener.volume = 0;
        soundOn = false;
        MainManager.Instance.GameSounds = false;
    }
}
