using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TweenWork : MonoBehaviour
{
    public GameObject tweenObj;
    bool intrig;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")&&!intrig)
        {
            intrig = true;
            tweenObj.GetComponent<DOTweenAnimation>().DORestart();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            intrig = false;
        }
    }
}
