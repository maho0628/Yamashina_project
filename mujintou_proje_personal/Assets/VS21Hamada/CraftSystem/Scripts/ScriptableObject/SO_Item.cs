using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemCategory
{
    Basic,
    Craft,
    Food,
    Special
}
public struct ItemParameter
{
    public int addHP;
    public int addHungry;
    public int addWater;
}
[CreateAssetMenu(menuName = "ScriptableObj_Item")]
public class SO_Item : ScriptableObject
{
    [Header("アイテムのカテゴリー")]
    public ItemCategory enCategory;
    [Header("アイテムID")]
    public int itemID;
    [Header("最大スタック数")]
    public int stackCount;
    [Header("アイテム名")]
    public string itemName;
    [Header("アイテム説明")]
    public string itemInfo;
    [Header("クラフトで作れるアイテムなのか")]
    public bool isCraftingItem;
    [Header("使用できるアイテムなのか")]
    public bool isUseItem;
    [Header("このアイテム生成に必要なアイテムの各ID")]
    public int firstID;
    public int secondID, ThirdID;
    [Header("このアイテム生成に必要な個数")]
    public int fItemCount;
    public int sItemCount, tItemCount;
    [Header("アイテム使用時の回復量"), SerializeField]
    private int addHealHP;
    [SerializeField]
    private int addHealWater, addHealHungry;

    /// <summary>
    /// 一時格納用の変数。この中の情報が取得できます。
    /// </summary>
    private ItemParameter ItemParameter;

    /// <summary>
    ///   このオブジェクトに設定された体力、食料、水分の回復情報を取得できます。
    /// </summary>
    public ItemParameter GetHealValue()
    {
        ItemParameter.addHP = addHealHP;
        ItemParameter.addHungry = addHealHungry;
        ItemParameter.addWater = addHealWater;
        return ItemParameter;
    }
    /// <summary>
    /// アイテムに設定されているIDを取得できます。
    /// </summary>
    public int GetItemID()
    {
        return itemID;
    }
    /// <summary>
    /// アイテムに設定されている最大スタック量を取得できます
    /// </summary>
    public int GetStackCount()
    {
        return stackCount;
    }
    /// <summary>
    /// このオブジェクトのスタック量を設定できます。
    /// </summary>
    public void SetStackCount(int _Value)
    {
        stackCount = _Value;
    }
}
