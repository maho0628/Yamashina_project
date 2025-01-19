using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button_60082 : MonoBehaviour
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
        button.interactable = PlayerInfo.Instance.Inventry.GetItemAmount(Items.Item_ID.item_mat_stone) >= 1;

        if (Itemget == false)
        {
            gameObject.GetComponent<Button>().onClick.AddListener(() =>
            { PlayerInfo.Instance.Inventry.UseItem(Items.Item_ID.item_mat_stone); });
            Debug.Log("Î‚ğÁ”ï‚µ‚½‚æ");
            if (!PlayerInfo.Instance.Inventry.GetNullSlot())
            {
                Debug.Log("ƒCƒ“ƒxƒ“ƒgƒŠ‚¢‚Á‚Ï‚¢");

                gameObject.GetComponent<Button>().onClick.AddListener(() => { PlayerInfo.Instance.Inventry.GetItem(Items.Item_ID.item_mat_stone, 1); });
                Debug.Log("Î‚ğ“üè‚µ‚½‚æ");

            }
            Debug.Log("Î‚ğ“üè‚µ‚½‚æ");

        }
        Itemget = true;

    }
}
