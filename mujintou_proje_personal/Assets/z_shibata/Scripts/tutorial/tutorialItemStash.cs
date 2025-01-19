using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorialItemStash : MonoBehaviour
{
    void Start()
    {
        PlayerInfo.Instance.Inventry.UseItem(Items.Item_ID.item_mat_stone);
        PlayerInfo.Instance.Inventry.UseItem(Items.Item_ID.item_mat_stone);
        PlayerInfo.Instance.Inventry.UseItem(Items.Item_ID.item_mat_stone);
        PlayerInfo.Instance.Inventry.UseItem(Items.Item_ID.item_mat_stone);
        PlayerInfo.Instance.Inventry.UseItem(Items.Item_ID.item_mat_stone);

        PlayerInfo.Instance.Inventry.UseItem(Items.Item_ID.item_mat_stone);
        PlayerInfo.Instance.Inventry.UseItem(Items.Item_ID.item_mat_stone);


        PlayerInfo.Instance.Inventry.UseItem(Items.Item_ID.Plank);


        PlayerInfo.Instance.Inventry.UseItem(Items.Item_ID.Butterfly);
    }
}
