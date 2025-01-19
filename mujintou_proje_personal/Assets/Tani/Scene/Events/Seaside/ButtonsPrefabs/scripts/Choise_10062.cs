using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Choise_10062 : MonoBehaviour
{
    Button button;
    private void Start()
    {
        button = GetComponent<Button>();
        button.interactable = button.interactable &&
            (PlayerInfo.Instance.Inventry.GetItemAmount(Items.Item_ID.item_craft_DIYknife) >= 1
            || PlayerInfo.Instance.Inventry.GetItemAmount(Items.Item_ID.item_special_knife) >= 1);
        
    }

}
