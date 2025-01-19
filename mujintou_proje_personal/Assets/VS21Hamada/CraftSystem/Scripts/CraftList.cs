using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftList : MonoBehaviour
{
    //
    //  �֐��Ƃ��āA�N���t�g�ɕK�v�ȃA�C�e�����ǂ����𔻒肷��N���X�ł��B
    //  ������CraftItemID�N���X���Ǘ����Ă��܂��B
    //

    [Header("�N���t�g�������ʂ̃A�C�e���z��"), SerializeField]
    CraftItemID[] cpCraftPrefab;

    public GameObject TwoItemCraft(ItemHamada _fItem, ItemHamada _sItem)
    {
        //  ������Item�N���X����A���̃A�C�e����ID���擾
        int getFid = _fItem.GetItemID();
        int getSid = _sItem.GetItemID();

        //  for���Ŏ擾����ID�̑g�ݍ��킹�ŃN���t�g�ł�����̂����邩�`�F�b�N
        for (int i = 0; i < cpCraftPrefab.Length; i++)
        {
            //  �擾����ID�ƃN���t�g�\��ID���Ƃ炵���킹��
            if (getFid == cpCraftPrefab[i].GetID(true) && getSid == cpCraftPrefab[i].GetID(false))
            {
                return Instantiate(cpCraftPrefab[i].GetCraftItem());
            }
            //  ���]���ē���Ă��Ă��Ή�����p
            else if (getFid == cpCraftPrefab[i].GetID(false) && getSid == cpCraftPrefab[i].GetID(true))
            {
                return Instantiate(cpCraftPrefab[i].GetCraftItem());
            }
        }
        Debug.Log("ID:" + getFid + "-" + getSid + "�̃N���t�g�A�C�e���͑��݂��܂���");
        return null;
    }
}