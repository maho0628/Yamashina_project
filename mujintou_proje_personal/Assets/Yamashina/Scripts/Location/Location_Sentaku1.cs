using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Location_Sentaku1 : MonoBehaviour
{
    [SerializeField] public GameObject image;

    public void Start()
    {
        image.SetActive(false);
    }

    private void OnMouseOver()
    {
        image.SetActive(true);
    }


    private void OnMouseExit()
    {
        image.SetActive(false);

    }
}
