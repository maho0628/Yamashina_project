using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DustButtonToDeath : MonoBehaviour
{
    GameObject inv;
    [SerializeField] GameObject massage;
    GameObject a;
    GameObject dustBox;
    [SerializeField] GameObject awayButton;
    bool once = false;

    [SerializeField] Items.Item_ID[] switchX;
    private void Start()
    {
        inv = PlayerInfo.Instance.Inventry.gameObject;
    }
    private void Update()
    {
        if (inv.active == true)
        {
            awayButton.SetActive(true);
            if (once == false)
            {
                FindDustBox();
            }
        }
        if(inv.active == false)
        {
            awayButton.SetActive(false);
        }
    }
    public void SetMassage()
    {
        a.SetActive(true);
    }
    void FindDustBox()
    {
        once = true;
        //awayButton = GameObject.Find("trowaway");
        dustBox = GameObject.Find("dustBox");
        awayButton.GetComponent<Button>().onClick.AddListener(() =>
        { 
            InstaMassage();
            GameObject.FindAnyObjectByType<anotherSoundPlayer>().GetComponent<anotherSoundPlayer>().ChooseSongs_SE(4);
        });
    }
    public void InstaMassage()
    {
        var b = dustBox.GetComponent<SlotManager>().GetSlotItem(0).Value.id;
        var c = Items.Item_ID.EmptyObject;
        
        if (b != c)
        {
            for (int i = 0; i < switchX.Length ; i++)
            {
                var a = switchX[i];
                if(b == a)
                {
                    Debug.Log(switchX);
                    Instantiate(massage);
                    return;
                }
            }
            dustBox.GetComponent<SlotManager>().ClearSlot(0);
        }
    }
}
