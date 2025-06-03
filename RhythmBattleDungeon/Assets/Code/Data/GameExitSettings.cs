using UnityEngine;

/// <summary>
/// ゲーム終了処理の設定を管理するスクリプタブルオブジェクト（高度版）
/// </summary>
[CreateAssetMenu(fileName = "GameExitSettings", menuName = "Game Settings/Game Exit Settings")]
public class GameExitSettings : ScriptableObject
{
    [Header("確認ダイアログ設定")]
    [SerializeField] private bool showConfirmDialog = true;
    [SerializeField] private DialogElementSettings dialogSettings = new DialogElementSettings();

    [Header("シーン設定")]
    [SerializeField] private SceneReference menuSceneReference;

    [Header("保存設定")]
    [SerializeField] private string saveFileName = "savedata.json";
    [SerializeField] private GameExitManager.SaveLocation saveLocation = GameExitManager.SaveLocation.PersistentDataPath;
    [SerializeField] private string customSavePath = "";

    [Header("UI設定")]
    [SerializeField] private GameObject confirmDialogPrefab;

    [Header("タイムアウト設定")]
    [SerializeField] private float exitTimeoutSeconds = 10f;

    // プロパティでアクセス
    public bool ShowConfirmDialog => showConfirmDialog;
    public DialogElementSettings DialogSettings => dialogSettings;
    public string SaveFileName => saveFileName;
    public GameExitManager.SaveLocation SaveLocation => saveLocation;
    public string CustomSavePath => customSavePath;
    public GameObject ConfirmDialogPrefab => confirmDialogPrefab;
    public float ExitTimeoutSeconds => exitTimeoutSeconds;

    internal SceneReference MenuSceneReference => menuSceneReference;
    private void OnValidate()
    {
        if (string.IsNullOrEmpty(saveFileName))
        {
            saveFileName = "savedata.json";
        }

        if (exitTimeoutSeconds <= 0)
        {
            exitTimeoutSeconds = 10f;
        }

        if (dialogSettings == null)
        {
            dialogSettings = new DialogElementSettings();
        }

        if (!string.IsNullOrEmpty(customSavePath) && saveLocation != GameExitManager.SaveLocation.Custom)
        {
            Debug.LogWarning("CustomSavePathが設定されているため、SaveLocationをCustomに変更します。");
        }
    }
}