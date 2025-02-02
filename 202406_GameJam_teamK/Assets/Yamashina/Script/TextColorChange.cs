using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextColorChange : MonoBehaviour

{
    [SerializeField] Text Text;


    [SerializeField]GameObject Button;
        [SerializeField]Sprite Sprite;
    [SerializeField] Sprite sprite1;
    // Start is called before the first frame update
    void Start()
    {
        string colorString = "#FFFFFF"; // �ԐF��16�i��������
        Color newColor;
        ColorUtility.TryParseHtmlString(colorString, out newColor); // �V����Color���쐬
        Text.color = newColor;
        Button.GetComponent<Image>().sprite = Sprite;

    }
  
    public void Text_ColorChange()
    {
        string colorString = "#27289F"; // �ԐF��16�i��������
        Color newColor;
        ColorUtility.TryParseHtmlString(colorString, out newColor); // �V����Color���쐬
        Text.color = newColor;
        Button.GetComponent<Image>().sprite =sprite1;

    }
    public void Text_ColorRestart()
    {
        string colorString = "#FFFFFF"; // �ԐF��16�i��������
        Color newColor;
        ColorUtility.TryParseHtmlString(colorString, out newColor); // �V����Color���쐬
        Text.color = newColor;
        Button.GetComponent<Image>().sprite = Sprite;

    }



}
