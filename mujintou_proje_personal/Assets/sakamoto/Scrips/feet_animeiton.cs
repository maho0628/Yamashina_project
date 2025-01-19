using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class feet_animeiton : MonoBehaviour
{
    [SerializeField] GameObject[] gameObjects;
    // Start is called before the first frame update
    void Start()
    {
        int rand =(int) Random.Range(0, 4.9f);
        gameObjects[rand].SetActive(true);
        Debug.Log(rand);
    }

    // Update is called once per frame
}
