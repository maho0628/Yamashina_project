using UnityEngine;
using UnityEngine.InputSystem;
using Cysharp.Threading.Tasks;
using System.Threading;
using TMPro;
using UnityEngine.UI;

/// <summary>
/// ゲーム終了処理を管理する汎用クラス
/// </summary>
public class GameExitManager : SingletonMonoBehaviour<GameExitManager>
{
  

    [Header("UI設定（オプション）")]
    [SerializeField, Tooltip("確認ダイアログを生成する親キャンバス。未設定の場合は自動検出")]
    private Canvas targetCanvas;

    // 設定関連（スクリプタブルオブジェクトから取得）
    private GameExitSettings settings;

    // UI参照（動的に生成）
    private GameObject confirmDialog;

    

    public void InitializeConfirmSettings()
    {
        // GameInitializerの初期化を待つ
        InitializeAsync().Forget();
    }

    /// <summary>
    /// 非同期初期化処理
    /// </summary>
    private async UniTaskVoid InitializeAsync()
    {
        // GameInitializerの初期化完了を待機
        await UniTask.WaitUntil(() => GameInitializer.Instance != null && GameInitializer.Instance.Initialized);

        // 設定を取得
        LoadSettings();

  

        // 確認ダイアログを準備
        SetupConfirmDialog();
        confirmDialog.SetActive(true);

        Debug.Log("GameExitManager初期化完了");
    }

  
    /// <summary>
    /// 設定をGameInitializerから取得
    /// </summary>
    private void LoadSettings()
    {
        if (GameInitializer.Instance != null)
        {
            settings = GameInitializer.Instance.GetGameExitSettings();
        }

        // デフォルト設定を作成
        if (settings == null)
        {
            Debug.LogWarning("GameExitSettingsが見つかりません。デフォルト設定を作成します。");
            CreateDefaultSettings();
        }
    }

    /// <summary>
    /// デフォルト設定を作成
    /// </summary>
    private void CreateDefaultSettings()
    {
        // ScriptableObjectとして作成（実際には設定値だけを使用）
        settings = ScriptableObject.CreateInstance<GameExitSettings>();

        // デフォルトのダイアログ設定を作成
        if (settings.DialogSettings == null)
        {
            settings.DialogSettings = new DialogElementSettings
            {
                MessageText = "ゲームを終了しますか？",
                MessageObjectName = "MessageText",
                ConfirmButtonName = "ConfirmButton",
                CancelButtonName = "CancelButton",
                ConfirmButtonText = "はい",
                CancelButtonText = "いいえ",
                UseCustomColors = false,
                MessageTextColor = Color.white,
                ConfirmButtonColor = Color.green,
                CancelButtonColor = Color.red
            };
        }

        settings.ShowConfirmDialog = true;
   
    }
    /// <summary>
    /// 確認ダイアログの準備
    /// </summary>
    private void SetupConfirmDialog()
    {
        if (settings?.ConfirmDialogPrefab != null)
        {
            Transform parentTransform = targetCanvas.transform;

            confirmDialog = Instantiate(settings.ConfirmDialogPrefab, parentTransform);
            confirmDialog.SetActive(false);

            // 不要なCanvasRendererを削除
            RemoveUnnecessaryCanvasRenderers();

            ApplyDialogSettings();
        }
    }

    /// <summary>
    /// 不要なCanvasRendererコンポーネントを削除
    /// </summary>
    private void RemoveUnnecessaryCanvasRenderers()
    {
        if (confirmDialog == null) return;

        var textComponents = confirmDialog.GetComponentsInChildren<UnityEngine.UI.Text>();
        foreach (var textComponent in textComponents)
        {
            var canvasRenderers = textComponent.GetComponents<CanvasRenderer>();
            // Text コンポーネントには1つの CanvasRenderer があれば十分
            for (int i = 1; i < canvasRenderers.Length; i++)
            {
                DestroyImmediate(canvasRenderers[i]);
            }
        }
    }

  

