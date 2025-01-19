using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningPanel : PanelBase
{

    protected override void Start()
    {
        SetSortOrder(OrderOfUI.NormalPanel);
    }
}
