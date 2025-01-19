using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashItemSystem : MonoBehaviour
{
    //�A�C�e�������ׂĎ̂Ă�ꍇ��16�ɂȂ邽�߁������Ŏw��B
    public NewItem[] TrashItems = new NewItem[16];
    private Collider2D AreaCollider;
    private SpriteRenderer srArea;
    [SerializeField] GameObject goTrashSet;
    private void Start()
    {
        //  Component�擾
        srArea = GetComponent<SpriteRenderer>();
        AreaCollider = GetComponent<Collider2D>();
        DisableTrashArea();
    }

    /// <summary>
    /// �̂Ă�G���A���o�����Ă��邩���擾�ł���B
    /// </summary>
    /// <returns></returns>
    public bool GetTrashAreaisShow()
    {
        if (srArea.enabled)
            return true;
        else
            return false;
    }
    /// <summary>
    /// �̂Ă�G���A�������B
    /// </summary>
    public void DisableTrashArea()
    {
        goTrashSet.SetActive(false);
        srArea.enabled = false;
        AreaCollider.enabled = false;
    }
    /// <summary>
    /// �̂Ă�G���A���o��������B
    /// </summary>
    public void EnableTrashArea()
    {
        goTrashSet.SetActive(true);
        srArea.enabled = true;
        AreaCollider.enabled = true;
    }
    /// <summary>
    /// �̂Ă�G���A���ɂ���A�C�e�����ׂĂ����ł�����B
    /// </summary>
    public void OnTrashItem()
    {
        for (int i = 0; i < TrashItems.Length; i++)
        {
            if (TrashItems[i] == null) continue;

            TrashItems[i].TrashItem();
            TrashItems[i] = null;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //  �����Ă����A�C�e�����擾����B
        if (collision.tag == "Item")
        {
            for (int i = 0; i < TrashItems.Length; i++)
            {
                if (TrashItems[i] == null)
                {
                    TrashItems[i] = collision.GetComponent<NewItem>();
                    break;
                }
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //  �o�čs�����A�C�e����z�񂩂�폜����B
        for (int i = 0; i < TrashItems.Length; i++)
        {
            if (collision.gameObject == TrashItems[i].gameObject)
            {
                TrashItems[i] = null;
                break;
            }
        }
    }
}
