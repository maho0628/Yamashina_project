using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewCraftSystem : MonoBehaviour
{
    public NewCraftList cpList;
    public GameObject[] prefabCraftItem;

    public InventorySlotVer cpFirstItemSlot, cpSecondItemSlot, cpThirdItemSlot, cpCraftItemSpawnPos;

    /// <summary>
    /// ���g�̃��X�g����A���݃A�C�e���X���b�g�Ŏ擾���Ă���A�C�e���̑g�ݍ��킹�ŃN���t�g�ł��邩���m�F���܂��B
    /// ���X�g���ɃN���t�g�\�Ȃ��̂�����΂���ɕt������v���n�u���X�|�[�������܂��B
    /// </summary>
    public void CraftStart()
    {
        var _ItemNum = cpList.CraftItem(cpFirstItemSlot.GetItem(), cpSecondItemSlot.GetItem(), cpThirdItemSlot.GetItem());
        if (_ItemNum != -1)
        {
            Instantiate(prefabCraftItem[_ItemNum], cpCraftItemSpawnPos.transform.position, cpCraftItemSpawnPos.transform.rotation);
        }
        else
            Debug.LogWarning("�N���t�g�V�X�e������󂯎�����l��-1�ł��B");

    }
}
