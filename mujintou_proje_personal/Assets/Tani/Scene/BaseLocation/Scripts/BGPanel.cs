using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BGPanel : MonoBehaviour
{
    [SerializeField]
    Image BG_image_obj;
    [SerializeField]
    List<Sprite> BG_Sprites;


    public void SetBGSprite(int index)
    {
        BG_image_obj.sprite = BG_Sprites[index];
    }
}
