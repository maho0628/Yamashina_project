using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainPanel_BL_Tu_1 : PanelBase
{
    [SerializeField]
    Image bg_image_obj;
    [SerializeField]
    List<Sprite> extra_sprites;

    int current_sprite_index = 0;
    PlayerInfo info;
    protected override void Awake()
    {
        //ƒpƒlƒ‹ŠÖŒW
        info = PlayerInfo.Instance;

    }

    protected override void Start()
    {
        SetSortOrder(OrderOfUI.MainPanel);
    }

    protected override void Update()
    {
        base.Update();

        if (info.Fire != 0 && current_sprite_index != 1)
        {
            bg_image_obj.sprite = extra_sprites[1];
            current_sprite_index = 1;
        }
        else if (info.Fire == 0 && current_sprite_index != 0)
        {
            bg_image_obj.sprite = extra_sprites[0];
            current_sprite_index = 0;
        }
    }
}
