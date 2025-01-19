using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class OffTirgger : MonoBehaviour
{
    //[SerializeField] GameObject Trigger;
    [SerializeField] GameObject[] HLs;
    [SerializeField]  public List <EventTrigger> eventTriggers;
    [SerializeField] GameObject cover;
    

    private void Awake()
    {

        eventTriggers[1].enabled = true;

        eventTriggers[3].enabled = true;
        eventTriggers[5].enabled = true;
        //HLs[0].SetActive(true);

        //HLs[1].SetActive(true);
        ////HLs[2].SetActive(true);
        //HLs[3].SetActive(true);
        ////HLs[4].SetActive(true);
        //HLs[5].SetActive(true);

       // Debug.Log(PlayerInfo.Instance.Inventry.GetVisibility());   
    }
    // Update is called once per frame
    void Update()
    {
       // Debug.Log(PlayerInfo.Instance.Inventry.GetVisibility());
        //インベントリが開いていたら他のUIがおせないようにする処理
        if (PlayerInfo.Instance.Inventry.GetVisibility())
        {

            eventTriggers[0].enabled = false;

            eventTriggers[1].enabled = false;
            eventTriggers[2].enabled = false;

            eventTriggers[3].enabled = false;
            eventTriggers[4].enabled = false;
            eventTriggers[5].enabled = false;
            //Debug.Log(PlayerInfo.Instance.Inventry.gameObject.gameObject.activeSelf);

            cover.SetActive(true);
            //HLs.SetActive(false);   
        }
        if (!PlayerInfo.Instance.Inventry.GetVisibility())
        {
            eventTriggers[0].enabled = true;

            eventTriggers[1].enabled = true;
            eventTriggers[2].enabled = true;

            eventTriggers[3].enabled = true;
            eventTriggers[4].enabled = true;
            eventTriggers[5].enabled = true;

            cover.SetActive(false);
            //HLs.SetActive(true);


            //Debug.Log(PlayerInfo.Instance.Inventry.GetVisibility());

            //HLs.SetActive(true);
        }
        if (eventTriggers[1].enabled==false&&PlayerInfo.Instance.Inventry.GetVisibility())
        {
            HLs[1].SetActive(false);

        }
        if (eventTriggers[3].enabled == false && PlayerInfo.Instance.Inventry.GetVisibility())
        {
            HLs[3].SetActive(false);

        }
        if (eventTriggers[5].enabled == false && PlayerInfo.Instance.Inventry.GetVisibility())
        {
            HLs[5].SetActive(false);

        }
        //if (eventTriggers[5].enabled == true && !PlayerInfo.Instance.Inventry.GetVisibility())
        //{

        //}

    }
}
