using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

//[RequireComponent(typeof(Button))]
public class UnityEventTest : MonoBehaviour
{

    [SerializeField]
    Button button;

    // Start is called before the first frame update
    void Start()
    {
    //      button = GetComponent<Button>();

        button.onClick.AddListener(PushButton);
      //  button.onClick.AddListener(PlayerInfo.Instance.OnStartDrag);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PushButton()
    {
        print("Button Down");
        //button.onClick.RemoveListener(PushButton);

    }

    void PushButtonSecond()
    {
        print("second push  ");
        //button.onClick.RemoveAllListeners();
    }
}
