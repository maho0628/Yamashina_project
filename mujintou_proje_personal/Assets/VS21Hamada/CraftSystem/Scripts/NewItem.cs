using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewItem : MonoBehaviour
{
    public SO_Item ScriptalItem;
    public int CurrentStackCount;
    public bool isOverMouse, isGetItem, isOverSlot;
    public Text txStack;

    [SerializeField] private NewItem OverlapItem;

    // Update is called once per frame
    private void Start()
    {
        CurrentStackCount = ScriptalItem.GetStackCount();
        txStack.text = CurrentStackCount.ToString();
    }
    void Update()
    {
        if (CurrentStackCount <= 0)
        {
            if (this.gameObject)
                Destroy(this.gameObject);
        }
        if (isGetItem)
        {
            gameObject.transform.position =
                new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x,
                Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        }

        if (isOverMouse)
        {
            if (OverlapItem != null)
            {
                if (Input.GetMouseButtonDown(0) && isGetItem)
                {
                    if (OverlapItem.CurrentStackCount < OverlapItem.ScriptalItem.stackCount)
                    {
                        //  �X�^�b�N�ړ�����
                        var addStackCount = OverlapItem.ScriptalItem.stackCount - OverlapItem.CurrentStackCount;
                        if (addStackCount >= CurrentStackCount) addStackCount = CurrentStackCount;
                        OverlapItem.AddStack(addStackCount);
                        this.MinStack(addStackCount);
                        Debug.Log("�X�^�b�N�ړ��F�� " + addStackCount);
                    }
                }
            }
            else
            {
                if (Input.GetMouseButtonDown(0))
                {
                    //  �����Ă��Ȃ��ĂȂ������N���b�N
                    if (isOverSlot)
                        isGetItem = !isGetItem;
                }
            }

            //if (Input.GetMouseButtonDown(0) && isGetItem)
            //{
            //    //  �����Ă��ĂȂ������N���b�N
            //    isGetItem = !isGetItem;
            //}
        }
    }
    /// <summary>
    /// �A�C�e�����g�p����֐��B���I�Ƀ{�^����Onclick���X�g�Ɋi�[���Ďg�p���Ă��܂��B
    /// �ǉ��Ńv���C���[�։񕜒l�𑗂�X�N���v�g�ǉ��K�v�B
    /// </summary>
    public void UseItem()
    {
        //  �����Ńv���C���[�̃X�e�[�^�X�񕜊֐����Ă�
        MinStack(1);
    }
    /// <summary>
    /// �A�C�e�������ł����܂��B
    /// </summary>
    public void TrashItem()
    {
        MinStack(CurrentStackCount);
    }
    public void AddStack(int _Value)
    {
        CurrentStackCount += _Value;
        txStack.text = CurrentStackCount.ToString();
    }
    public void MinStack(int _Value)
    {
        CurrentStackCount -= _Value;
        if (CurrentStackCount <= 0)
        {
            GameObject.Find("InventoryManager").GetComponent<InventoryManagerVer>().btnListReset();
        }
        txStack.text = CurrentStackCount.ToString();
        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "PutItemArea")
        {
            isOverSlot = true;
        }
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<NewItem>() == null) return;

        if (other.GetComponent<NewItem>().ScriptalItem.itemID == this.ScriptalItem.itemID)
        {
            OverlapItem = other.GetComponent<NewItem>();
        }
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "PutItemArea")
        {
            isOverSlot = false;
        }
        if (other.GetComponent<NewItem>() == null) return;

        if (other.GetComponent<NewItem>().ScriptalItem.itemID == this.ScriptalItem.itemID)
        {
            OverlapItem = null;
        }
    }
    private void OnMouseOver()
    {
        //isOverMouse = true;
    }
    private void OnMouseEnter()
    {
        isOverMouse = true;
    }
    private void OnMouseExit()
    {
        isOverMouse = false;
    }

}
