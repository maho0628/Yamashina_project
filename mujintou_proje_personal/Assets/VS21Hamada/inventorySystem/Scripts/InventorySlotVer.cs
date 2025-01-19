using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlotVer : MonoBehaviour
{
    public enum SlotMode
    {
        Storage,
        Craft
    }

    //  クラフトシステムに流用するよう(現在は使っていません)
    public SlotMode enMode;

    //  マネージャー取得
    public InventoryManagerVer cpInventoryManager;

    //  自身の中に動的に確保するアイテム変数
    public NewItem cpInSlotItem;

    //  一時的に確保する変数
    private NewItem cpOverlapItem;

    //  自身のスロットのID(現在は使っていません)
    [SerializeField] public  int iSlotID;

    //  各種状態管理bool
    public  bool isSetItem, isMouseOver;

    private void Start()
    {
        //  動的にマネージャー取得
        cpInventoryManager = GameObject.Find("InventoryManager").GetComponent<InventoryManagerVer>();
    }
    private void Update()
    {
        //  一時的に格納したアイテムがあれば
        if (cpOverlapItem != null)
        {
            //  一時的に保管したアイテムがマウスに引っ付いている + 自身にセットされていなければ
            if (!cpOverlapItem.isGetItem && !isSetItem)
            {
                //  引数のアイテムを取得　場所を設定する関数呼び出し
                SetItem(cpOverlapItem.gameObject);
            }
        }

        //  もし上記で関数が呼ばれたら(SetItem内でcpInSlotItemに代入される)
        if (cpInSlotItem != null)
            if (Input.GetMouseButtonDown(1) && isMouseOver)
            {
                cpInventoryManager.btnListReset();
                if (enMode == SlotMode.Storage)
                {
                    //  マウスが重なっている + マウスの右クリック押したときにマネージャーの関数呼び出し
                    cpInventoryManager.SetWindowStat(cpInSlotItem);
                }
            }
    }
    private void OnMouseEnter()
    {
        //  マウスが重なったら呼ばれる関数　※RigidBody2D必須
        isMouseOver = true;
        Debug.Log("通った");
        if (isSetItem == true)
        {
            PlayerInfo.Instance.OnHover(0);
        }
       
    }
    private void OnMouseExit()
    {
        //  マウスが重ならなくなったら呼ばれる関数　※RigidBody2D必須
        isMouseOver = false;
        PlayerInfo.Instance.OnUnhover();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        //  アイテムが重なったら　※RigidBody2D必須
        Debug.Log("SlotHit!!  ID = " + iSlotID);
        if (collision.GetComponent<NewItem>() != null)
        {
            //  一時的に変換に格納
            cpOverlapItem = collision.GetComponent<NewItem>();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //  アイテムがコライダーから抜けたら
        if (collision.GetComponent<NewItem>() != null)
        {
            //  自身の変数からアイテムを外して初期状態にする
            cpInSlotItem = null;
            cpOverlapItem = null;
            isSetItem = false;
        }
    }
    public void EnableSlot()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }
    public void DisableSlot()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }
    public  void SetItem(GameObject goItem)
    {
        //  引数のオブジェクトからアイテムスクリプトを取得して場所を設定する
        cpInSlotItem = goItem.GetComponent<NewItem>();
        goItem.transform.position = transform.position;
        isSetItem = true;
    }

    //  ↓↓↓getter　setter↓↓↓
    public  void SetItemSlot(NewItem _ValueItem)
    {
        cpInSlotItem = _ValueItem;
    }
    public NewItem GetItem()
    {
        return cpInSlotItem;
    }
    public int GetSlotID()
    {
        return iSlotID;
    }
   
}
