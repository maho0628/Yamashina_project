using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftSystem : MonoBehaviour
{
    //
    //  ���ۂɃN���t�g�֐����ĂԃN���X�ł�
    //  CraftID�N���X���Ǘ����Ă���CraftList���Ǘ����Ă��܂��B
    //

    //  CraftList�N���X���擾
    [SerializeField] CraftList cpCraftList;

    //  �N���t�g�e�[�u����̃A�C�e����⊮����ϐ�
    public InventorySlotVer cpFirstItemSlot, cpSecondItemSlot, cpThirdItemSlot, cpCraftItemSpawnPos;

    void Update()
    {
        //  �ȉ��̕��@�̓e�X�g�ł��B
        //  �V�[����ŉ��炩�̕��@�Ŏ擾���āAcpFirstItemSlot��cpSecondItemSlot�ɑ�����Ă���֐�����т����Ă��������B
        //  �ʂɊ֐��ɂ��Ȃ��Ă������ł����A�֐��ɂ��Ă����ƁA��X�炭����ł��ˁB

    }
    //public void CraftStart()
    //{
    //    ����̃{�^���ŌĂяo���悤�ɂ��܂����BUnity�����Ă݂Ă�

    //  var _item = cpCraftList.TwoItemCraft(cpFirstItemSlot.GetItem(), cpSecondItemSlot.GetItem());
    //    var�ϐ��Ń��[�J���ϐ��Ƃ��Ď擾���Ă����ƁA�ȉ��̂悤�ɃX�|�[���ꏊ���w��ł����
    //    if (_item != null)
    //    {
    //        cpCraftItemSpawnPos.SetItem(_item);
    //        Debug.Log("�N���t�g�ɐ���");
    //    }
    //}
}
