using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ordertest : MonoBehaviour
{
    public void Init()
    {
        print("init");
    }
    private void Awake()
    {
        print("awake");
    }
    private void OnEnable()
    {
        print("enabl,e");
    }
    private void Start()
    {
        print("start");
    }
}
