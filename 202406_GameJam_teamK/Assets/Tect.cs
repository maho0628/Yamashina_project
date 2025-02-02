using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tect : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string colorString = "#FFFFFF"; // 赤色の16進数文字列
        Color newColor;
        ColorUtility.TryParseHtmlString(colorString, out newColor); // 新しくColorを作成
       

        Text text = GetComponent<Text>();
        text.color = newColor;
        text.text=Gamemanager.score.ToString();
    }
   

    public void Text_ColorChange()
    {
        string colorString = "#27289F"; // 赤色の16進数文字列
        Color newColor;
        Text text = GetComponent<Text>();

        ColorUtility.TryParseHtmlString(colorString, out newColor); // 新しくColorを作成
        text.color = newColor;

    }
    public void Text_ColorRestart()
    {
        string colorString = "#FFFFFF"; // 赤色の16進数文字列
        Color newColor;
        Text text = GetComponent<Text>();

        ColorUtility.TryParseHtmlString(colorString, out newColor); // 新しくColorを作成
        text.color = newColor;

    }
    // Update is called once per frame
    
}
