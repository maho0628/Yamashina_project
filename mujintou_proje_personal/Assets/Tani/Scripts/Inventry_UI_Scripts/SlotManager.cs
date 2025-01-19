using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.IO;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Localization.Settings;
#if UNITY_EDITOR
using UnityEditor;
#endif

[System.Serializable]
public class SlotsInfo
{
    public int slot_horizontal_num = 0;
    public int slot_vertical_num = 0;
    public int margin_horizontal = 20;
    public int margin_vertical = 20;
    public Slot slot_prefab = null;
    public GameObject slot_parent = null;
    public List<int> index_igonored;
    public List<Slot> extra_slots;
}

public class SlotManager : MonoBehaviour
{
    public static (SlotManager slotManager, int index) selectedItem = (null, 0);


    [SerializeField]
    protected SlotsInfo data;
    [SerializeField]
    GameObject Slots_Main;
    [SerializeField]
    string fileName = "";


    public UnityAction OnSlotVisibilityChanged { get; set; } = null;


    private Vector2Int slot_rect = new Vector2Int(0, 0);
    protected Slot[] _Slots = null;
    protected (Items.Item_ID id, int amount)[] item_list = null;
    private float active_range = 0;
    private ItemPickupVisualizer visualizer;


    virtual protected void Awake()
    {
        SlotReconstruct();
        active_range = data.slot_prefab.GetComponent<RectTransform>().rect.width * 2;


        LoadSlotDatas(fileName);
        visualizer = GetComponentInParent<ItemPickupVisualizer>();

    }

    virtual protected void Start()
    {
       
    }

    virtual protected void Update()
    {

    }
    public virtual void SlotReconstruct()
    {
        #region エラー処理
        if (!data.slot_prefab)
        {
            Debug.LogError("SlotPrefab is not set");
            return;
        }
        #endregion





        var rectTransform = data.slot_prefab.gameObject.GetComponent<RectTransform>();
        slot_rect = new Vector2Int((int)rectTransform.rect.width, (int)rectTransform.rect.height);

        if (_Slots != null && _Slots.Length != 0)
        {
            for (int i = 0; i < _Slots.Length - data.extra_slots.Count; i++)
            {
                if (_Slots[i])
                {
                    DestroyImmediate(_Slots[i].gameObject);
                }
                _Slots[i] = null;
            }
        }

        if (data.slot_parent)
        {
            while (data.slot_parent.transform.childCount != 0)
            {
                DestroyImmediate(data.slot_parent.transform.GetChild(0).gameObject);
            }
        }
        else
        {
            while (gameObject.transform.childCount != 0)
            {
                DestroyImmediate(gameObject.transform.GetChild(0).gameObject);
            }
        }



        _Slots = new Slot[data.slot_horizontal_num * data.slot_vertical_num + data.extra_slots.Count];
        item_list = new (Items.Item_ID id, int amount)[data.slot_horizontal_num * data.slot_vertical_num + data.extra_slots.Count];
        for (int i = 0; i < item_list.Length; i++) item_list[i] = (Items.Item_ID.EmptyObject, 0);

        for (int i = 0; i < _Slots.Length - data.extra_slots.Count; i++)
        {
            if (!data.index_igonored.Contains(i))
            {
                _Slots[i] = Instantiate(data.slot_prefab.gameObject).GetComponent<Slot>();
                _Slots[i].transform.SetParent(
                    data.slot_parent ? data.slot_parent.transform : gameObject.transform);
                _Slots[i].Slot_index = i;
                _Slots[i].Affiliation = this;
                _Slots[i].SetIcon(null);
                _Slots[i].SetAmoutText(null);
            }


            int h_index = i % data.slot_horizontal_num;
            int v_index = i / data.slot_horizontal_num;

            //0 , 1 , 2 , 3
            //4 , 5 , 6 , 7
            //8 , 9 , 10 , 11
            //12 , 13 , 14 , 15

            //↓↓↓↓↓↓↓↓
            float temp_x = h_index - ((data.slot_horizontal_num - 1) / 2.0f);
            float temp_y = (data.slot_vertical_num - 1 - v_index) - ((data.slot_vertical_num - 1) / 2.0f);
            Vector2Int local_index = new Vector2Int(
                  temp_x >= 0 ? Mathf.CeilToInt(temp_x) : Mathf.FloorToInt(temp_x),
                  temp_y >= 0 ? Mathf.CeilToInt(temp_y) : Mathf.FloorToInt(temp_y)
                );

            //(-2,2),(-1,2) , (1,2) , (2,2)
            //(-2,1),(-1,1) , (1,1) , (2,1)
            //(-2,-1),(-1,-1) , (1,-1) , (2,-1)
            //(-2,-2),(-1,-2) , (1,-2) , (2,-2)


            //define horizontal position
            int local_x = 0;
            if (data.slot_horizontal_num % 2 == 0)
            {
                //横の数が偶数の時
                if (local_index.x > 0)
                    local_x = (int)(((2 * local_index.x - 1) / 2.0f) * (data.margin_horizontal + slot_rect.x));
                if (local_index.x < 0)
                    local_x = (int)(((2 * local_index.x + 1) / 2.0f) * (data.margin_horizontal + slot_rect.x));
            }
            else
            {
                //横の数が奇数の時
                local_x = local_index.x * (data.margin_horizontal + slot_rect.x);

            }

            //define vertical position
            int local_y = 0;
            if (data.slot_vertical_num % 2 == 0)
            {
                //縦の数が偶数の時
                if (local_index.y > 0)
                    local_y = (int)(((2 * local_index.y - 1) / 2.0f) * (data.margin_vertical + slot_rect.y));
                if (local_index.y < 0)
                    local_y = (int)(((2 * local_index.y + 1) / 2.0f) * (data.margin_vertical + slot_rect.y));
            }
            else
            {
                //縦の数が奇数の時
                local_y = local_index.y * (data.margin_vertical + slot_rect.y);

            }

            if (!data.index_igonored.Contains(i))
            {
                _Slots[i].gameObject.transform.localPosition = new Vector3(local_x, local_y, 0);
            }

        }
        if (data.extra_slots.Count != 0)
        {
            for (int i = data.slot_horizontal_num * data.slot_vertical_num; i < _Slots.Length; i++)
            {
                _Slots[i] = data.extra_slots[i - data.slot_horizontal_num * data.slot_vertical_num];
                _Slots[i].Slot_index = i;
                _Slots[i].Affiliation = this;
                _Slots[i].SetIcon(null);
                _Slots[i].SetAmoutText(null);
            }
        }

    }

