using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewCraftList : MonoBehaviour
{
    public SO_Item[] Items;

    public int fID, sID, tID;

    /// <summary>
    /// �󂯎��������3�̑g�ݍ��킹�ō쐬�ł���A�C�e����ID��T���܂��B
    /// �Ԃ�l�̓N���t�g�ł�����̂�����΂��̃A�C�e��ID���A�����N���t�g�ł��Ȃ��ꍇ��-1���Ԃ���܂��B
    /// </summary>
    public int CraftItem(NewItem fItem, NewItem sItem, NewItem tItem)
    {
        for (int i = 0; i < 61; i++)
        {
            if (Items[i].itemID == 1) continue;

            if (Items[i].isCraftingItem)
            {
                Debug.Log(i + "���");
                if (fItem != null)
                {
                    fID = fItem.ScriptalItem.itemID;
                }
                else
                {
                    fID = 0;
                }

                if (sItem != null)
                {
                    sID = sItem.ScriptalItem.itemID;
                }
                else
                {
                    sID = 0;
                }

                if (tItem != null)
                {
                    tID = tItem.ScriptalItem.itemID;
                }
                else
                {
                    tID = 0;
                }


                //  �ݒ肳��Ă���ID��1�̏ꍇ
                if (fID != 0 && sID == 0 && tID == 0)
                {
                    Debug.LogWarning("�ݒ肳��Ă���ID���P��");
                    if (fID == Items[i].firstID)
                    {
                        Debug.LogWarning("Items[" + i + "]��ID��v");
                        if (fItem.CurrentStackCount >= Items[i].fItemCount)
                        {
                            Debug.LogWarning("������Ă��܂��B");
                            fItem.MinStack(Items[i].fItemCount);
                            return i;
                        }
                    }
                    else
                        continue;
                }
                //  �ݒ肳��Ă���ID��2�̏ꍇ
                else if (fID != 0 && sID != 0 && tID == 0)
                {
                    Debug.LogWarning("�ݒ肳��Ă���ID���Q��");
                    if (fID == Items[i].firstID && sID == Items[i].secondID)
                    {
                        Debug.LogWarning("Items[" + i + "]��ID��v");
                        if (fItem.CurrentStackCount >= Items[i].fItemCount && sItem.CurrentStackCount >= Items[i].sItemCount)
                        {
                            Debug.LogWarning("������Ă��܂��B");
                            fItem.MinStack(Items[i].fItemCount);
                            sItem.MinStack(Items[i].sItemCount);
                            return i;
                        }
                    }
                    else
                        continue;
                }
                //  �ݒ肳��Ă���ID��3�̏ꍇ
                else if (fID != 0 && sID != 0 && tID != 0)
                {
                    Debug.LogWarning("�ݒ肳��Ă���ID���R��");
                    if (fID == Items[i].firstID && sID == Items[i].secondID && tID == Items[i].ThirdID)
                    {
                        Debug.LogWarning("Items[" + i + "]��ID��v");
                        if (fItem.CurrentStackCount >= Items[i].fItemCount && sItem.CurrentStackCount >= Items[i].sItemCount && tItem.CurrentStackCount >= Items[i].tItemCount)
                        {
                            Debug.LogWarning("������Ă��܂��B");
                            fItem.MinStack(Items[i].fItemCount);
                            sItem.MinStack(Items[i].sItemCount);
                            tItem.MinStack(Items[i].tItemCount);
                            return i;
                        }
                    }
                    else
                        continue;
                }
            }
            else
            {
                Debug.LogWarning("�N���t�g�\�ł͂Ȃ��I�u�W�F�N�g�����X�g���ɂ���܂��B(" + i + "�Ԓn)");
                return -1;
            }
        }
        Debug.LogWarning("���X�g��For�����I�����܂����B�G���[");
        return -1;
    }
}
