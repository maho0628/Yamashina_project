using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextColorChange : MonoBehaviour

{
    [SerializeField] Text Text;
    Button button;  // Start is called before the first frame update
    void Start()
    {
        string colorString = "#4D4D4D"; // 赤色の16進数文字列
        Color newColor;
        ColorUtility.TryParseHtmlString(colorString, out newColor); // 新しくColorを作成
        Text.color = newColor;
        button = GetComponent<Button>();

    }

    public void Text_ColorChange()
    {
        string colorString = "#1B1464"; // 赤色の16進数文字列
        Color newColor;
        ColorUtility.TryParseHtmlString(colorString, out newColor); // 新しくColorを作成

        Text.color = newColor;
        Debug.Log(button.interactable);
        Debug.Log(Text.color);
    }
    public void Text_ColorRestart()
    {
        string colorString = "#4D4D4D"; // 赤色の16進数文字列
        Color newColor;
        ColorUtility.TryParseHtmlString(colorString, out newColor); // 新しくColorを作成
        Text.color = newColor;
        //Debug.Log(button.interactable);
        Debug.Log(Text.color);
    }


    private void OnGUI()
    {
#if UNITY_STANDALONE_WIN
        if (!Application.isEditor)
        {
            return;  // エディタ以外の場合は何も描画しない
        }
        GUI.Label(new Rect(10.0f,10.0f,Screen.width,Screen.height),Text.color.ToString());

#endif
    }
}
