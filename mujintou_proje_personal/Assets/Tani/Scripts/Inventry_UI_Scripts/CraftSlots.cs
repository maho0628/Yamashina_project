using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

[System.Serializable]
class IdAmountPair
{
    public IdAmountPair() { }
    public IdAmountPair(Items.Item_ID id,int amount)
    {
        this.id = id;
        this.amount = amount;
    }
    public  Items.Item_ID id = Items.Item_ID.EmptyObject;
    public  int amount = 1;
    public bool Matching(Items.Item_ID id, int amount)
    {
       // Debug.Log($"this id:{this.id},this amount:{this.amount},id:{id},amout{amount}");
        if (this.id != id) return false;
        return amount >= this.amount;
    }
}

[System.Serializable]
class CraftRecipe
{

    public List<IdAmountPair> input_items;
    public Items.Item_ID crafted_item;
    public int craft_num = 1;
    [HideInInspector]
    public int[] input_relation = null;
}

public class CraftSlots : SlotManager
{
    [SerializeField]
    Slot craft_output_slot;

    
    
    [SerializeField]
    TextAsset recipe_book;
    [SerializeField]
    List<CraftRecipe> recipes;
    [SerializeField]
    string recipe_save_fileName;
    [SerializeField]
    bool use_action_value = true;

    public bool UseActionValue => use_action_value;

    int input_slot_num = 0;

    CraftRecipe cureentCraftableRecipe = null;
    string recipe_data_save_folder = Application.dataPath + "/Tani/Saves/";
    string default_save_name = "default_recipe_save";
    protected override void Awake()
    {


        data.index_igonored.Clear();
        var n = data.extra_slots[0];
        data.extra_slots.Clear();
        data.extra_slots.Add(n);

        
        base.Awake();
        

        
    }
    protected override void Start()
    {
        //SetItemToSlot(Items.Item_ID.Plank, 1, 3);

        Save();

        
    }

    public override void SlotReconstruct()
    {
        base.SlotReconstruct();
        input_slot_num = _Slots.Length - 1;

    }

    public override bool SetItemToSlot(Items.Item_ID item_ID, int num, int slot_index)
    {
        if (!_Slots[slot_index]) return false;
        item_list[slot_index] = (item_ID, num);
        var n = Resources.Load($"{item_ID}") as Items;
        if (n == null)
        {
            Debug.LogError($"Couldn't find Item Data : {item_ID} in Resources");
            return false;
        }
        _Slots[slot_index].SetIcon(n.icon);
        _Slots[slot_index].SetAmoutText(num);

        if(slot_index >= 0 && slot_index < input_slot_num)
        {
            OnInputItemChanged();
        }
        else if(slot_index == craft_output_slot.Slot_index)
        {
            OnOutputItemChanged();
        }

        return true;
    }

