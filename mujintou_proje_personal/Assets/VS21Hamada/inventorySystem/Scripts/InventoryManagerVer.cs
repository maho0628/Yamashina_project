using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InventoryManagerVer : MonoBehaviour
{

    //  テキスト表示用
    public Text txName, txInfo;
    [SerializeField] private Button btnUseButton, btnTrashButton;
    [SerializeField] NewItem dummy;
    private NewItem item;
    private bool isClick, isItemFull;
    [SerializeField, Header("この配列の最後には必ず捨てる画面のスロットを入れてください")] InventorySlotVer[] cpSlots;
    [SerializeField] TrashItemSystem cpTrashSystem;

    private void Start()
    {
        //  Slotの最後にあるスロットを非表示にする。
        cpSlots[cpSlots.Length - 1].DisableSlot();
    }
    private void Update()
    {
        //  最後のスロットにアイテムがあるかどうか
        if (cpSlots[cpSlots.Length - 1].GetItem() != null && !cpTrashSystem.GetTrashAreaisShow())
        {
            isItemFull = true;
            cpTrashSystem.EnableTrashArea();
        }
        if (cpSlots[cpSlots.Length - 1].GetItem() == null)
        {
            isItemFull = false;
        }
    }
    /// <summary>
    /// 新しく手に入れたアイテムをスロットに格納する関数。
    /// あふれる場合はどれかを捨てるウィンドウが出てくる。
    /// </summary>
    /// <param name="_ItemValue"></param>
    public void SetNewGetItem(GameObject _ItemValue)
    {
        for (int i = 0; i < cpSlots.Length; i++)
        {
            if (i == cpSlots.Length - 1)
            {
                if (cpSlots[i].GetItem() != null)
                {
                    Destroy(_ItemValue);
                    return;
                }
                //  あふれる
                Debug.Log("あれた");
                cpSlots[i].EnableSlot();
                cpSlots[i].SetItem(_ItemValue);
                cpTrashSystem.EnableTrashArea();
                return;
            }

            if (cpSlots[i].GetItem() != null)
            {
                continue;
            }
            else
            {
                cpSlots[i].SetItem(_ItemValue);
                return;
            }
        }
    }
    public  bool GetIsItemFull()
    {
        return isItemFull;
    }


    /// <summary>
    ///  引数のアイテムの情報を表示する
    /// </summary>
    public void SetWindowStat(NewItem ItemValue)
    {
        item = ItemValue;
        txName.text = item.ScriptalItem.itemName;
        txInfo.text = item.ScriptalItem.itemInfo;

        if (!isClick)
        {
            btnTrashButton.gameObject.SetActive(true);
            btnTrashButton.onClick.AddListener(delegate { SendTrash(item); });

            if (item.ScriptalItem.isUseItem)
            {
                btnUseButton.gameObject.SetActive(true);
                btnUseButton.onClick.AddListener(delegate { SetCurrentItemUse(item); });
            }
            else
            {
                btnUseButton.gameObject.SetActive(false);
            }
            isClick = true;
        }
    }
    public void SetCurrentItemUse(NewItem _CurrentItem)
    {
        _CurrentItem.UseItem();
    }
    public void SendTrash(NewItem _CurrentItem)
    {
        _CurrentItem.TrashItem();
        btnListReset();    }
    public void btnListReset()
    {
        if (item == null) return;

            isClick = false;
            btnUseButton.onClick.RemoveAllListeners();
            btnTrashButton.onClick.RemoveAllListeners();
            item = null;
        
    }

}