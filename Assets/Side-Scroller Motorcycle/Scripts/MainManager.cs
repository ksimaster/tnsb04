using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;

    public int Level_Index = 0;
    public int Bike_Number;
    public int Selected_Level;
    public int i = 0;
    public int Unlocked_Level = 0;

    public int Coins = 0;
    //public AudioSource Menu_BG_Sounds;

    //purchased bikes
    public int[] Purchased_Bikes = new int[4];
    public bool[] Purchased = new bool[4];

    public bool GameSounds = true;
    public int afterRewardCoins = 0;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        //SaveUserData();
        LoadUserData();
    }

    [System.Serializable]
    class SaveData
    {
        public int Level_Index;
        public int Unlocked_Level;
        public int[] unlocked_Levels = new int[19];
        public int coins;
        public int[] purchased_Bikes = new int[4];
        public bool[] purchased = new bool[4];
    }

    public void SaveUserData()
    {
        SaveData data = new SaveData();
        data.Unlocked_Level = Unlocked_Level;
        data.coins = Coins;
        //Saving the purchased bikes
        for (int i = 0; i < Purchased_Bikes.Length; i++)
        {
            data.purchased_Bikes[i] = Purchased_Bikes[i];
        }
        for (int i = 0; i < Purchased.Length; i++)
        {
            data.purchased[i] = Purchased[i];
        }

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadUserData()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        //Debug.Log("����: " + path);
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            Unlocked_Level = data.Unlocked_Level;
            Coins = data.coins;

            for (int i = 0; i < Purchased_Bikes.Length; i++)
            {
                Purchased_Bikes[i] = data.purchased_Bikes[i];
            }
            for (int i = 0; i < Purchased.Length; i++)
            {
                Purchased[i] = data.purchased[i];
            }
        }
    }

    public void SetRewardCoins()
    {
        if (PlayerPrefs.GetInt("ShowReward") == 1)
        {
            LoadUserData();
            Coins += 500;
            SaveUserData();
            PlayerPrefs.SetInt("ShowReward", 0);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }   
    }
    private void Update()
    {
        SetRewardCoins();
    }
}
