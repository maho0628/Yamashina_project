using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using static Items;
using UnityEngine.UI;


public class Item_get_motion : Event_Text
{
    [SerializeField] Button Parent;
    [SerializeField] GameObject ItemPrefab;
    int item_ID;
    int get_Num;

    void getItems(int Item_ID, int get_num)
    {
        string Item_name;
        PlayerInfo.Instance.Inventry.GetItem((Items.Item_ID)Item_ID, get_num);
        Item_name = PlayerInfo.Instance.Inventry.GetItemName((Items.Item_ID)Item_ID);
        textControl.AddTextData($"{Item_name}を{get_num}つ手に入れました。");
        item_ID = Item_ID;
        get_Num = get_num;
        textControl.ClickEventAfterTextsEnd.AddListener(DisplayGetItem);

    }

    void DisplayGetItem()
    {
        StartCoroutine(displayGetItem());
    }
    IEnumerator displayGetItem()
    {
        for (int i = 0; i < get_Num; i++)
        {
            yield return new WaitForSeconds(0.2f);
            //生成位置
            Vector3 pos = new Vector3(570, -250 + 125f * i, 0.0f);
            //プレハブを指定位置に生成
            //Instantiate(ItemPrefab, pos, Quaternion.identity);
            GameObject obj = Instantiate(ItemPrefab, pos, Quaternion.identity);
            obj.transform.SetParent(Parent.image.canvas.gameObject.transform);
            obj.transform.localScale = Vector3.one;
            obj.transform.localPosition = pos;
            obj.GetComponent<Image>().sprite = SlotManager.GetItemData((Items.Item_ID)item_ID).icon;
            obj.transform.GetChild(0).gameObject.GetComponent<Text>().text = PlayerInfo.Instance.Inventry.GetItemAmount((Items.Item_ID)item_ID).ToString();
        }
        textControl.ClickEventAfterTextsEnd.RemoveAllListeners();

    }
}