    /// <summary>
    /// ダイアログの設定を適用
    /// </summary>
    private void ApplyDialogSettings()
    {
        if (confirmDialog == null || settings?.DialogSettings == null) return;

        var dialogSettings = settings.DialogSettings;

        // メッセージテキストを設定
        SetDialogMessage(dialogSettings.MessageText);

        // ボタンテキストを設定
        SetButtonText(dialogSettings.ConfirmButtonName, dialogSettings.ConfirmButtonText);
        SetButtonText(dialogSettings.CancelButtonName, dialogSettings.CancelButtonText);

        // カスタムカラーを適用
        if (dialogSettings.UseCustomColors)
        {
            ApplyCustomColors(dialogSettings);
        }
        // 各ボタンにリスナーを登録
        SetButtonListener(dialogSettings.ConfirmButtonName, OnConfirmExit);
        SetButtonListener(dialogSettings.CancelButtonName, OnCancelExit);
    }
    private void SetButtonListener(string buttonName, UnityEngine.Events.UnityAction action)
    {
        var buttonTransform = confirmDialog.transform.GetChild(0).Find(buttonName);
        if (buttonTransform != null)
        {
            var button = buttonTransform.GetComponent<Button>();
            if (button != null)
            {
                button.onClick.RemoveAllListeners(); // 古いリスナーをクリア
                button.onClick.AddListener(action);  // 新しいリスナーを追加
            }
            else
            {
                Debug.LogWarning($"'{buttonName}' に Button コンポーネントが見つかりません");
            }
        }
        else
        {
            Debug.LogWarning($"'{buttonName}' がダイアログ内に見つかりません");
        }
    }
    /// <summary>
    /// ボタンのテキストを設定
    /// </summary>
    private void SetButtonText(string buttonName, string text)
    {
        var buttonTransform = confirmDialog.transform.GetChild(0).Find(buttonName);
        if (buttonTransform != null)
        {
            var buttonComponent = buttonTransform.GetComponent<Button>();
            if (buttonComponent != null)
            {
                // ボタン内のテキストコンポーネントを探す
                var textComponent = buttonComponent.GetComponentInChildren<Text>();
                if (textComponent != null)
                {
                    textComponent.text = text;
                    return;
                }

                var tmpComponent = buttonComponent.GetComponentInChildren<TextMeshProUGUI>();
                if (tmpComponent != null)
                {
                    tmpComponent.text = text;
                    return;
                }
            }
        }
    }

    /// <summary>
    /// カスタムカラーを適用
    /// </summary>
    private void ApplyCustomColors(DialogElementSettings dialogSettings)
    {
        // メッセージテキストの色を設定
        ApplyTextColor(dialogSettings.MessageObjectName, dialogSettings.MessageTextColor);
        
        // ボタンの色を設定
        ApplyButtonColor(dialogSettings.ConfirmButtonName, dialogSettings.ConfirmButtonColor);
        ApplyButtonColor(dialogSettings.CancelButtonName, dialogSettings.CancelButtonColor);
    }

    /// <summary>
    /// テキストの色を設定
    /// </summary>
    private void ApplyTextColor(string objectName, Color color)
    {
        var textTransform = confirmDialog.transform.Find(objectName);
        if (textTransform != null)
        {
            var uiText = textTransform.GetComponent<Text>();
            if (uiText != null)
            {
                uiText.color = color;
                return;
            }

            var tmpText = textTransform.GetComponent<TextMeshProUGUI>();
            if (tmpText != null)
            {
                tmpText.color = color;
            }
        }
    }