    /// <summary>
    /// Return false if slot_index is included in index_ignored 
    /// </summary>
    public virtual bool SetItemToSlot(Items.Item_ID item_ID, int num, int slot_index)
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
        return true;
    }

    static public Items GetItemData(Items.Item_ID id)
    {
        var n = Resources.Load($"{id}") as Items;
        if (n == null)
        {
            Debug.LogError($"Couldn't find Item Data : {id} in Resources");
            return null;
        }
        return n;
    }

    public string GetItemName(Items.Item_ID item_ID)
    {
        var n = Resources.Load($"{item_ID}") as Items;
        if (n == null)
        {
            Debug.LogError($"Couldn't find Item Data : {item_ID} in Resources");
            return string.Empty;
        }
        return n.item_name.GetLocalizedString();
      
    }

    public void ChangeSlotItemAmount(int new_Amount, int slot_index)
    {
        if (!_Slots[slot_index]) return;
        if (new_Amount < 1)
        {
            ClearSlot(slot_index);
            return;
        }
        item_list[slot_index] = (item_list[slot_index].id, new_Amount);
        _Slots[slot_index].SetAmoutText(new_Amount);
    }

    public virtual void ClearSlot(int slot_index)
    {
        if (!_Slots[slot_index]) return;
        item_list[slot_index] = (Items.Item_ID.EmptyObject, 0);
        _Slots[slot_index].SetIcon(null);
        _Slots[slot_index].SetAmoutText(null);
    }

    /// <summary>
    /// Return null if slot_index is included in index_ignored 
    /// </summary>
    public (Items.Item_ID id, int amount)? GetSlotItem(int index)
    {
        if (!_Slots[index]) return null;
        return item_list[index];
    }

    static public GameObject[] SearchActiveSlotsInScene()
    {
        return GameObject.FindGameObjectsWithTag(Slot.slot_tag_name);
    }

    /// <summary>
    /// Return null if when no active slot or active slot is too far
    /// </summary>
    /// <param name="pos"></param>
    /// <returns></returns>
    public Slot GetNearestSlot(Vector3 pos)
    {
        float nearest_dis = float.MaxValue;
        GameObject nearest_slot = null;
        foreach (var n in SearchActiveSlotsInScene())
        {
            float distant = (n.transform.position - pos).magnitude;
            if (distant < nearest_dis && n.GetComponent<Slot>().can_place_item)
            {
                nearest_dis = distant;
                nearest_slot = n;
            }
        }

        if (nearest_dis <= active_range)
        {
            return nearest_slot.GetComponent<Slot>();
        }
        else
        {
            return null;
        }

    }


    public Slot GetNullSlot()
    {
        for (int i = 0; i < _Slots.Length; i++)
        {
            if (data.index_igonored.Contains(i)) continue;
            if (item_list[i].id == Items.Item_ID.EmptyObject && _Slots[i].can_place_item)
            {
                return _Slots[i];
            }
        }
        return null;
    }

    public bool CanSlotOverlapItem(int slot_index, Items.Item_ID id, int num)
    {
        if (id == Items.Item_ID.EmptyObject) return false;
        if (item_list[slot_index].id != id)
        {
            return false;
        }
        if (num + GetSlotItem(slot_index).Value.amount > GetItemData(id).overlap_num)
        {
            return false;
        }
        return true;
    }

    /// <summary>
    /// Return move success or not
    /// </summary>
    /// <param name="src"></param>
    /// <param name="dis"></param>
    /// <returns></returns>
    static public bool MoveItem(Slot src, Slot dis)
    {
        #region 例外処理
        if (src == dis)
        {
            return false;
        }

        if (src == null || dis == null)
        {
            return false;
        }

        if (!src.can_place_item && dis.Affiliation.GetSlotItem(dis.Slot_index).Value.id != Items.Item_ID.EmptyObject)
        {
            //移動先のスロットにアイテムをスタックできるか
            if (dis.Affiliation.CanSlotOverlapItem(dis.Slot_index, src.Affiliation.item_list[src.Slot_index].id, src.Affiliation.item_list[src.Slot_index].amount))
            {
                dis.Affiliation.ChangeSlotItemAmount(
                    dis.Affiliation.item_list[dis.Slot_index].amount + src.Affiliation.item_list[src.Slot_index].amount
                    , dis.Slot_index);
                src.Affiliation.ClearSlot(src.Slot_index);
                dis.gameObject.GetComponent<Selectable>().Select();
                return true;
            }


            return false;
        }
        #endregion


        //移動先にアイテムが存在するか否か
        if (dis.Affiliation.item_list[dis.Slot_index].id != Items.Item_ID.EmptyObject)
        {
            //移動先のスロットにアイテムをスタックできるか
            if (dis.Affiliation.CanSlotOverlapItem(dis.Slot_index, src.Affiliation.item_list[src.Slot_index].id, src.Affiliation.item_list[src.Slot_index].amount))
            {
                dis.Affiliation.ChangeSlotItemAmount(
                    dis.Affiliation.item_list[dis.Slot_index].amount + src.Affiliation.item_list[src.Slot_index].amount
                    , dis.Slot_index);
                src.Affiliation.ClearSlot(src.Slot_index);
                dis.gameObject.GetComponent<Selectable>().Select();
                return true;
            }


            var temp_src = src.Affiliation.item_list[src.Slot_index];
            var temp_dis = dis.Affiliation.item_list[dis.Slot_index];

            dis.Affiliation.SetItemToSlot(temp_src.id, temp_src.amount, dis.Slot_index);
            src.Affiliation.SetItemToSlot(temp_dis.id, temp_dis.amount, src.Slot_index);
            dis.gameObject.GetComponent<Selectable>().Select();
            return true;

        }
        else
        {
            var temp_src = src.Affiliation.item_list[src.Slot_index];
            dis.Affiliation.SetItemToSlot(temp_src.id, temp_src.amount, dis.Slot_index);
            src.Affiliation.ClearSlot(src.Slot_index);
            dis.gameObject.GetComponent<Selectable>().Select();
            return true;
        }
    }

    public bool SlotPartition(int slot_index)
    {
        var target_slot = GetNullSlot();
        if (target_slot)
        {
            if (item_list[slot_index].amount <= 1) return false;
            int move_amount = item_list[slot_index].amount / 2;
            SetItemToSlot(item_list[slot_index].id, move_amount, target_slot.Slot_index);
            ChangeSlotItemAmount(item_list[slot_index].amount - move_amount, slot_index);
            return true;
        }
        return false;
    }

    /// <summary>
    /// Add item to slot if there is empty slot
    /// </summary>
    public bool GetItem(Items.Item_ID id, int num, bool visualize = false)
    {
        if (num == 0) return true;

        for (int i = 0; i < item_list.Length; i++)
        {
            if (item_list[i].id == id)
            {
                if (CanSlotOverlapItem(i, id, num))
                {
                    ChangeSlotItemAmount(item_list[i].amount + num, i);
                    if (visualize)
                    {
                        visualizer.ItemViewVisualize(id, num);
                    }
                    return true;
                }
            }
        }

        var slot = GetNullSlot();
        if (slot)
        {
            SetItemToSlot(id, num, slot.Slot_index);
            if (visualize)
            {
                visualizer.ItemViewVisualize(id, num);
            }
            return true;
        }
        else
        {
            return false;
        }
    }



    /// <summary>
    /// return item amount in slots
    /// </summary>
    public int GetItemAmount(Items.Item_ID id)
    {
        int amount = 0;
        for (int i = 0; i < item_list.Length; i++)
        {
            if (data.index_igonored.Contains(i)) continue;
            if (!_Slots[i]) continue;
            if (item_list[i].id == id)
            {
                amount += item_list[i].amount;
            }
        }
        return amount;
    }
    /// <summary>
    /// アイテムが使用できない場合そのアイテムを一つ減らす
    /// </summary>
    public bool UseItem(Items.Item_ID id)
    {
        if (id == Items.Item_ID.EmptyObject) return false;
        if (GetItemAmount(id) < 1) return false;

        for (int i = 0; i < item_list.Length; i++)
        {
            if (item_list[i].id == id)
            {
                if (data.index_igonored.Contains(i)) continue;
                if (item_list[i].amount < 1) continue;
                UseSlotItem(i);
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// 指定したslotにあるアイテムを一つ消費
    /// </summary>
    public void UseSlotItem(int index)
    {
        if (item_list[index].id == Items.Item_ID.EmptyObject) return;
        if (item_list[index].amount < 1) return;


        Items item_data = SlotManager.GetItemData(item_list[index].id);
        if (item_data.canUse)
        {
            PlayerInfo info = PlayerInfo.Instance;
            info.Health += item_data.Health_Change;
            info.Hunger += item_data.Hunger_Change;
            info.Thirst += item_data.Thirst_Chage;
            switch (item_data.item_ID)
            {
                case Items.Item_ID.item_mat_shell:
                    {
                        float probability = 0.10f;
                        if (Random.value < probability)
                        {
                            PlayerInfo.Instance.AddPlayerCondition(PlayerInfo.Condition.Poisoned);
                        }
                        break;
                    }
                case Items.Item_ID.item_mat_snake:
                    {
                        float probability = 0.10f;
                        if (Random.value < probability)
                        {
                            PlayerInfo.Instance.AddPlayerCondition(PlayerInfo.Condition.Poisoned);
                        }
                        break;
                    }
                case Items.Item_ID.item_mat_stMash:
                    {
                        float probability = 0.9f;
                        if (Random.value < probability)
                        {
                            PlayerInfo.Instance.AddPlayerCondition(PlayerInfo.Condition.Poisoned);
                        }
                        break;
                    }
                case Items.Item_ID.item_mat_herb:
                    {
                        float probability = 0.65f;
                        if (Random.value < probability)
                        {
                            PlayerInfo.Instance.EraseCondition(PlayerInfo.Condition.Poisoned);
                        }
                        break;
                    }
                case Items.Item_ID.item_craft_water:
                    {
                        float probability = 0.05f;
                        if (Random.value < probability)
                        {
                            PlayerInfo.Instance.AddPlayerCondition(PlayerInfo.Condition.Poisoned);
                        }
                        break;
                    }
                case Items.Item_ID.item_craft_bakedSnake:
                    {
                        float probability = 0.5f;
                        if (Random.value < probability)
                        {
                            PlayerInfo.Instance.EraseCondition(PlayerInfo.Condition.Poisoned);
                        }
                        break;
                    }
                case Items.Item_ID.item_craft_DIYmedicine:
                    {
                        float probability = 1f;
                        if (Random.value < probability)
                        {
                            PlayerInfo.Instance.EraseCondition(PlayerInfo.Condition.Poisoned);
                        }
                        break;
                    }
                case Items.Item_ID.item_special_medicine:
                    {
                        float probability = 1f;
                        if (Random.value < probability)
                        {
                            PlayerInfo.Instance.EraseCondition(PlayerInfo.Condition.Poisoned);
                        }
                        break;
                    }
                case Items.Item_ID.item_craft_raft:
                    {
                        SceneManager.LoadScene("TrueEndingScene_CaseOfRaft");
                        break;
                    }
                case Items.Item_ID.item_special_book:
                    {
                        PlayerInfo.Instance.MaxActionValue += 2;
                        break;
                    }
                default:
                    break;
            }
        }
        ChangeSlotItemAmount(item_list[index].amount - 1, index);
        _Slots[index].gameObject.GetComponent<Selectable>().Select();
    }

    public void SetVisible(bool visible)
    {
        if (Slots_Main)
        {
            Slots_Main.SetActive(visible);
            OnSlotVisibilityChanged?.Invoke();
        }
        else
        {
            gameObject.SetActive(visible);
            OnSlotVisibilityChanged?.Invoke();

        }
    }
    public void SwitchVisible()
    {
        if (Slots_Main)
        {
            Slots_Main.SetActive(!Slots_Main.activeSelf);

            OnSlotVisibilityChanged?.Invoke();

        }
        else
        {
            gameObject.SetActive(!gameObject.activeSelf);
            OnSlotVisibilityChanged?.Invoke();

        }


    }

    public bool GetVisibility()
    {
        if (Slots_Main)
        {
            return Slots_Main.activeSelf;
        }
        else
        {
            return gameObject.activeSelf;
        }
    }
    protected void SaveSlotDatas(string fileName)
    {
        if (fileName.Trim().Length == 0)
        {
            Debug.LogWarning($"File name not assigned at {gameObject.name}");
            return;
        }
        string path = Application.streamingAssetsPath + "/Saves/" + fileName + ".txt";
        if (!File.Exists(path))
        {
            using (File.Create(path)) { }
            print($"make new slots data file of {gameObject.name}");

        }


        string[] texts = new string[item_list.Length];

        for (int i = 0; i < texts.Length; i++)
        {
            texts[i] = ((int)item_list[i].id).ToString() + "," + item_list[i].amount.ToString();
        }
        File.WriteAllLines(path, texts);

    }

    protected void LoadSlotDatas(string fileName)
    {
        if (fileName.Trim().Length == 0)
        {
            Debug.LogWarning($"File name not assigned at {gameObject.name}");
            return;
        }
        string path = Application.streamingAssetsPath + "/Saves/" + fileName + ".txt";
        if (!File.Exists(path))
        {

            SaveSlotDatas(fileName);
            return;
        }
        var datas = File.ReadAllLines(path);
        for (int i = 0; i < datas.Length; i++)
        {
            if (i >= item_list.Length) break;
            var line = datas[i].Split(",");
            if (int.Parse(line[0]) == (int)Items.Item_ID.EmptyObject) continue;
            SetItemToSlot((Items.Item_ID)int.Parse(line[0]), int.Parse(line[1]), i);
        }
    }



    protected void OnDestroy()
    {
        SaveSlotDatas(fileName);
    }





#if UNITY_EDITOR
    [SerializeField]
    bool slot_data_editable = false;


#endif
}

#if UNITY_EDITOR
[CustomEditor(typeof(SlotManager))]
class SlotManagerInspector : Editor
{
    SerializedProperty property_slot_editable;

    bool use_slotdata_maintaining = true;
    private void OnEnable()
    {
        var manager = target as SlotManager;
        property_slot_editable = serializedObject.FindProperty("slot_data_editable");


    }

    public override void OnInspectorGUI()
    {
        // base.OnInspectorGUI();
        serializedObject.Update();



        var manager = target as SlotManager;
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

        EditorGUILayout.PropertyField(serializedObject.FindProperty("Slots_Main"), new GUIContent("表示の切り替え元"));

        use_slotdata_maintaining = EditorGUILayout.Toggle("アイテムデータを保持する", use_slotdata_maintaining);
        if (use_slotdata_maintaining)
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("fileName"), new GUIContent("データ保存先"));

        }




        serializedObject.ApplyModifiedProperties();

    }
}


#endif