    public override void ClearSlot(int slot_index)
    {
        if (!_Slots[slot_index]) return;
        item_list[slot_index] = (Items.Item_ID.EmptyObject, 0);
        _Slots[slot_index].SetIcon(null);
        _Slots[slot_index].SetAmoutText(null);
        if (slot_index >= 0 && slot_index < input_slot_num)
        {
            OnInputItemChanged();
        }
        else if (slot_index == craft_output_slot.Slot_index)
        {
            OnOutputItemChanged();
        }
    }
    public void DoCraft()
    {
        int craftable = CheckCraftable();
        if (craftable == 0 || cureentCraftableRecipe == null) return;

        
        if(craftable == 1)
        {
            print("craft new");
            var temp_current_recipe = cureentCraftableRecipe;//currentRecipeが更新でnullになってしまうため
            for (int i = 0; i < temp_current_recipe.input_relation.Length; i++)
            {
                ChangeSlotItemAmount(item_list[temp_current_recipe.input_relation[i]].amount
                    - temp_current_recipe.input_items[i].amount, temp_current_recipe.input_relation[i]);
            }
            SetItemToSlot(temp_current_recipe.crafted_item, temp_current_recipe.craft_num, craft_output_slot.Slot_index);
            if (use_action_value)
            {
                PlayerInfo.Instance.ActionValue--;

            }
            //柴田追加コード//ここから//
            switch (temp_current_recipe.crafted_item)
            {
                case Items.Item_ID.item_craft_boiledEgg:
                    PlayerInfo.Instance.Inventry.GetItem(Items.Item_ID.item_mat_bottle, 1);
                    break;
                case Items.Item_ID.item_craft_seaSoup:
                    PlayerInfo.Instance.Inventry.GetItem(Items.Item_ID.item_mat_bottle,1);
                    break;
            }
            //ここまで//
            return ;
        }
        else if(craftable == 2)
        {
            print("craft add");
            var temp_current_recipe = cureentCraftableRecipe;//currentRecipeが更新でnullになってしまうため
            for (int i = 0; i < temp_current_recipe.input_relation.Length; i++)
            {
                ChangeSlotItemAmount(item_list[temp_current_recipe.input_relation[i]].amount
                    - temp_current_recipe.input_items[i].amount, temp_current_recipe.input_relation[i]);
            }

            ChangeSlotItemAmount(GetSlotItem(craft_output_slot.Slot_index).Value.amount + temp_current_recipe.craft_num, craft_output_slot.Slot_index);
            if (use_action_value)
            {
                PlayerInfo.Instance.ActionValue--;
            }
            //柴田追加コード//ここから//
            switch (temp_current_recipe.crafted_item)
            {
                case Items.Item_ID.item_craft_boiledEgg:
                    PlayerInfo.Instance.Inventry.GetItem(Items.Item_ID.item_mat_bottle, 1);
                    break;
                case Items.Item_ID.item_craft_seaSoup:
                    PlayerInfo.Instance.Inventry.GetItem(Items.Item_ID.item_mat_bottle, 1);
                    break;
            }
            //ここまで//

            return;
        }
        else
        {
            return ;
        }

    }
    /// <summary>
    /// Return 0 if cant craft,1 if can make new,2 if can make add
    /// </summary>
    /// <returns></returns>
    public int CheckCraftable()
    {
        if (cureentCraftableRecipe == null || cureentCraftableRecipe.input_relation == null) return 0;

        if (GetSlotItem(craft_output_slot.Slot_index).Value.id == Items.Item_ID.EmptyObject)
        {
            return 1;
        }
        else if (GetSlotItem(craft_output_slot.Slot_index).Value.id == cureentCraftableRecipe.crafted_item)
        {
            return 2;
        }
        else
        {
            return 0;
        }
    }

    private void OnInputItemChanged()
    {
        cureentCraftableRecipe = ExistCraftRecipe();
       // print("recipe : "+ recipe_index);
        if (CheckCraftable() == 1)
        {
            var item_data = SlotManager.GetItemData(cureentCraftableRecipe.crafted_item);
            craft_output_slot.SetIcon(item_data.icon, 200);
        
        }
        else if(CheckCraftable() == 0 && GetSlotItem(craft_output_slot.Slot_index).Value.id == Items.Item_ID.EmptyObject)
        {
            craft_output_slot.SetIcon(null);
       
        }
        
    }

    private void OnOutputItemChanged()
    {
        bool is_output_slot_empty = craft_output_slot.Affiliation
            .GetSlotItem(craft_output_slot.Slot_index).Value.id == Items.Item_ID.EmptyObject;

        for (int i = 0; i < input_slot_num; i++)
        {
            _Slots[i].can_place_item = is_output_slot_empty;
        }
        if (CheckCraftable() == 1)
        {
            var item_data = SlotManager.GetItemData(cureentCraftableRecipe.crafted_item);
            craft_output_slot.SetIcon(item_data.icon, 200);

        }
    }

