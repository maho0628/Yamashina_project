using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

/// <summary>
/// ゲーム終了処理の設定を管理するスクリプタブルオブジェクト（高度版）
/// </summary>
[CreateAssetMenu(fileName = "GameExitSettings", menuName = "Game Settings/Game Exit Settings")]
public class GameExitSettings : ScriptableObject
{
    [Header("確認ダイアログ設定")]
    [SerializeField] private bool showConfirmDialog = true;
    [SerializeField] private DialogElementSettings dialogSettings = new DialogElementSettings();
   

    [Header("UI設定")]
    [SerializeField] private GameObject confirmDialogPrefab;

   

    // プロパティでアクセス
    public bool ShowConfirmDialog
    {
        get { return showConfirmDialog; }
        set { showConfirmDialog = value; }
    }
    public DialogElementSettings DialogSettings
    { 
        get { return dialogSettings; } 
        set { dialogSettings = value; } 
    }



    public GameObject ConfirmDialogPrefab => confirmDialogPrefab;
   

    private void OnValidate()
    {
       

       

        if (dialogSettings == null)
        {
            dialogSettings = new DialogElementSettings();
        }

        
    }
}