using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftSystem : MonoBehaviour
{
    //
    //  実際にクラフト関数を呼ぶクラスです
    //  CraftIDクラスを管理しているCraftListも管理しています。
    //

    //  CraftListクラスを取得
    [SerializeField] CraftList cpCraftList;

    //  クラフトテーブル上のアイテムを補完する変数
    public InventorySlotVer cpFirstItemSlot, cpSecondItemSlot, cpThirdItemSlot, cpCraftItemSpawnPos;

    void Update()
    {
        //  以下の方法はテストです。
        //  シーン上で何らかの方法で取得して、cpFirstItemSlotとcpSecondItemSlotに代入してから関数をよびだしてください。
        //  別に関数にしなくてもいいですが、関数にしておくと、後々らくちんですね。

    }
    //public void CraftStart()
    //{
    //    自作のボタンで呼び出すようにしました。Unityを見てみてね

    //  var _item = cpCraftList.TwoItemCraft(cpFirstItemSlot.GetItem(), cpSecondItemSlot.GetItem());
    //    var変数でローカル変数として取得しておくと、以下のようにスポーン場所も指定できるね
    //    if (_item != null)
    //    {
    //        cpCraftItemSpawnPos.SetItem(_item);
    //        Debug.Log("クラフトに成功");
    //    }
    //}
}
