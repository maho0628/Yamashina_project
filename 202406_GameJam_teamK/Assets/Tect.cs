using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tect : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string colorString = "#FFFFFF"; // �ԐF��16�i��������
        Color newColor;
        ColorUtility.TryParseHtmlString(colorString, out newColor); // �V����Color���쐬
       

        Text text = GetComponent<Text>();
        text.color = newColor;
        text.text=Gamemanager.score.ToString();
    }
   

    public void Text_ColorChange()
    {
        string colorString = "#27289F"; // �ԐF��16�i��������
        Color newColor;
        Text text = GetComponent<Text>();

        ColorUtility.TryParseHtmlString(colorString, out newColor); // �V����Color���쐬
        text.color = newColor;

    }
    public void Text_ColorRestart()
    {
        string colorString = "#FFFFFF"; // �ԐF��16�i��������
        Color newColor;
        Text text = GetComponent<Text>();

        ColorUtility.TryParseHtmlString(colorString, out newColor); // �V����Color���쐬
        text.color = newColor;

    }
    // Update is called once per frame
    
}
