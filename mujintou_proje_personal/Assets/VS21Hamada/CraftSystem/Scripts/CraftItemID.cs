using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftItemID : MonoBehaviour
{
    //
    //  �N���t�g�̑g�ݍ��킹��ݒ肷��N���X�ł��B
    //

    //  �N���t�g����̂ɕK�v�ȃA�C�e��ID
    [Header("�N���t�g�A�C�e��ID"), SerializeField]
    private int FirstID;
    [SerializeField] int SecondID;

    //  ��L��ID�ŃN���t�g�ł���A�C�e��
    [SerializeField] private GameObject goCraftItem;

    //  ID���O������擾����֐�
    public int GetID(bool isFirstID)
    {
        if (isFirstID)
        {
            return FirstID;
        }
        else
        {
            return SecondID;
        }
    }
    //  �N���t�g�A�C�e�����擾����֐�
    public GameObject GetCraftItem()
    {
        return goCraftItem;
    }
}
