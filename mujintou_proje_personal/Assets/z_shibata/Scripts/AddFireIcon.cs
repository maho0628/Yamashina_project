using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddFireIcon : MonoBehaviour
{
    [SerializeField] SlotManager addSlot;
    [SerializeField] GameObject branchImage;

    private void Update()
    {
        var a = addSlot.GetSlotItem(0).Value.id;
        if( a == Items.Item_ID.EmptyObject )
        {
            branchImage.SetActive(true);
        }
        else if(a != Items.Item_ID.EmptyObject)
        {
            branchImage.SetActive(false);
        }
    }
}
