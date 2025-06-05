using UnityEngine;

/// <summary>
/// ダイアログ要素の設定
/// </summary>
[System.Serializable]
public class DialogElementSettings
{
    [SerializeField, Header("オブジェクト名設定")]
    private string messageObjectName = "MessageText";
    [SerializeField]
    private string confirmButtonName = "ConfirmButton";
    [SerializeField]
    private string cancelButtonName = "CancelButton";

  
    [SerializeField, Header("テキスト設定")]
    private string messageText = "ゲームを終了しますか？";
    [SerializeField]
    private string confirmButtonText = "はい";
    [SerializeField]
    private string cancelButtonText = "いいえ";

    [SerializeField, Header("色設定（オプション）")]
    private bool useCustomColors = false;

    [SerializeField]
    private Color confirmButtonColor = Color.green;
    [SerializeField]
    private Color cancelButtonColor = Color.red;
    [SerializeField]
    private Color messageTextColor = Color.white;


    [SerializeField, Header("フォントサイズ設定（オプション）")]
    private bool useCustomFontSize = false;
    [SerializeField]
    private int messageFontSize = 100;
    [SerializeField]
    private int cancelFontSize = 100;
    [SerializeField]
    private int confirmFontSize = 100;
    // プロパティ（読み書き可能）
    public string MessageObjectName
    {
        get { return messageObjectName; }
        set { messageObjectName = value; }
    }

    public string ConfirmButtonName
    {
        get { return confirmButtonName; }
        set { confirmButtonName = value; }
    }

    public string CancelButtonName
    {
        get { return cancelButtonName; }
        set { cancelButtonName = value; }
    }

    public string MessageText
    {
        get { return messageText; }
        set { messageText = value; }
    }

    public string ConfirmButtonText
    {
        get { return confirmButtonText; }
        set { confirmButtonText = value; }
    }

    public string CancelButtonText
    {
        get { return cancelButtonText; }
        set { cancelButtonText = value; }
    }

    public bool UseCustomColors
    {
        get { return useCustomColors; }
        set { useCustomColors = value; }
    }

    public Color ConfirmButtonColor
    {
        get { return confirmButtonColor; }
        set { confirmButtonColor = value; }
    }

    public Color CancelButtonColor
    {
        get { return cancelButtonColor; }
        set { cancelButtonColor = value; }
    }

    public Color MessageTextColor
    {
        get { return messageTextColor; }
        set { messageTextColor = value; }
    }

    public bool UseCustomFontSize
    {
        get { return useCustomFontSize; }
        set { useCustomFontSize = value; }
    }
    public int MessageFontSize
    {
        get { return messageFontSize; }
        set { messageFontSize = value; }
    }

    public int ConfirmFontSize
    {
        get { return confirmFontSize; } 
        set { confirmFontSize = value; }    
    }
    public int CancelFontSize
    {
        get { return cancelFontSize; }      
        set { cancelFontSize = value; } 
    }
}