using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Wwew : MonoBehaviour
{
    [SerializeField]
    GameObject p;
    //[SerializeField]
    //Button button;
    private void Awake()
    {
        //button.onClick.AddListener(() => print("cliclk"));
        
        var g= Instantiate<GameObject>(p, GameObject.Find("Canvas").transform);
        g.GetComponent<ordertest>().Init();
    }   


}
