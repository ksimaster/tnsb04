using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ADScript : MonoBehaviour
{
    //public Slider sliderHome;
    //public Slider sliderFuelCar;
    //public float rewardBonusSliderHome;
    //public float rewardBonusSliderFuel;
    //public float lowBalanceFuel;
    public string nameScene;
    public GameObject panelLoose;
    public GameObject panelWin;
    //public GameObject panelReward;
    // public Text textCoin;
    public GameObject adsWinError;
    public GameObject adsLoseError;
    public TMP_Text adsWinText;
    public TMP_Text adsLoseText;
    public Button restartWin;
    public Button nextWin;
    public Button restartLose;
    public Button outScene;
    //private int coin;
    private int i;

    IEnumerator Pause()
    {
        restartWin.gameObject.SetActive(false);
        nextWin.gameObject.SetActive(false);
        restartLose.gameObject.SetActive(false);
        outScene.gameObject.SetActive(false);
        adsWinText.text = "�������! ���������� ��������� ������ � ������!";
        adsLoseText.text = "������ ������� � ����� ����������� ��� ���!";
        yield return new WaitForSeconds(0.5f);
        adsWinText.text = "�������! ���������� ��������� ������ � ������!";
        adsLoseText.text = "������ ������� � ����� ����������� ��� ���!";
        yield return new WaitForSeconds(1f);
        adsWinText.text = "�������! ���������� ��������� ������ � ������!";
        adsLoseText.text = "������ ������� � ����� ����������� ��� ���!";
        yield return new WaitForSeconds(1f);
        adsWinText.text = "������ ������! ����� �����!";
        adsLoseText.text = "�� ������! ����� �����!";
        yield return new WaitForSeconds(1f);
#if UNITY_WEBGL && !UNITY_EDITOR
    	WebGLPluginJS.InterstitialFunction();
#endif
        //CheckAdsWork();
       // adsWinText.text = "";
        //adsLoseText.text = "";
        restartWin.gameObject.SetActive(true);
        nextWin.gameObject.SetActive(true);
        restartLose.gameObject.SetActive(true);
        outScene.gameObject.SetActive(true);
    }
    IEnumerator MicroPause()
    {
        
        yield return new WaitForSeconds(0.3f);
        
    }
    public void ShareFriend(){
#if UNITY_WEBGL && !UNITY_EDITOR
        WebGLPluginJS.ShareFunction();
#endif
    }

    public void ShowAdInterstitial(){
#if UNITY_WEBGL && !UNITY_EDITOR
    	WebGLPluginJS.InterstitialFunction();
#endif
    }

    public void ShowAdInterstitialLogPause()
    {
        StartCoroutine("Pause");

    }

    public void ShowAdReward(){
#if UNITY_WEBGL && !UNITY_EDITOR
    	WebGLPluginJS.RewardFunction();
#endif
       // panelReward.SetActive(true);
       /*
        coin = int.Parse(textCoin.text);
        coin += 500;
        textCoin.text = coin.ToString();
        */
        PlayerPrefs.SetInt("ShowReward", 1);

        // sliderHome.value += rewardBonusSliderHome;
        //if(sliderFuelCar.value<=lowBalanceFuel) sliderFuelCar.value += rewardBonusSliderFuel;
    }

    //Change language

    public void SetEnglish(string message)
    {
#if UNITY_WEBGL && !UNITY_EDITOR
    	PlayerPrefs.SetString("lang", message);
#endif

    }
    private void Start()
    {
        i = 0;
        if (nameScene == "Menu") ShowAdInterstitial();
        
    }
    private void Update()
    {
        CheckAds();
        //CheckAdsWork();
        //if (sliderHome.value <= sliderHome.minValue) ShowAdInterstitial();
        
        if ((panelLoose.activeSelf || panelWin.activeSelf) && i == 0) 
        {
           // StartCoroutine("MicroPause");
            i+=1;
            ShowAdInterstitialLogPause();
        }
        if (panelWin.activeSelf) panelLoose.SetActive(false);
    }
    private void OnApplicationFocus(bool focus)
    {
        if (focus)
        {
            AudioListener.pause = false;

        }
        else
        {
            AudioListener.pause = true;
        }
    }

    public void OnApplicationPause(bool pause)
    {
        if (!pause)
        {
            AudioListener.pause = false;
        }
        else
        {
            AudioListener.pause = true;
        }
    }

    public void CheckAds()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        if(WebGLPluginJS.GetAdsOpen() == "yes")
        {
            AudioListener.pause = true;
        }
        else
        {
            AudioListener.pause = false;
        }
#endif
    }
    /*
    public void CheckAdsWork()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        if(WebGLPluginJS.GetAdsWork() == "yes")
        {
            adsWinError.SetActive(false);
            adsLoseError.SetActive(false);
        }
        else
        {
            adsWinError.SetActive(true);
            adsLoseError.SetActive(true);
        }
#endif
    }
    */
}
