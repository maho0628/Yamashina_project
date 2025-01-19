using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetItemManager : MonoBehaviour
{
    [SerializeField,Header("�S�A�C�e���i�[�K�{")] GameObject[] goAllItemPrefabs;
    [SerializeField, Header("inventory�}�l�[�W���[�擾���Ă�")] InventoryManagerVer cpInventoryManager;

    private void Update()
    {
        //  debug�p�BUpdate���̓R�����g�A�E�g���Ă�
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (!cpInventoryManager.GetIsItemFull())
                GetNewItem(Random.Range(4, 68));
        }
    }
    /// <summary>
    /// �����ɃA�C�e��ID�����鎖�ŐV���ɃA�C�e�����o�������邱�Ƃ��ł���B
    /// �������ꂽ�A�C�e���͎����I�ɋ󂢂Ă���X���b�g�ɓ������B
    /// </summary>
    /// <param name="_ItemID"></param>
    public void GetNewItem(int _ItemID)
    {
         Transform parent = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Transform>();

        var item = Instantiate(goAllItemPrefabs[_ItemID],parent);
        item.transform.localScale *= 20;
        cpInventoryManager.SetNewGetItem(item);
    }
}
