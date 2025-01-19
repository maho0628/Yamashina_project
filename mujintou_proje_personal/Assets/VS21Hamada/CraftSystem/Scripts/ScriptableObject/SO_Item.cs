using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemCategory
{
    Basic,
    Craft,
    Food,
    Special
}
public struct ItemParameter
{
    public int addHP;
    public int addHungry;
    public int addWater;
}
[CreateAssetMenu(menuName = "ScriptableObj_Item")]
public class SO_Item : ScriptableObject
{
    [Header("�A�C�e���̃J�e�S���[")]
    public ItemCategory enCategory;
    [Header("�A�C�e��ID")]
    public int itemID;
    [Header("�ő�X�^�b�N��")]
    public int stackCount;
    [Header("�A�C�e����")]
    public string itemName;
    [Header("�A�C�e������")]
    public string itemInfo;
    [Header("�N���t�g�ō���A�C�e���Ȃ̂�")]
    public bool isCraftingItem;
    [Header("�g�p�ł���A�C�e���Ȃ̂�")]
    public bool isUseItem;
    [Header("���̃A�C�e�������ɕK�v�ȃA�C�e���̊eID")]
    public int firstID;
    public int secondID, ThirdID;
    [Header("���̃A�C�e�������ɕK�v�Ȍ�")]
    public int fItemCount;
    public int sItemCount, tItemCount;
    [Header("�A�C�e���g�p���̉񕜗�"), SerializeField]
    private int addHealHP;
    [SerializeField]
    private int addHealWater, addHealHungry;

    /// <summary>
    /// �ꎞ�i�[�p�̕ϐ��B���̒��̏�񂪎擾�ł��܂��B
    /// </summary>
    private ItemParameter ItemParameter;

    /// <summary>
    ///   ���̃I�u�W�F�N�g�ɐݒ肳�ꂽ�̗́A�H���A�����̉񕜏����擾�ł��܂��B
    /// </summary>
    public ItemParameter GetHealValue()
    {
        ItemParameter.addHP = addHealHP;
        ItemParameter.addHungry = addHealHungry;
        ItemParameter.addWater = addHealWater;
        return ItemParameter;
    }
    /// <summary>
    /// �A�C�e���ɐݒ肳��Ă���ID���擾�ł��܂��B
    /// </summary>
    public int GetItemID()
    {
        return itemID;
    }
    /// <summary>
    /// �A�C�e���ɐݒ肳��Ă���ő�X�^�b�N�ʂ��擾�ł��܂�
    /// </summary>
    public int GetStackCount()
    {
        return stackCount;
    }
    /// <summary>
    /// ���̃I�u�W�F�N�g�̃X�^�b�N�ʂ�ݒ�ł��܂��B
    /// </summary>
    public void SetStackCount(int _Value)
    {
        stackCount = _Value;
    }
}
