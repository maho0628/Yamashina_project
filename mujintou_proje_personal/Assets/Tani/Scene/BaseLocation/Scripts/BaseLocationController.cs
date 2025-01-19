using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class BaseLocationController : MonoBehaviour
{
    [SerializeField]
    List<GameObject> initial_Instancing_Objects;

    private void Awake()
    {
        
    }
    private void Start()
    {
        foreach (var item in initial_Instancing_Objects)
        {
            Instantiate(item);
        }
    }

}
