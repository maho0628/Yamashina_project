using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftItemID : MonoBehaviour
{
    //
    //  クラフトの組み合わせを設定するクラスです。
    //

    //  クラフトするのに必要なアイテムID
    [Header("クラフトアイテムID"), SerializeField]
    private int FirstID;
    [SerializeField] int SecondID;

    //  上記のIDでクラフトできるアイテム
    [SerializeField] private GameObject goCraftItem;

    //  IDを外部から取得する関数
    public int GetID(bool isFirstID)
    {
        if (isFirstID)
        {
            return FirstID;
        }
        else
        {
            return SecondID;
        }
    }
    //  クラフトアイテムを取得する関数
    public GameObject GetCraftItem()
    {
        return goCraftItem;
    }
}
