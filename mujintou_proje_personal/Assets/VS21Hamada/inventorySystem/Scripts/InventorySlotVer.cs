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

    //  �N���t�g�V�X�e���ɗ��p����悤(���݂͎g���Ă��܂���)
    public SlotMode enMode;

    //  �}�l�[�W���[�擾
    public InventoryManagerVer cpInventoryManager;

    //  ���g�̒��ɓ��I�Ɋm�ۂ���A�C�e���ϐ�
    public NewItem cpInSlotItem;

    //  �ꎞ�I�Ɋm�ۂ���ϐ�
    private NewItem cpOverlapItem;

    //  ���g�̃X���b�g��ID(���݂͎g���Ă��܂���)
    [SerializeField] public  int iSlotID;

    //  �e���ԊǗ�bool
    public  bool isSetItem, isMouseOver;

    private void Start()
    {
        //  ���I�Ƀ}�l�[�W���[�擾
        cpInventoryManager = GameObject.Find("InventoryManager").GetComponent<InventoryManagerVer>();
    }
    private void Update()
    {
        //  �ꎞ�I�Ɋi�[�����A�C�e���������
        if (cpOverlapItem != null)
        {
            //  �ꎞ�I�ɕۊǂ����A�C�e�����}�E�X�Ɉ����t���Ă��� + ���g�ɃZ�b�g����Ă��Ȃ����
            if (!cpOverlapItem.isGetItem && !isSetItem)
            {
                //  �����̃A�C�e�����擾�@�ꏊ��ݒ肷��֐��Ăяo��
                SetItem(cpOverlapItem.gameObject);
            }
        }

        //  ������L�Ŋ֐����Ă΂ꂽ��(SetItem����cpInSlotItem�ɑ�������)
        if (cpInSlotItem != null)
            if (Input.GetMouseButtonDown(1) && isMouseOver)
            {
                cpInventoryManager.btnListReset();
                if (enMode == SlotMode.Storage)
                {
                    //  �}�E�X���d�Ȃ��Ă��� + �}�E�X�̉E�N���b�N�������Ƃ��Ƀ}�l�[�W���[�̊֐��Ăяo��
                    cpInventoryManager.SetWindowStat(cpInSlotItem);
                }
            }
    }
    private void OnMouseEnter()
    {
        //  �}�E�X���d�Ȃ�����Ă΂��֐��@��RigidBody2D�K�{
        isMouseOver = true;
        Debug.Log("�ʂ���");
        if (isSetItem == true)
        {
            PlayerInfo.Instance.OnHover(0);
        }
       
    }
    private void OnMouseExit()
    {
        //  �}�E�X���d�Ȃ�Ȃ��Ȃ�����Ă΂��֐��@��RigidBody2D�K�{
        isMouseOver = false;
        PlayerInfo.Instance.OnUnhover();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        //  �A�C�e�����d�Ȃ�����@��RigidBody2D�K�{
        Debug.Log("SlotHit!!  ID = " + iSlotID);
        if (collision.GetComponent<NewItem>() != null)
        {
            //  �ꎞ�I�ɕϊ��Ɋi�[
            cpOverlapItem = collision.GetComponent<NewItem>();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //  �A�C�e�����R���C�_�[���甲������
        if (collision.GetComponent<NewItem>() != null)
        {
            //  ���g�̕ϐ�����A�C�e�����O���ď�����Ԃɂ���
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
        //  �����̃I�u�W�F�N�g����A�C�e���X�N���v�g���擾���ďꏊ��ݒ肷��
        cpInSlotItem = goItem.GetComponent<NewItem>();
        goItem.transform.position = transform.position;
        isSetItem = true;
    }

    //  ������getter�@setter������
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
