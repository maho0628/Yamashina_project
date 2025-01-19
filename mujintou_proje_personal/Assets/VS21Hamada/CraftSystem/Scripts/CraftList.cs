using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftList : MonoBehaviour
{
    //
    //  関数として、クラフトに必要なアイテムかどうかを判定するクラスです。
    //  同時にCraftItemIDクラスも管理しています。
    //

    [Header("クラフトした結果のアイテム配列"), SerializeField]
    CraftItemID[] cpCraftPrefab;

    public GameObject TwoItemCraft(ItemHamada _fItem, ItemHamada _sItem)
    {
        //  引数のItemクラスから、そのアイテムのIDを取得
        int getFid = _fItem.GetItemID();
        int getSid = _sItem.GetItemID();

        //  for文で取得したIDの組み合わせでクラフトできるものがあるかチェック
        for (int i = 0; i < cpCraftPrefab.Length; i++)
        {
            //  取得したIDとクラフト表のIDを照らし合わせる
            if (getFid == cpCraftPrefab[i].GetID(true) && getSid == cpCraftPrefab[i].GetID(false))
            {
                return Instantiate(cpCraftPrefab[i].GetCraftItem());
            }
            //  反転して入れていても対応する用
            else if (getFid == cpCraftPrefab[i].GetID(false) && getSid == cpCraftPrefab[i].GetID(true))
            {
                return Instantiate(cpCraftPrefab[i].GetCraftItem());
            }
        }
        Debug.Log("ID:" + getFid + "-" + getSid + "のクラフトアイテムは存在しません");
        return null;
    }
}