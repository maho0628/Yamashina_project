using UnityEngine;

/// <summary>
/// ダイアログ要素の設定
/// </summary>
[System.Serializable]
public class DialogElementSettings
{
    [SerializeField, Header("オブジェクト名設定")]

    private string messageObjectName = "MessageText";
    [SerializeField, Header("オブジェクト名設定")]

    private string confirmButtonName = "ConfirmButton";
    [SerializeField, Header("オブジェクト名設定")]

    private string cancelButtonName = "CancelButton";

    [SerializeField, Header("テキスト設定")]
    private string messageText = "ゲームを終了しますか？";
    [SerializeField, Header("テキスト設定")]

    private string confirmButtonText = "はい";
    [SerializeField, Header("テキスト設定")]

    private string cancelButtonText = "いいえ";

    [SerializeField, Header("色設定（オプション）")]
    private bool useCustomColors = false;

    private Color confirmButtonColor = Color.green;
    [SerializeField, Header("色設定（オプション）")]

    private Color cancelButtonColor = Color.red;
    [SerializeField, Header("色設定（オプション）")]

    private Color messageTextColor = Color.white;


    internal string MessageObjectName { get { return messageObjectName; } }

    internal string ConfirmButtonName { get { return confirmButtonName; } }

    internal string CancelButtonName { get {return cancelButtonName; } }    
    internal string MessageText { get { return messageText; } }
    internal string ConfirmButtonText { get { return confirmButtonText; } }
    internal string CancelButtonText { get {return cancelButtonText; } }

    internal bool UseCustomColors { get { return useCustomColors; } }

    internal Color ConfirmButtonColor { get { return confirmButtonColor; } }
    internal Color CancelButtonColor { get {return cancelButtonColor; } }
    internal Color MessageTextColor { get { return messageTextColor; } }


}
