using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class goBase_Tutorial : MonoBehaviour
{
    [SerializeField]
    SceneObject scene;
    EventSceneControllerBase event_controller;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(() =>
        { SceneManager.LoadScene(scene); });
    }

}
