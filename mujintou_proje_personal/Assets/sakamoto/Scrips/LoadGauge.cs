using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class LoadGauge : MonoBehaviour
{
    [SerializeField] Image gauge;
    public float  countTime;

    // Start is called before the first frame update
    void Start()
    {
        gauge.fillAmount = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        gauge.fillAmount += 1.0f / countTime * Time.deltaTime;
    }
}
