using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bike_Selection : MonoBehaviour
{
    public GameObject[] bikes;
    public GameObject Buy_Button;
    public GameObject Select_Button;
    public GameObject[] Purchase_Texts;
    public GameObject NotEnoughMoney;
    public int[] Prices;
    public int currentBike = 0;
    public GameObject[] stats;
    public int Stats = 0;

    //public bool[] purchased = new bool[4];
    public int[] purchased_Bikes = new int[4];

    void Start()
    {
        for(int j = 0; j < 4; j++)
        {
            if(MainManager.Instance.Purchased_Bikes[j] != 0)
            {
                Buy_Button.SetActive(false);
                Select_Button.SetActive(true);
            }
            else
            {
                //Debug.Log("Bike" + j + "Purchased");
                Buy_Button.SetActive(true);
                Select_Button.SetActive(false);
            }
        }
        ShowBike();
    }

    void Update()
    {
        for (int j = 0; j < 4/*MainManager.Instance.Purchased_Bikes.Length*/; j++)
        {
            //Debug.Log("Loop executing time" + j);
            if (currentBike > 0 && MainManager.Instance.Purchased[currentBike - 1] == false && MainManager.Instance.Purchased_Bikes[j] == 0)
            {
                Buy_Button.SetActive(true);
                Select_Button.SetActive(false);

                for (int i = 0; i < Purchase_Texts.Length; i++)
                {
                    if (i == currentBike - 1)
                    {
                        Purchase_Texts[i].SetActive(true);
                    }
                    else
                    {
                        Purchase_Texts[i].SetActive(false);
                    }
                }
            }
            else
            {
                Buy_Button.SetActive(false);
                Select_Button.SetActive(true);
            }
        }
    }

    public void Left_Bike()
    {
        currentBike--;
        Stats--;
        if (currentBike < 0 && Stats < 0)
        {
            currentBike = bikes.Length - 1;
            Stats = bikes.Length - 1;
        }
        ShowBike();
    }

    public void Right_Bike()
    {
        currentBike++;
        Stats++;
        if (currentBike == bikes.Length && Stats == bikes.Length)
        {
            currentBike = 0;
            Stats = 0;
        }
        ShowBike();
    }

    void ShowBike()
    {
        for (int i = 0; i < bikes.Length; i++)
        {
            if (i == currentBike && i == Stats)
            {
                bikes[i].SetActive(true);
                stats[i].SetActive(true);
                MainManager.Instance.Bike_Number = currentBike;
                Debug.Log("Current Active Bike is: " + currentBike);
            }
            else
            {
                bikes[i].SetActive(false);
                stats[i].SetActive(false);
            }
        }
    }

    public void Purchase_Bike()
    {
        PurchasedBike();
    }

    public void PurchasedBike()
    {
        if(MainManager.Instance.Coins >= Prices[currentBike-1])
        {
            MainManager.Instance.Coins -= Prices[currentBike - 1];
            for (int i = 0; i <= MainManager.Instance.Purchased_Bikes.Length; i++)
            {
                if (i == currentBike)
                {
                    MainManager.Instance.Purchased_Bikes[i - 1] = currentBike;
                }
            }
            purchased_Bikes[currentBike - 1] = currentBike;
            Scene_Manager.FindObjectOfType<Scene_Manager>().Coins_Text.text = MainManager.Instance.Coins.ToString();
            MainManager.Instance.Purchased[currentBike - 1] = true;
            //purchased[currentBike - 1] = true;
            MainManager.Instance.SaveUserData();
        }
        else
        {
            NotEnoughMoney.SetActive(true);
        }
    }
}
