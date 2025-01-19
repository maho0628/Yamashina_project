using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class Image_Method : MonoBehaviour
{
    [SerializeField] GameObject _imageObject;
    [SerializeField] GameObject _imagePrefab;


    // ‰æ‘œ‚ğ”z’u‚·‚é
    public void PutImage(Sprite sprite)
    {
        Sprite image = sprite;
        GameObject parentObject = _imageObject;

        Vector2 position = parentObject.transform.position;
        Quaternion rotation = Quaternion.identity;
        Transform parent = parentObject.transform;
        GameObject item = Instantiate(_imagePrefab, position, rotation, parent);
        item.GetComponent<Image>().sprite = image;

    }

    //‰æ‘œ‚ğíœ‚·‚é
    public void Remove_Image(Sprite sprite)
    {
        Destroy(sprite);
    }
}
