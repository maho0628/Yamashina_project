using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Option_button : MonoBehaviour
{
    //[SerializeField] public GameObject prefab;
    [SerializeField] public string sceneName;

    public void OnClick()
    {
       // Destroy(prefab);
        SceneManager.LoadScene(sceneName);

    }

}

