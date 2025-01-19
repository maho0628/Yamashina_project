using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CraftPanel : PanelBase
{
    [SerializeField]
    CraftSlots craftslots;
    [SerializeField]
    List<Text> discriptions;
    [SerializeField]
    Button close_button;
    [SerializeField] CraftSlots slotManager;
    protected override  void Awake()
    {
        OnStateChange.AddListener((enable) =>
        {
            
            if (enable)
            {
                PlayerInfo.Instance.Inventry.SetVisible(true);
                gameObject.SetActive(true);
             

            }
        });
        close_button.onClick.AddListener(() => { PlayerInfo.Instance.Inventry.SwitchVisible(); });
    }

    private void OnDisable()
    {
        if (!PlayerInfo.InstanceNullable) return;
        var data1 = slotManager.GetSlotItem(0).Value;
        var data2 = slotManager.GetSlotItem(1).Value;
        var data3 = slotManager.GetSlotItem(2).Value;
        var data4 = slotManager.GetSlotItem(3).Value;

        if (PlayerInfo.Instance.Inventry.GetItem(data1.id, data1.amount))
        {
            slotManager.ClearSlot(0);
        }
        if (PlayerInfo.Instance.Inventry.GetItem(data2.id, data2.amount))
        {
            slotManager.ClearSlot(1);
        }
        if (PlayerInfo.Instance.Inventry.GetItem(data3.id, data3.amount))
        {
            slotManager.ClearSlot(2);
        }
        if (PlayerInfo.Instance.Inventry.GetItem(data4.id, data4.amount))
        {
            slotManager.ClearSlot(3);
        }
    }
    protected override void Start()
    {
        
        SetSortOrder(OrderOfUI.NormalPanel);
        for (int i = 0; i < discriptions.Count; i++)
        {
            discriptions[i].text = "";


        }

    }
    protected override void Update()
    {
        base.Update();
        if (PlayerInfo.InstanceNullable == null) return;

        if (gameObject.activeSelf && !PlayerInfo.Instance.Inventry.GetVisibility())
        {
            gameObject.SetActive(false);
        }

        for (int i = 0; i < discriptions.Count; i++)
        {
            var data = craftslots.GetSlotItem(i);
            if (!data.HasValue) return;
            
            if(data.Value.id != Items.Item_ID.EmptyObject)
            {
                discriptions[i].text = SlotManager.GetItemData(data.Value.id).item_name.GetLocalizedString();
            }
            else
            {
                discriptions[i].text = "";
            }

        }
    }
}
