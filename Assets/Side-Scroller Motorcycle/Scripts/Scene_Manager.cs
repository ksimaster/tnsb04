using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Scene_Manager : MonoBehaviour
{
    public Text Coins_Text;
    public AudioSource Menu_BG_Audio;

    private void Start()
    {
        Menu_BG_Audio.Play();

        Coins_Text.text = MainManager.Instance.Coins.ToString();
    }

    public void Bike_Selection()
    {
        Application.LoadLevel("Bike_Selection");
    }

    public void Exit_Game()
    {
        Application.Quit();
        Debug.Log("Exit from game !");
    }

    public void Main_Menu()
    {
        Application.LoadLevel("Main_Menu");
    }

    public void GamePlay_Scene()
    {
        Application.LoadLevel("BikeStunt_Levels");
    }

    public void Like_Us()
    {
        Application.OpenURL("https://play.google.com/store/apps/dev?id=5360866347787636750");
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.LoadLevel("Main_Menu");
        }

        if(MainManager.Instance.GameSounds == true)
        {
            AudioListener.volume = 1;
        }
        else
        {
            AudioListener.volume = 0;
        }
    }
}
