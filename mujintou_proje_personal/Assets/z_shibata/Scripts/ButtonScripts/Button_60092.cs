using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button_60092 : MonoBehaviour
{
    Button button;
    private bool Itemget;

    void Start()
    {
        Itemget = false;

        button = GetComponent<Button>();
           }

    private void Update()
    {
        button.interactable = PlayerInfo.Instance.Inventry.GetItemAmount(Items.Item_ID.item_craft_bakedBird) >= 1;

        if (Itemget == false)
        {

            gameObject.GetComponent<Button>().onClick.AddListener(() =>
            { PlayerInfo.Instance.Inventry.UseItem(Items.Item_ID.item_craft_bakedBird); });
            if (!PlayerInfo.Instance.Inventry.GetNullSlot())
            {
                gameObject.GetComponent<Button>().onClick.AddListener(() => { PlayerInfo.Instance.Inventry.GetItem(Items.Item_ID.item_craft_bakedBird, 1); });
            }

        }
        Itemget = true;
    }
       
    
}
