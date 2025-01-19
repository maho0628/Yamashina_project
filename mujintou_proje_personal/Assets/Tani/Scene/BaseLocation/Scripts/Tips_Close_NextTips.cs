using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tips_Close_NextTips : MonoBehaviour
{
    [SerializeField]
    Button button;
    [SerializeField]
    GameObject nextTips;
    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(() =>
        {
            Instantiate(nextTips);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