    /// <summary>
    /// ボタンの色を設定
    /// </summary>
    private void ApplyButtonColor(string buttonName, Color color)
    {
        var buttonTransform = confirmDialog.transform.GetChild(0).Find(buttonName);
        if (buttonTransform != null)
        {
            var button = buttonTransform.GetComponent<Button>();
            if (button != null)
            {
                // ボタン内のテキストコンポーネントを探す
                var textComponent = button.GetComponentInChildren<Text>();
                if (textComponent != null)
                {
                    textComponent.color = color;
                    return;
                }

                var tmpComponent = button.GetComponentInChildren<TextMeshProUGUI>();
                if (tmpComponent != null)
                {
                    tmpComponent.color = color;
                    return;
                }
            }
        }
    }

    private void OnDestroy()
    {

        if (confirmDialog != null)
        {
            Destroy(confirmDialog);
        }
    }

    /// <summary>
    /// ダイアログのメッセージテキストを設定
    /// </summary>
    private void SetDialogMessage(string message)
    {
        if (confirmDialog == null)
        {
            Debug.LogError("confirmDialog が null です");
            return;
        }

        Debug.Log($"=== MessageText 検索開始 ===");
        Debug.Log($"confirmDialog 名前: {confirmDialog.name}");

        // 設定からオブジェクト名を取得
        string targetName = settings?.DialogSettings?.MessageObjectName ?? "MessageText";
        Debug.Log($"検索対象名: {targetName}");

        var directChild = confirmDialog.transform.Find(targetName);
        if (directChild != null)
        {
            Debug.Log($"直接の子オブジェクトで発見: {directChild.name}");
            if (TrySetTextComponent(directChild, message))
            {
                return;
            }
        }
        else
        {
            Debug.Log($"直接の子オブジェクトには '{targetName}' が見つかりません");
        }

      

      
        Debug.LogError("テキストコンポーネントが全く見つかりませんでした");
    }

    /// <summary>
    /// テキストコンポーネントに値を設定を試行
    /// </summary>
    private bool TrySetTextComponent(Transform target, string message)
    {
        if (target == null) return false;

        Debug.Log($"テキストコンポーネント設定試行: {target.name}");

        // Unity UI Text
        var uiText = target.GetComponent<Text>();
        if (uiText != null)
        {
            uiText.text = message;
            Debug.Log($"UI Text に設定完了: {message}");
            return true;
        }

        // TextMeshPro UGUI
        var tmpText = target.GetComponent<TextMeshProUGUI>();
        if (tmpText != null)
        {
            tmpText.text = message;
            Debug.Log($"TextMeshPro に設定完了: {message}");
            return true;
        }

        // TextMeshPro (3D)
        var tmp3DText = target.GetComponent<TextMeshPro>();
        if (tmp3DText != null)
        {
            tmp3DText.text = message;
            Debug.Log($"TextMeshPro 3D に設定完了: {message}");
            return true;
        }

        Debug.LogWarning($"'{target.name}' にテキストコンポーネントが見つかりません");


        return false;
    }

 
    /// <summary>
    /// 確認ダイアログの「はい」ボタン用（直接呼び出し版）
    /// </summary>
    public void OnConfirmExit()
    {
        if (confirmDialog != null)
        {
            confirmDialog.SetActive(false);
            Time.timeScale = 1f;
        }
        QuitApplication();
    }

    /// <summary>
    /// 確認ダイアログの「いいえ」ボタン用
    /// </summary>
    public void OnCancelExit()
    {
        if (confirmDialog != null)
        {
            confirmDialog.SetActive(false);
            Time.timeScale = 1f;
        }
    }


    /// <summary>
    /// プラットフォーム別のアプリケーション終了処理
    /// </summary>
    private void QuitApplication()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        Debug.Log("エディタでの実行を停止しました");
#elif UNITY_WEBGL
        Debug.Log("WebGLプラットフォームではゲーム終了は行われません");
#elif UNITY_ANDROID || UNITY_IOS
        Application.Quit();
        Debug.Log("モバイルアプリケーションを終了しました");
#else
        Application.Quit();
        Debug.Log("アプリケーションを終了しました");
#endif
    }

}