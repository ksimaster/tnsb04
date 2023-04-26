using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coins : MonoBehaviour
{
    public Text Coins_Text;
    // Start is called before the first frame update
    void Start()
    {
        Coins_Text.text = MainManager.Instance.Coins.ToString();
    }
}
