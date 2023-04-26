using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public GameObject FinishPanel;
    public GameObject[] levelPanels;
    public AudioSource GamePlay_Music;
    //public Level_Selection level_Selection;

    private void Start()
    {
        GamePlay_Music.Play();

        if (MainManager.Instance.Level_Index > MainManager.Instance.Unlocked_Level)
        {
            MainManager.Instance.Unlocked_Level++;
            Debug.Log("Unlocked Level is Updading");
        }
        if (MainManager.Instance.i == 0)
        {
            levelPanels[MainManager.Instance.Selected_Level].SetActive(true);
            MainManager.Instance.i++;
            //MainManager.Instance.SaveUserData();
        }
        else
        {
            for (int i = 0; i < levelPanels.Length; i++)
            {
                levelPanels[i].SetActive(false);
            }
            levelPanels[MainManager.Instance.Level_Index].SetActive(true);
            MainManager.Instance.SaveUserData();
        }

        if (MainManager.Instance.GameSounds == true)
        {
            AudioListener.volume = 1;
        }
        else
        {
            AudioListener.volume = 0;
        }
    }

    public void ReloadLevel()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

    public void NextLevel()
    {
        Motorcycle_Controller.isControllable = true;
        MainManager.Instance.Level_Index++;

        if (MainManager.Instance.Level_Index >= levelPanels.Length)
        {
            MainManager.Instance.Level_Index = 0;
        }
        SceneManager.LoadScene("BikeStunt_Levels");
        //for (int i = 0; i < levelPanels.Length; i++)
        //{
        //    if (i == MainManager.Instance.Level_Index)
        //    {
        //        //Application.LoadLevel(Application.loadedLevel);
        //        SceneManager.LoadScene("BikeStunt_Levels");
        //        FinishPanel.SetActive(false);
        //    }
        //    else
        //    {
        //        levelPanels[MainManager.Instance.Level_Index].SetActive(false);
        //    }
        //}
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

    //public void Unlock_Next_Level()
    //{
    //    Debug.Log(MainManager.Instance.Unlocked_Level);
    //    if(MainManager.Instance.Level_Index >= MainManager.Instance.Unlocked_Level)
    //    {
    //        MainManager.Instance.Unlocked_Level++;
    //        Debug.Log("Unlocked Level is Updading");
    //    }
    //}

}
