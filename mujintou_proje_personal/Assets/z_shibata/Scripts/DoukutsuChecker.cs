using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DoukutsuChecker : MonoBehaviour
{
    [SerializeField] GameObject caveImage;
    private void OnEnable()
    {
        Debug.Log("enabled");
        string sceneName = SceneManager.GetActiveScene().name;
        if (sceneName == "Doukutu_fire" || sceneName == "Doukutu_light")
        {
            Debug.Log("ena002");
            caveImage.GetComponent<Image>().enabled = true;
            Debug.Log(caveImage.active);
        }
        else
        {
            Debug.Log("ena003");
            caveImage.GetComponent<Image>().enabled = false;
        }
    }
}
