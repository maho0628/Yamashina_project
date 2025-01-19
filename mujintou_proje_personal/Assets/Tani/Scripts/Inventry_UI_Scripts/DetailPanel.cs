using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;
using UnityEngine.Events;

public class DetailPanel : MonoBehaviour
{
    [SerializeField]
    Text effect_text;
    [SerializeField]
    Text discription_text;
    [SerializeField]
    Text ItemName;
    [SerializeField]
    Image icon_image;
    [SerializeField]
    Button Use_Button;
    [SerializeField]
    List<Items.Item_ID> foodsForActionValueIncrease;

    Items.Item_ID current_id = Items.Item_ID.Item_Max;
    string foodUsageLogForActionValueIncrease_textName = "FoodUsageLog.txt";
    string fullPath;
    Dictionary<Items.Item_ID,bool> foodUsageLog = new Dictionary<Items.Item_ID, bool>();
    public event UnityAction<Items.Item_ID> OnItemUse;
    private void OnEnable()
    {
        SlotManager.selectedItem.slotManager = null;
    }
    private void Awake()
    {
        fullPath = Application.streamingAssetsPath + "/Saves/" + foodUsageLogForActionValueIncrease_textName;
        var loaded = LoadFoodUsageLog();
        foreach (var item in loaded)
        {
            
            foodUsageLog.Add(item.id, item.isUsed);
        }

        Use_Button.onClick.AddListener(() => 
        {
            Items.Item_ID id = SlotManager.selectedItem.slotManager.GetSlotItem(SlotManager.selectedItem.index).Value.id;
            SlotManager affiliation = SlotManager.selectedItem.slotManager;
            int index = SlotManager.selectedItem.index;

            if (!affiliation) return;

            affiliation.UseSlotItem(index);
            OnItemUse?.Invoke(id);

            if (id == Items.Item_ID.item_craft_water || id == Items.Item_ID.item_craft_water2)
            {
                PlayerInfo.Instance.Inventry.GetItem(Items.Item_ID.item_mat_bottle, 1);
            }

            //Keyにアイテムがなかったらスキップ
            if (!foodUsageLog.ContainsKey(id)) {return; }
            if(  foodUsageLog[id] == false)
            {
                PlayerInfo.Instance.MaxActionValue++;
                foodUsageLog[id] = true;
            }

           


        });

        #region
        if (SlotManager.selectedItem.slotManager)
        {
            //指定したスロットにアイテムがなければreturn
            if (!SlotManager.selectedItem.slotManager.GetSlotItem(SlotManager.selectedItem.index).HasValue) return;

            Items.Item_ID id = SlotManager.selectedItem.slotManager.GetSlotItem(SlotManager.selectedItem.index).Value.id;
            //idがemptyならreturn
            if (id == Items.Item_ID.EmptyObject) return;

            var item_data = SlotManager.GetItemData(id);

            //idが同じならreturnl
            if (item_data.item_ID == current_id) return;

            if (item_data.canUse)
            {
                effect_text.text = (item_data.Health_Change > 0 ? $"体力 : +{item_data.Health_Change}" : $"体力 : {item_data.Health_Change}") + " " +
                               (item_data.Hunger_Change > 0 ? $"食料 : +{item_data.Hunger_Change}" : $"食料 : {item_data.Hunger_Change}") + " " +
                               (item_data.Thirst_Chage > 0 ? $"水分 : +{item_data.Thirst_Chage}" : $"水分 : {item_data.Thirst_Chage}") + "\n" +
                                item_data.extra_effect;

            }
            else
            {
                effect_text.text = "使用できない";
            }

            discription_text.text = item_data.Discription.ToString();
            icon_image.sprite = item_data.icon;
            icon_image.color = new Color(1, 1, 1, 1);
            ItemName.text = item_data.item_name.ToString();

            current_id = item_data.item_ID;
            Use_Button.interactable = item_data.canUse;
        }
        else
        {

            effect_text.text = null;
            discription_text.text = null;
            ItemName.text = null;
            icon_image.sprite = null;
            icon_image.color = new Color(1, 1, 1, 0);
            current_id = Items.Item_ID.Item_Max;
            Use_Button.interactable = false;
        }
        #endregion
    }

    private void Update()
    {

        if (SlotManager.selectedItem.slotManager)
        {
            //指定したスロットにアイテムがなければreturn
            if (!SlotManager.selectedItem.slotManager.GetSlotItem(SlotManager.selectedItem.index).HasValue) return;
            
            Items.Item_ID id = SlotManager.selectedItem.slotManager.GetSlotItem(SlotManager.selectedItem.index).Value.id;
            //idがemptyならreturn
            if (id == Items.Item_ID.EmptyObject) return;

            var item_data = SlotManager.GetItemData(id);
            
            //idが同じならreturnl
            if (item_data.item_ID == current_id) return;

            if (item_data.canUse)
            {
                effect_text.text = (item_data.Health_Change > 0 ? $"体力 : +{item_data.Health_Change}" : $"体力 : {item_data.Health_Change}") +" "+
                               (item_data.Hunger_Change > 0 ? $"食料 : +{item_data.Hunger_Change}" : $"食料 : {item_data.Hunger_Change}" ) + " " +
                               (item_data.Thirst_Chage > 0 ? $"水分 : +{item_data.Thirst_Chage}" : $"水分 : {item_data.Thirst_Chage}") + "\n" +
                                item_data.extra_effect;

            }
            else
            {
                effect_text.text = "使用できない";
            }

            discription_text.text = item_data.Discription.GetLocalizedString();
            icon_image.sprite = item_data.icon;
            icon_image.color = new Color(1, 1, 1, 1);
            ItemName.text = item_data.item_name.GetLocalizedString()  ;

            current_id = item_data.item_ID;
            Use_Button.interactable = item_data.canUse ;
        }
        else
        {

            effect_text.text = null;
            discription_text.text = null;
            ItemName.text = null;
            icon_image.sprite = null;
            icon_image.color = new Color(1, 1, 1, 0);
            current_id = Items.Item_ID.Item_Max;
            Use_Button.interactable = false;
        }
    }



    IEnumerable<(Items.Item_ID id, bool isUsed)> LoadFoodUsageLog()
    {
        if (!File.Exists(fullPath)) SaveFoodUsageLog();

        using (StreamReader sr = new StreamReader(fullPath))
        {
            while (!sr.EndOfStream)
            {
                var line = sr.ReadLine().Split(":");
                yield return ((Items.Item_ID)(int.Parse(line[0])), bool.Parse(line[1]));
            }
        }
    }

    public void SaveFoodUsageLog()
    {
          // ファイルが存在しないとき
          if (!File.Exists(fullPath))
          {
             using (var fs = File.Create(fullPath))
             using (StreamWriter sw = new StreamWriter(fs))
             {
                foreach (Items.Item_ID id in foodsForActionValueIncrease)
                {
                    sw.WriteLine(((int)id).ToString() + ":" + "False");
                }
             }
                return;
            }

        using (var sw = new StreamWriter(fullPath))
        {
            foreach (var pair in foodUsageLog)
            {
                sw.WriteLine((int)pair.Key + ":" + pair.Value.ToString());
            }
        }
    }

   
   

    private void OnDestroy()
    {
        SaveFoodUsageLog();
    }
}
