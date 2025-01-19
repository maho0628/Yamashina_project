using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StroragePanel : PanelBase
{
    [SerializeField]
    Button close_button;
    protected override void Awake()
    {
        OnStateChange.AddListener((enable) =>
        {

            if (enable)
            {
                PlayerInfo.Instance.Inventry.SetVisible(true);
                gameObject.SetActive(true);


            }
        });
        close_button.onClick.AddListener(() => PlayerInfo.Instance.Inventry.SwitchVisible());
    }


    protected override void Start()
    {

        SetSortOrder(OrderOfUI.NormalPanel);


    }

    // Update is called once per frame
    protected override void Update()
    {
        if (PlayerInfo.InstanceNullable == null) return;

        if (gameObject.activeSelf && !PlayerInfo.Instance.Inventry.GetVisibility())
        {
            gameObject.SetActive(false);

        }
    }
}
