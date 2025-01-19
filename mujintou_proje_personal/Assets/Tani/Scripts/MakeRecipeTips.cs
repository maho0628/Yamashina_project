using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MakeRecipeTips : MakeFloatWindow
{
    [SerializeField]
    Items.Item_ID id;
    protected override void Awake()
    {
        EventTrigger eventTrigger = GetComponent<EventTrigger>();
        EventTrigger.Entry[] entries = { new EventTrigger.Entry(), new EventTrigger.Entry() };
        print(entries[0]);
        entries[0].eventID = EventTriggerType.PointerEnter;
        entries[1].eventID = EventTriggerType.PointerExit;

        entries[0].callback.AddListener(_ =>
        {
            if (createdObject) return;

            GameObject c_obj = new GameObject("FloatingWindowCanvas");
            Canvas c = c_obj.AddComponent<Canvas>();
            c_obj.AddComponent<CanvasRenderer>();
            c_obj.AddComponent<CanvasScaler>();
            c.overrideSorting = true;
            c.sortingOrder = canvasOrder;
            c.renderMode = RenderMode.ScreenSpaceOverlay;
            createdObject = Instantiate(window, c_obj.transform);
            createdObject.transform.position = this.gameObject.transform.position + offset;
            createdObject.transform.parent.GetComponent<Canvas>().sortingOrder = 6;
            var item_data = SlotManager.GetItemData(id);
            createdObject.GetComponentInChildren<Text>().text =
                                                    (SlotManager.GetItemData(id).item_name.GetLocalizedString() + "\n" +
                                                    "‘Ì—Í :" +item_data.Health_Change ) + "\n" +
                                                    "H—¿ :" +item_data.Hunger_Change + "\n" +
                                                    "…•ª :" +item_data.Thirst_Chage + "\n" + 
                                                    item_data.extra_effect.GetLocalizedString();
            if(item_data.extra_effect.GetLocalizedString() == "")
            {
                return;
            }
        });
        entries[1].callback.AddListener(_ =>
        {
            if (!createdObject) return;
            Destroy(createdObject.transform.parent.gameObject);
            createdObject = null;

        });

        for (int i = 0; i < entries.Length; i++)
        {
            eventTrigger.triggers.Add(entries[i]);
        }
    }
}
