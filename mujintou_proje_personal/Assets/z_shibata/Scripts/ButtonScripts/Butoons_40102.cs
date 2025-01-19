using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Butoons_40102 : MonoBehaviour
{
    Button button;

    void Start()
    {
        button = GetComponent<Button>();
        button.interactable = PlayerInfo.Instance.Inventry.GetItemAmount(Items.Item_ID.item_craft_torch) >= 1 || PlayerInfo.Instance.Inventry.GetItemAmount(Items.Item_ID.item_special_flash) >= 1;

    }
}