    private CraftRecipe ExistCraftRecipe()
    {
        bool input_slot_has_item = false;
        for (int i = 0; i < input_slot_num; i++)
        {
            if (item_list[i].id != Items.Item_ID.EmptyObject)
            {
                input_slot_has_item = true;
                break;
            }
        }
        if (!input_slot_has_item) return null;

        //MatchRecipeの絞り込み
        List<CraftRecipe> match_recipe = recipes.FindAll(n => n.input_items.Count <= input_slot_num);

        //必要アイテム個数がスロットの数より小さいときemptyを足しておく
        foreach(var n in match_recipe)
        {
            if (n.input_items.Count < input_slot_num)
            {
                while (n.input_items.Count < input_slot_num)
                {
                    n.input_items.Add(new IdAmountPair(Items.Item_ID.EmptyObject,0));
                }
            }
        }

 
        var input_item_list = item_list.ToList();
        input_item_list.RemoveAt(input_item_list.Count - 1);

        //レシピデータの素材に対応するインプットスロットのインデックス
        int[] relation = new int[input_slot_num];
        
        //レシピリストからレシピデータを取り出す
        foreach (var recipe_data in match_recipe)
        {
            List<int> used_numbers = new List<int>();
            ///レシピデータの素材をひとつづつ取り出す
            for (int n = 0; n < recipe_data.input_items.Count; n++)
            {
                //レシピデータの素材に対応するインプットスロットのアイテムを探す
                for (int i = 0; i < input_item_list.Count; i++)
                {
                    if (recipe_data.input_items[n].Matching(input_item_list[i].id, input_item_list[i].amount))
                    {
                        print($"match {i}");
                        if (used_numbers.Contains(i)) continue ;
                        relation[n] = i;
                        used_numbers.Add(i);
                        break;
                    }
                    else
                    {
                        if (i >= input_item_list.Count - 1)
                        {
                            goto There;
                        }
                    }
                }

                if (used_numbers.Count == input_slot_num)
                {
                    recipe_data.input_relation = relation;
                    return recipe_data;
                }     
            }
        There:;


        }
    
        return null;
    }

    public  void LoadRecipeData()
    {
        #region error回避
        if (!recipe_book)
        {
            Debug.LogError("参照先レシピが登録されていません");
            return;
        }
        #endregion
        recipes.Clear();
        string raw_text = recipe_book.text;
        var str_per_line = raw_text.Split(char.Parse("\n"));
        foreach(var line in str_per_line)
        {
            if (line.Contains("IGNORE")) continue;
            var data_per_slot = line.Split(",");
            CraftRecipe recipe = new CraftRecipe();
            recipe.input_items = new List<IdAmountPair>();
            for (int i = 0;i < data_per_slot.Length - 2; i++)
            {
                int id_data = 0;
                int amount_data = 0;
                #region TryParseのエラー回避
                if (!(int.TryParse(data_per_slot[i].Split(":")[0],out id_data) 
                    && int.TryParse(data_per_slot[i].Split(":")[1], out amount_data)))
                {
                    Debug.LogError("参照先レシピの読み込みに失敗\n形式が正しいか確認してください");
                    return;
                }
                #endregion
                recipe.input_items.Add(new IdAmountPair((Items.Item_ID)id_data,amount_data));
            }
            recipe.crafted_item = (Items.Item_ID)int.Parse(data_per_slot[data_per_slot.Length - 2]);
            recipe.craft_num = int.Parse(data_per_slot[data_per_slot.Length - 1]);
            recipes.Add(recipe);

 
        }
    }

    private void SaveRecipeData(string fileName)
    {
        var path = recipe_data_save_folder + fileName + ".txt";
        
        LinkedList<string> strs = new LinkedList<string>();
        foreach(var n in recipes)
        {
            string line = "";
            foreach(var item in n.input_items)
            {
                line += ((int)item.id).ToString() + ":"+ item.amount.ToString() + ",";
               
            }
            line += ((int)n.crafted_item).ToString() + ",";
            line += n.craft_num.ToString();
            strs.AddLast(line);
        }
        File.WriteAllLines(path, strs.ToArray());


    }

    public void Save()
    {
        
        if (recipe_save_fileName.Trim().Length == 0)
        {
           
            SaveRecipeData(default_save_name);
        }
        else
        {
            SaveRecipeData(recipe_save_fileName);
        }
    }

}


