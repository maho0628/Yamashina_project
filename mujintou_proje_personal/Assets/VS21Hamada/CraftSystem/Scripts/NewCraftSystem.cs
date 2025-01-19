using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewCraftSystem : MonoBehaviour
{
    public NewCraftList cpList;
    public GameObject[] prefabCraftItem;

    public InventorySlotVer cpFirstItemSlot, cpSecondItemSlot, cpThirdItemSlot, cpCraftItemSpawnPos;

    /// <summary>
    /// 自身のリストから、現在アイテムスロットで取得しているアイテムの組み合わせでクラフトできるかを確認します。
    /// リスト内にクラフト可能なものがあればそれに付随するプレハブをスポーンさせます。
    /// </summary>
    public void CraftStart()
    {
        var _ItemNum = cpList.CraftItem(cpFirstItemSlot.GetItem(), cpSecondItemSlot.GetItem(), cpThirdItemSlot.GetItem());
        if (_ItemNum != -1)
        {
            Instantiate(prefabCraftItem[_ItemNum], cpCraftItemSpawnPos.transform.position, cpCraftItemSpawnPos.transform.rotation);
        }
        else
            Debug.LogWarning("クラフトシステムから受け取った値が-1です。");

    }
}
