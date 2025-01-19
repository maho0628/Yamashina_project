using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChekHaveFlash : MonoBehaviour
{
    [SerializeField] GameObject goButton;
    [SerializeField] GameObject cantCover;
    void Update()
    {
        if(PlayerInfo.Instance.Inventry.GetItemAmount(Items.Item_ID.item_special_flash) >= 1 || PlayerInfo.Instance.Inventry.GetItemAmount(Items.Item_ID.item_craft_torch) >= 1)
        {
            goButton.SetActive(true);
            if(PlayerInfo.Instance.ActionValue > 0) { cantCover.SetActive(false);}
            else { cantCover.SetActive(true);}
        }
        else
        {
            goButton.SetActive(false);
        }
    }
}