#if UNITY_EDITOR
[CustomEditor(typeof(CraftSlots))]
class CraftSlotsInspector : Editor
{
    SerializedProperty property_slot_editable;
    CraftSlots manager;

    bool bShowLoadAction = false;
    bool bShowSaveAction = false;
    private void OnEnable()
    {
        manager = target as CraftSlots;
        property_slot_editable = serializedObject.FindProperty("slot_data_editable");

    }

    
    public override void OnInspectorGUI()
    {
        //  base.OnInspectorGUI();
        serializedObject.Update();
      

        EditorGUILayout.PropertyField(serializedObject.FindProperty("Slots_Main"), new GUIContent("表示の切り替え元"));
        bool use_slotdata_maintaining = true;

        using (var check = new EditorGUI.ChangeCheckScope())
        {
            EditorGUILayout.PropertyField(property_slot_editable, new GUIContent("データを編集"));
            if (check.changed)
            {
                serializedObject.ApplyModifiedProperties();
                if (!property_slot_editable.boolValue)
                {
                    manager.SlotReconstruct();
                }
                serializedObject.Update();

            }

        }



        EditorGUI.BeginDisabledGroup(!property_slot_editable.boolValue);

        EditorGUILayout.PropertyField(serializedObject.FindProperty("data"), new GUIContent("スロット群のデータ"));

        EditorGUI.EndDisabledGroup();





        EditorGUILayout.PropertyField(serializedObject.FindProperty("craft_output_slot"), new GUIContent("出力先スロット","extra_slotsに指定したものと同じものを入れてください"));

        EditorGUILayout.PropertyField(serializedObject.FindProperty("recipes"), new GUIContent("クラフトレシピ"));

        bShowLoadAction = EditorGUILayout.BeginFoldoutHeaderGroup(bShowLoadAction, "レシピの読み込み");
        if (bShowLoadAction)
        {

            EditorGUILayout.PropertyField(serializedObject.FindProperty("recipe_book"), new GUIContent("参照先レシピ"));
            EditorGUILayout.LabelField("現在のクラフトレシピは上書きされます", EditorStyles.boldLabel);
            GUILayout.BeginHorizontal();
         
            var loadButtonDown =  GUILayout.Button("読み込み", GUILayout.ExpandWidth(true), GUILayout.Height(30));
            GUILayout.EndHorizontal();
            if (loadButtonDown)
            {
                Debug.Log("レシピを読み込み");
                serializedObject.ApplyModifiedProperties();
                manager.LoadRecipeData();
                serializedObject.Update();

            }
         
        }
        EditorGUILayout.EndFoldoutHeaderGroup();

        bShowSaveAction = EditorGUILayout.BeginFoldoutHeaderGroup(bShowSaveAction, "レシピの書き出し");
        if (bShowSaveAction)
        {

            EditorGUILayout.PropertyField(serializedObject.FindProperty("recipe_save_fileName"), new GUIContent("レシピ保存先の名前"));
            EditorGUILayout.LabelField(new GUIContent("Asset/Tani/Saves/レシピ保存先の名前.txtに保存されます"
                ,"名前がないときはDefault_recipe_saveとして保存されます"), EditorStyles.boldLabel);
            EditorGUILayout.LabelField("同名が存在するとき上書きされます", EditorStyles.boldLabel);

            var saveButtonDown = GUILayout.Button("書き出し", GUILayout.ExpandWidth(true), GUILayout.Height(30));
            if (saveButtonDown)
            {
                Debug.Log("レシピを書き出し");
                serializedObject.ApplyModifiedProperties();
                manager.Save();
                serializedObject.Update();

            }

        }
        EditorGUILayout.EndFoldoutHeaderGroup();


        use_slotdata_maintaining = EditorGUILayout.Toggle("アイテムデータを保持する", use_slotdata_maintaining);
        if (use_slotdata_maintaining)
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("fileName"), new GUIContent("データ保存先"));

        }

        EditorGUILayout.PropertyField(serializedObject.FindProperty("use_action_value"), new GUIContent("行動値を消費する"));
       



        serializedObject.ApplyModifiedProperties();

    }
}



#endif