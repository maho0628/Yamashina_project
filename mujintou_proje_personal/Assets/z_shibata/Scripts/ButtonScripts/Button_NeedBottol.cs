using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Button_NeedBottol : MonoBehaviour
{
    Button button;
    private bool Itemget;
    private void Start()
    {
        Itemget = false;
        button = GetComponent<Button>();
    }
    private void Update()
    {



        button.interactable = PlayerInfo.Instance.Inventry.GetItemAmount(Items.Item_ID.item_mat_bottle) >= 1;
        if (Itemget == false)
        {
            gameObject.GetComponent<Button>().onClick.AddListener(() =>
            { PlayerInfo.Instance.Inventry.UseItem(Items.Item_ID.item_mat_bottle); });
            if (!PlayerInfo.Instance.Inventry.GetNullSlot())
            {
                gameObject.GetComponent<Button>().onClick.AddListener(() => { PlayerInfo.Instance.Inventry.GetItem(Items.Item_ID.item_mat_bottle, 1); });
            }

        }
        Itemget = true;


    }




}
    

