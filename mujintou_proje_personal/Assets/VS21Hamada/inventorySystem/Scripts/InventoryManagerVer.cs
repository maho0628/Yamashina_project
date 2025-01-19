using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InventoryManagerVer : MonoBehaviour
{

    //  �e�L�X�g�\���p
    public Text txName, txInfo;
    [SerializeField] private Button btnUseButton, btnTrashButton;
    [SerializeField] NewItem dummy;
    private NewItem item;
    private bool isClick, isItemFull;
    [SerializeField, Header("���̔z��̍Ō�ɂ͕K���̂Ă��ʂ̃X���b�g�����Ă�������")] InventorySlotVer[] cpSlots;
    [SerializeField] TrashItemSystem cpTrashSystem;

    private void Start()
    {
        //  Slot�̍Ō�ɂ���X���b�g���\���ɂ���B
        cpSlots[cpSlots.Length - 1].DisableSlot();
    }
    private void Update()
    {
        //  �Ō�̃X���b�g�ɃA�C�e�������邩�ǂ���
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
    /// �V������ɓ��ꂽ�A�C�e�����X���b�g�Ɋi�[����֐��B
    /// ���ӂ��ꍇ�͂ǂꂩ���̂Ă�E�B���h�E���o�Ă���B
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
                //  ���ӂ��
                Debug.Log("���ꂽ");
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
    ///  �����̃A�C�e���̏���\������
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