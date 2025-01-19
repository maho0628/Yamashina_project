using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugItemGet : MonoBehaviour
{
    [SerializeField] Items.Item_ID getItem;
    [SerializeField] int num = 1;
    public void DebugItemGetter()
    {
        PlayerInfo.Instance.Inventry.GetItem(getItem, num);
        PlayerInfo.Instance.Health = 100;
        PlayerInfo.Instance.ActionValue = 5;
    }
}
