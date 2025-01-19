using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ImageViewWithSprites : MonoBehaviour
{
    [SerializeField]
    List<Sprite> spriteDatas;
    [SerializeField]
    Image img_obj;

    public void SetImageSprite(int index)
    {
        if(!(index < spriteDatas.Count))
        {
            Debug.LogError("index out of range");
            return;
        }
        img_obj.sprite = spriteDatas[index];
    }

    public void SetImageColor(Color color)
    {
        img_obj.color = color;
    }

}
