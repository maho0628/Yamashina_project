using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainPanel : PanelBase
{
    ImageViewWithSprites image_view;

    
    //‚È‚ñ‚©‚±‚Ì–¼‘O‚¢‚â‚â‚È
    enum EBL_Situation
    {
        DayTimeNoFire = 0,
        DayTimeOnFire,
        NightNoFire,
        NightOnFire,
    }

    EBL_Situation situation = EBL_Situation.DayTimeNoFire;
    protected override void Awake()
    {
        image_view = GetComponentInChildren<ImageViewWithSprites>();

        //ƒpƒlƒ‹ŠÖŒW

        UpdateBG();
        PlayerInfo.Instance.OnFireSet += UpdateBG;

    }
    private void OnDestroy()
    {
        if (PlayerInfo.InstanceNullable)
        {
            PlayerInfo.Instance.OnFireSet -= UpdateBG;

        }
    }

    protected override void Start()
    {
        SetSortOrder(OrderOfUI.MainPanel);
    }

    protected override void Update()
    {
        base.Update();

        
    }

    void UpdateBG()
    {
        var info = PlayerInfo.Instance;
        if (info.Day.isDayTime)
        {
            if (info.Fire > 0) situation = EBL_Situation.DayTimeOnFire;
            else situation = EBL_Situation.DayTimeNoFire;
        }
        else
        {
            if (info.Fire > 0) situation = EBL_Situation.NightOnFire;
            else situation = EBL_Situation.NightNoFire;
        }

        image_view.SetImageSprite((int)situation);
    }

}
