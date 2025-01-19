using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkipAndUseCJ : MonoBehaviour
{
    void Start()
    {
        PlayerInfo.Instance.Inventry.GetItem(Items.Item_ID.item_craft_coconutJuice, 1);
    }
    public void StashCJ()
    {
        PlayerInfo.Instance.Inventry.ClearSlot(0);
    }
}
