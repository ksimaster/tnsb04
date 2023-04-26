using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selected_Bike : MonoBehaviour
{
    public GameObject[] MotorBikes;
    private int MotorBike_Number;

    // Start is called before the first frame update
    void Start()
    {
        MotorBike_Number = MainManager.Instance.Bike_Number;
        MotorBikes[MotorBike_Number].SetActive(true);
    }
}
