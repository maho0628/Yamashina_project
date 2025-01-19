using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button_60051 : MonoBehaviour
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
        //アイテム判別してボタンを消す
        if (PlayerInfo.Instance.Inventry.GetItemAmount(Items.Item_ID.item_craft_water) >= 1 || PlayerInfo.Instance.Inventry.GetItemAmount(Items.Item_ID.item_craft_water2) >= 1)
        {
            button.interactable = true;
        }
        else if (PlayerInfo.Instance.Inventry.GetItemAmount(Items.Item_ID.item_craft_water) < 1 || PlayerInfo.Instance.Inventry.GetItemAmount(Items.Item_ID.item_craft_water2) < 1)
        {
            button.interactable = false;
        }
        if (Itemget == false)
        {
            //使ったアイテムを減らす処理
            if (PlayerInfo.Instance.Inventry.GetItemAmount(Items.Item_ID.item_craft_water) >= 1)
            {
                gameObject.GetComponent<Button>().onClick.AddListener(() =>
                { PlayerInfo.Instance.Inventry.UseItem(Items.Item_ID.item_craft_water);
                    if (PlayerInfo.Instance.Inventry.UseItem(Items.Item_ID.item_craft_water))
                    {
                        PlayerInfo.Instance.EraseCondition(PlayerInfo.Condition.Poisoned);
                        ;


                    }
                });


                if (!PlayerInfo.Instance.Inventry.GetNullSlot())
                {
                    gameObject.GetComponent<Button>().onClick.AddListener(() => { PlayerInfo.Instance.Inventry.GetItem(Items.Item_ID.item_craft_water, 1); });
                }
            }

            else if (PlayerInfo.Instance.Inventry.GetItemAmount(Items.Item_ID.item_craft_water2) >= 1 && PlayerInfo.Instance.Inventry.GetItemAmount(Items.Item_ID.item_craft_water) < 1)
            {
                gameObject.GetComponent<Button>().onClick.AddListener(() =>
                { PlayerInfo.Instance.Inventry.UseItem(Items.Item_ID.item_craft_water2); });
                if (!PlayerInfo.Instance.Inventry.GetNullSlot())
                {
                    gameObject.GetComponent<Button>().onClick.AddListener(() => { PlayerInfo.Instance.Inventry.GetItem(Items.Item_ID.item_craft_water2, 1); });
                }
            }
        }
        Itemget = true;

    }

}
