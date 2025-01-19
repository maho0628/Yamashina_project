using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorial : MonoBehaviour
{
    [SerializeField] GameObject tutorialPrefab;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerInfo.Instance.GetdoukutuCheck())
        {
            Instantiate(tutorialPrefab);
            PlayerInfo.Instance.SetdoukutuCheck(false);
        }
        
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
