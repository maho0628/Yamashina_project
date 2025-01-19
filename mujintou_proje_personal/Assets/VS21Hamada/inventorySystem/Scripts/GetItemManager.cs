using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetItemManager : MonoBehaviour
{
    [SerializeField,Header("全アイテム格納必須")] GameObject[] goAllItemPrefabs;
    [SerializeField, Header("inventoryマネージャー取得してね")] InventoryManagerVer cpInventoryManager;

    private void Update()
    {
        //  debug用。Update内はコメントアウトしてね
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (!cpInventoryManager.GetIsItemFull())
                GetNewItem(Random.Range(4, 68));
        }
    }
    /// <summary>
    /// 引数にアイテムIDを入れる事で新たにアイテムを出現させることができる。
    /// 生成されたアイテムは自動的に空いているスロットに入れられる。
    /// </summary>
    /// <param name="_ItemID"></param>
    public void GetNewItem(int _ItemID)
    {
         Transform parent = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Transform>();

        var item = Instantiate(goAllItemPrefabs[_ItemID],parent);
        item.transform.localScale *= 20;
        cpInventoryManager.SetNewGetItem(item);
    }
}
