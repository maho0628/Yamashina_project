using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class instantiateModal : MonoBehaviour
{
    [SerializeField] GameObject Modal;
    void Start()
    {
        Instantiate(Modal);
    }
    public void InstantiateObject()
    {
        Instantiate(Modal);
    }
}
