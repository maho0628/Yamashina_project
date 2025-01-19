using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Button60011 : MonoBehaviour
{
    Button button;
    void Start()
    {
        button = GetComponent<Button>();
        button.interactable =button.interactable &&(
PlayerInfo.Instance.Inventry.GetItemAmount(Items.Item_ID.item_mat_stone) >= 1);
    }
}
