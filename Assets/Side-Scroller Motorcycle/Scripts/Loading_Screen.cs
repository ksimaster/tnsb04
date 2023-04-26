using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Loading_Screen : MonoBehaviour
{
    public Slider loadingSlider;
    public float loadingTime = 5f;
    public GameObject Loading_Panel;
    public GameObject Level_Selection_Panel;
    public GameObject Settings_Button;
    public GameObject Coin_Bar;

    private void Start()
    {
        StartCoroutine(Loading());
    }

    private IEnumerator Loading()
    {
        float progress = 0f;

        while (progress < loadingTime)
        {
            progress += Time.deltaTime;
            loadingSlider.value = progress / loadingTime;
            yield return null;
        }

        yield return new WaitForSeconds(1.5f);
        Loading_Panel.SetActive(false);
        Level_Selection_Panel.SetActive(true);
        Settings_Button.SetActive(true);
        Coin_Bar.SetActive(true);
    }
}
