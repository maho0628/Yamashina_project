using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GoEnding : MonoBehaviour
{
    [SerializeField]
    bool isTrueEnd = true;

    private void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(() =>
        {
            if (isTrueEnd)
            {
                ((EventSceneControllerBase)GameObject.FindAnyObjectByType(typeof(EventSceneControllerBase))) .TrueEnd();
            }
            else
            {
                ((EventSceneControllerBase)GameObject.FindAnyObjectByType(typeof(EventSceneControllerBase))).BadEnd();
            }
            
        });
    }

}
