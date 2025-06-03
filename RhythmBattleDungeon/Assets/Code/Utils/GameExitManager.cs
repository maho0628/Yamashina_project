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
    [Header("入力設定")]
    private PlayerInputActions inputActions;
    private InputAction exitAction;
    private InputAction menuAction;

    [Header("UI設定（オプション）")]
    [SerializeField, Tooltip("確認ダイアログを生成する親キャンバス。未設定の場合は自動検出")]
    private Canvas targetCanvas;

    // 設定関連（スクリプタブルオブジェクトから取得）
    private GameExitSettings settings;

    // UI参照（動的に生成）
    private GameObject confirmDialog;

    // キャンセレーショントークン
    private CancellationTokenSource cancellationTokenSource;

    /// <summary>
    /// 保存場所の選択肢
    /// </summary>
    public enum SaveLocation
    {
        PersistentDataPath,    // Application.persistentDataPath
        DataPath,             // Application.dataPath
        StreamingAssetsPath,  // Application.streamingAssetsPath
        TemporaryCachePath,   // Application.temporaryCachePath
        Custom                // カスタムパス
    }

    protected override void Awake()
    {
        base.Awake();
        cancellationTokenSource = new CancellationTokenSource();

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

        // 入力アクションを初期化
        InitializeInputActions();

        // 確認ダイアログを準備
        SetupConfirmDialog();

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

            if (settings == null)
            {
                Debug.LogWarning("GameExitSettingsが見つかりません。デフォルト設定を使用します。");
                // デフォルト設定として、設定項目を直接使用
            }
        }
    }

    /// <summary>
    /// 確認ダイアログの準備
    /// </summary>
    private void SetupConfirmDialog()
    {
        if (settings?.ConfirmDialogPrefab != null)
        {
            // 適切な親オブジェクト（キャンバス）を見つける
            Transform parentTransform = FindAppropriateParent();
            
            confirmDialog = Instantiate(settings.ConfirmDialogPrefab, parentTransform);
            confirmDialog.SetActive(false);
            
            // ダイアログの初期設定を適用
            ApplyDialogSettings();
        }
    }

    /// <summary>
    /// 確認ダイアログの適切な親オブジェクトを探す
    /// </summary>
    private Transform FindAppropriateParent()
    {
        // 1. インスペクターで指定されたキャンバスがあるかチェック
        if (targetCanvas != null && targetCanvas.gameObject.activeInHierarchy)
        {
            Debug.Log($"指定されたキャンバス '{targetCanvas.name}' を使用します");
            return targetCanvas.transform;
        }

        // 2. 現在のシーンでアクティブなキャンバスを探す
        Object[] canvases = FindObjectsByType(typeof(Canvas), FindObjectsSortMode.None);

        
        // 3. Screen Space - Overlay のキャンバスを優先
        Canvas overlayCanvas = null;
        Canvas cameraCanvas = null;
        Canvas worldCanvas = null;
        
        foreach (Canvas canvas in canvases)
        {
            if (!canvas.gameObject.activeInHierarchy) continue;
            
            switch (canvas.renderMode)
            {
                case RenderMode.ScreenSpaceOverlay:
                    if (overlayCanvas == null || canvas.sortingOrder > overlayCanvas.sortingOrder)
                        overlayCanvas = canvas;
                    break;
                case RenderMode.ScreenSpaceCamera:
                    if (cameraCanvas == null || canvas.sortingOrder > cameraCanvas.sortingOrder)
                        cameraCanvas = canvas;
                    break;
                case RenderMode.WorldSpace:
                    if (worldCanvas == null || canvas.sortingOrder > worldCanvas.sortingOrder)
                        worldCanvas = canvas;
                    break;
            }
        }
        
        // 4. 優先順位に従って適切なキャンバスを選択
        Canvas selectedCanvas = overlayCanvas ?? cameraCanvas ?? worldCanvas;
        
        if (selectedCanvas != null)
        {
            Debug.Log($"確認ダイアログを '{selectedCanvas.name}' キャンバスに生成します");
            return selectedCanvas.transform;
        }
        
        // 5. キャンバスが見つからない場合は、UIキャンバスを動的に作成
        Debug.LogWarning("適切なキャンバスが見つかりません。新しいキャンバスを作成します");
        return CreateUICanvas().transform;
    }

    /// <summary>
    /// UI用のキャンバスを動的に作成
    /// </summary>
    private Canvas CreateUICanvas()
    {
        GameObject canvasGO = new GameObject("GameExitUI_Canvas");
        Canvas canvas = canvasGO.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.sortingOrder = 1000; // 最前面に表示
        
        // Canvas Scaler を追加
        CanvasScaler scaler = canvasGO.AddComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1920, 1080);
        scaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
        scaler.matchWidthOrHeight = 0.5f;
        
        // Graphic Raycaster を追加
        canvasGO.AddComponent<GraphicRaycaster>();
        
        // シーン変更時に削除されないようにする（必要に応じて）
        DontDestroyOnLoad(canvasGO);
        
        Debug.Log("GameExitManager用のUIキャンバスを作成しました");
        return canvas;
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
    }

    /// <summary>
    /// ボタンのテキストを設定
    /// </summary>
    private void SetButtonText(string buttonName, string text)
    {
        var buttonTransform = confirmDialog.transform.Find(buttonName);
        if (buttonTransform != null)
        {
            var buttonComponent = buttonTransform.GetComponent<UnityEngine.UI.Button>();
            if (buttonComponent != null)
            {
                // ボタン内のテキストコンポーネントを探す
                var textComponent = buttonComponent.GetComponentInChildren<UnityEngine.UI.Text>();
                if (textComponent != null)
                {
                    textComponent.text = text;
                    return;
                }

                var tmpComponent = buttonComponent.GetComponentInChildren<TMPro.TextMeshProUGUI>();
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
            var uiText = textTransform.GetComponent<UnityEngine.UI.Text>();
            if (uiText != null)
            {
                uiText.color = color;
                return;
            }

            var tmpText = textTransform.GetComponent<TMPro.TextMeshProUGUI>();
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
        var buttonTransform = confirmDialog.transform.Find(buttonName);
        if (buttonTransform != null)
        {
            var button = buttonTransform.GetComponent<UnityEngine.UI.Button>();
            if (button != null)
            {
                var colors = button.colors;
                colors.normalColor = color;
                button.colors = colors;
            }
        }
    }

    private void OnEnable()
    {
        EnableInputActions();
    }

    private void OnDisable()
    {
        DisableInputActions();
    }

    /// <summary>
    /// Input Actionの初期化
    /// </summary>
    private void InitializeInputActions()
    {
        inputActions = new PlayerInputActions();

        exitAction = inputActions.UI.Exit;
        menuAction = inputActions.UI.Menu;

        if (exitAction == null)
        {
            exitAction = new InputAction("Exit", InputActionType.Button);
            exitAction.AddBinding("<Keyboard>/escape");
            exitAction.AddBinding("<Gamepad>/start");
#if UNITY_ANDROID || UNITY_IOS
            exitAction.AddBinding("<AndroidGameController>/buttonSouth");
#endif
        }

        if (menuAction == null)
        {
            menuAction = new InputAction("Menu", InputActionType.Button);
            menuAction.AddBinding("<Keyboard>/m");
            menuAction.AddBinding("<Gamepad>/select");
        }

        exitAction.performed += OnExitInput;
        menuAction.performed += OnMenuInput;
    }

    /// <summary>
    /// Input Actionを有効化
    /// </summary>
    private void EnableInputActions()
    {
        inputActions?.Enable();
        exitAction?.Enable();
        menuAction?.Enable();
    }

    /// <summary>
    /// Input Actionを無効化
    /// </summary>
    private void DisableInputActions()
    {
        inputActions?.Disable();
        exitAction?.Disable();
        menuAction?.Disable();
    }

    /// <summary>
    /// 終了入力が行われた時の処理
    /// </summary>
    private void OnExitInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            RequestExitAsync().Forget();
        }
    }

    /// <summary>
    /// メニュー入力が行われた時の処理
    /// </summary>
    private void OnMenuInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            ReturnToMenuAsync().Forget();
        }
    }

    private void OnDestroy()
    {
        if (exitAction != null)
        {
            exitAction.performed -= OnExitInput;
        }

        if (menuAction != null)
        {
            menuAction.performed -= OnMenuInput;
        }

        DisableInputActions();
        inputActions?.Dispose();

        cancellationTokenSource?.Cancel();
        cancellationTokenSource?.Dispose();

        if (confirmDialog != null)
        {
            Destroy(confirmDialog);
        }
    }

    /// <summary>
    /// ゲーム終了を要求する（外部から呼び出し可能）
    /// </summary>
    public async UniTaskVoid RequestExitAsync()
    {
        try
        {
            bool showDialog = settings?.ShowConfirmDialog ?? true;

            if (showDialog)
            {
                bool confirmed = await ShowConfirmDialogAsync();
                if (!confirmed) return;
            }

            await ExitGameAsync();
        }
        catch (System.OperationCanceledException)
        {
            Debug.Log("ゲーム終了処理がキャンセルされました");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"ゲーム終了処理でエラーが発生: {e.Message}");
        }
    }

    /// <summary>
    /// 同期版のゲーム終了要求（UIボタン用）
    /// </summary>
    public void RequestExit()
    {
        RequestExitAsync().Forget();
    }

    /// <summary>
    /// 確認ダイアログを表示して結果を待機（改良版）
    /// </summary>
    private async UniTask<bool> ShowConfirmDialogAsync()
    {
        if (confirmDialog == null)
        {
            return true;
        }

        confirmDialog.SetActive(true);
        Time.timeScale = 0f;

        // 設定からメッセージテキストを取得
        string messageText = settings?.DialogSettings?.MessageText ?? "ゲームを終了しますか？";
        SetDialogMessage(messageText);

        bool? result = null;

        // 設定からボタン名を取得
        string confirmButtonName = settings?.DialogSettings?.ConfirmButtonName ?? "ConfirmButton";
        string cancelButtonName = settings?.DialogSettings?.CancelButtonName ?? "CancelButton";

        var confirmButton = confirmDialog.transform.Find(confirmButtonName)?.GetComponent<UnityEngine.UI.Button>();
        var cancelButton = confirmDialog.transform.Find(cancelButtonName)?.GetComponent<UnityEngine.UI.Button>();

        if (confirmButton != null)
        {
            confirmButton.onClick.AddListener(() => result = true);
        }

        if (cancelButton != null)
        {
            cancelButton.onClick.AddListener(() => result = false);
        }

        await UniTask.WaitUntil(() => result.HasValue, cancellationToken: cancellationTokenSource.Token);

        confirmButton?.onClick.RemoveAllListeners();
        cancelButton?.onClick.RemoveAllListeners();

        confirmDialog.SetActive(false);
        Time.timeScale = 1f;

        return result.Value;
    }

    /// <summary>
    /// ダイアログのメッセージテキストを設定
    /// </summary>
    private void SetDialogMessage(string message)
    {
        // 設定からオブジェクト名を取得、なければデフォルトの名前パターンを使用
        string[] possibleNames;
        
        if (settings?.DialogSettings != null)
        {
            possibleNames = new string[] { settings.DialogSettings.MessageObjectName };
        }
        else
        {
            // フォールバック用の一般的な名前のパターン
            possibleNames = new string[] { "MessageText", "Message", "Text", "DialogText", "ConfirmText" };
        }

        foreach (string name in possibleNames)
        {
            var textComponent = confirmDialog.transform.Find(name);
            if (textComponent != null)
            {
                // Unity UI Text
                var uiText = textComponent.GetComponent<UnityEngine.UI.Text>();
                if (uiText != null)
                {
                    uiText.text = message;
                    return;
                }

                // TextMeshPro UGUI
                var tmpText = textComponent.GetComponent<TMPro.TextMeshProUGUI>();
                if (tmpText != null)
                {
                    tmpText.text = message;
                    return;
                }

                // TextMeshPro (3D)
                var tmp3DText = textComponent.GetComponent<TMPro.TextMeshPro>();
                if (tmp3DText != null)
                {
                    tmp3DText.text = message;
                    return;
                }
            }
        }

        // 見つからない場合は警告を出す
        Debug.LogWarning($"確認ダイアログ内にテキストコンポーネントが見つかりませんでした。探索したオブジェクト名: {string.Join(", ", possibleNames)}");
    }

    /// <summary>
    /// 確認ダイアログの「はい」ボタン用（直接呼び出し版）
    /// </summary>
    public async UniTaskVoid OnConfirmExitAsync()
    {
        if (confirmDialog != null)
        {
            confirmDialog.SetActive(false);
            Time.timeScale = 1f;
        }
        await ExitGameAsync();
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
    /// メインメニューに戻る
    /// </summary>
    public async UniTaskVoid ReturnToMenuAsync()
    {
        try
        {
            if (SceneTransitionManager.Instance == null)
            {
                Debug.LogError("SceneTransitionManagerのインスタンスが見つかりません");
                await ExitGameAsync();
                return;
            }

            var menuScene = settings?.MenuSceneReference;
            if (menuScene == null)
            {
                Debug.LogWarning("メニューシーンが設定されていません");
                await ExitGameAsync();
                return;
            }

            await OnBeforeSceneChangeAsync();
            SceneTransitionManager.Instance.TransitionTo(menuScene);
        }
        catch (System.OperationCanceledException)
        {
            Debug.Log("メニュー復帰処理がキャンセルされました");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"メニュー復帰処理でエラーが発生: {e.Message}");
        }
    }

    /// <summary>
    /// 同期版のメニュー復帰（UIボタン用）
    /// </summary>
    public void ReturnToMenu()
    {
        ReturnToMenuAsync().Forget();
    }

    /// <summary>
    /// ゲームを終了する
    /// </summary>
    public async UniTask ExitGameAsync()
    {
        try
        {
            await OnBeforeExitAsync();

            if (SceneTransitionManager.Instance != null)
            {
                await WaitForFadeEffect();
            }
            else
            {
                await UniTask.Delay(500, cancellationToken: cancellationTokenSource.Token);
            }

            QuitApplication();
        }
        catch (System.OperationCanceledException)
        {
            Debug.Log("ゲーム終了処理がキャンセルされました");
        }
    }

    /// <summary>
    /// 同期版のゲーム終了（UIボタン用）
    /// </summary>
    public void ExitGame()
    {
        ExitGameAsync().Forget();
    }

    /// <summary>
    /// フェード効果を待機する処理
    /// </summary>
    private async UniTask WaitForFadeEffect()
    {
        if (!SceneTransitionManager.Instance.IsTransitioning)
        {
            float fadeSpeed = GameInitializer.Instance?.GetGameSettings()?.FadeSpeed ?? 1.0f;
            float fadeDuration = 1.0f / fadeSpeed;

            await UniTask.Delay((int)(fadeDuration * 1000), cancellationToken: cancellationTokenSource.Token);
        }
    }

    /// <summary>
    /// シーン変更前の処理
    /// </summary>
    protected virtual async UniTask OnBeforeSceneChangeAsync()
    {
        await SaveCurrentStateAsync();
        Debug.Log("シーン変更前処理を実行中...");
        await UniTask.Delay(100, cancellationToken: cancellationTokenSource.Token);
    }

    /// <summary>
    /// 終了前の処理（オーバーライド可能）
    /// </summary>
    protected virtual async UniTask OnBeforeExitAsync()
    {
        await SaveGameDataAsync();
        AudioListener.pause = true;
        Debug.Log("ゲーム終了処理を実行中...");
        await UniTask.Delay(200, cancellationToken: cancellationTokenSource.Token);
    }

    /// <summary>
    /// 現在の状態を保存（シーン変更用）
    /// </summary>
    private async UniTask SaveCurrentStateAsync()
    {
        await UniTask.RunOnThreadPool(() =>
        {
            PlayerPrefs.SetString("LastSceneChangeTime", System.DateTime.Now.ToString());
        }, cancellationToken: cancellationTokenSource.Token);

        Debug.Log("現在の状態を保存しました");
    }

    /// <summary>
    /// ゲームデータの保存（非同期版）
    /// </summary>
    private async UniTask SaveGameDataAsync()
    {
        try
        {
            PlayerPrefs.SetString("LastExitTime", System.DateTime.Now.ToString());
            PlayerPrefs.SetInt("SaveVersion", 1);
            PlayerPrefs.Save();

            if (HasHeavyDataToSave())
            {
                await UniTask.RunOnThreadPool(() =>
                {
                    SaveHeavyDataToFile();
                }, cancellationToken: cancellationTokenSource.Token);
            }

            await UniTask.SwitchToMainThread(cancellationToken: cancellationTokenSource.Token);
            Debug.Log("ゲームデータを保存しました");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"保存処理でエラーが発生: {e.Message}");
            throw;
        }
    }

    /// <summary>
    /// 重いデータがあるかチェック
    /// </summary>
    private bool HasHeavyDataToSave()
    {
        return false;
    }

    /// <summary>
    /// 保存パスを取得する
    /// </summary>
    private string GetSavePath()
    {
        string basePath;
        string fileName = settings?.SaveFileName ?? "savedata.json";
        SaveLocation location = settings?.SaveLocation ?? SaveLocation.PersistentDataPath;
        string customPath = settings?.CustomSavePath ?? "";

        switch (location)
        {
            case SaveLocation.PersistentDataPath:
                basePath = Application.persistentDataPath;
                break;
            case SaveLocation.DataPath:
                basePath = Application.dataPath;
                break;
            case SaveLocation.StreamingAssetsPath:
                basePath = Application.streamingAssetsPath;
                break;
            case SaveLocation.TemporaryCachePath:
                basePath = Application.temporaryCachePath;
                break;
            case SaveLocation.Custom:
                basePath = !string.IsNullOrEmpty(customPath) ? customPath : Application.persistentDataPath;
                break;
            default:
                basePath = Application.persistentDataPath;
                break;
        }

        return System.IO.Path.Combine(basePath, fileName);
    }

    /// <summary>
    /// 重いファイル保存処理（設定可能なパス使用）
    /// </summary>
    private void SaveHeavyDataToFile()
    {
        try
        {
            string saveData = "{ \"lastSave\": \"" + System.DateTime.Now.ToString() + "\" }";
            string filePath = GetSavePath();

            // ディレクトリが存在しない場合は作成
            string directory = System.IO.Path.GetDirectoryName(filePath);
            if (!System.IO.Directory.Exists(directory))
            {
                System.IO.Directory.CreateDirectory(directory);
            }

            System.IO.File.WriteAllText(filePath, saveData);
            Debug.Log($"ファイルを保存しました: {filePath}");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"ファイル保存エラー: {e.Message}");
            throw;
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

    /// <summary>
    /// 強制終了（緊急時用）
    /// </summary>
    public void ForceQuit()
    {
        Debug.Log("強制終了を実行します");
        cancellationTokenSource?.Cancel();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        System.Diagnostics.Process.GetCurrentProcess().Kill();
#endif
    }

    /// <summary>
    /// タイムアウト付きでゲーム終了
    /// </summary>
    public async UniTaskVoid ExitGameWithTimeoutAsync(float? timeoutSeconds = null)
    {
        try
        {
            float timeout = timeoutSeconds ?? settings?.ExitTimeoutSeconds ?? 10f;
            var timeoutToken = new CancellationTokenSource(System.TimeSpan.FromSeconds(timeout));
            var combinedToken = CancellationTokenSource.CreateLinkedTokenSource(
                cancellationTokenSource.Token, timeoutToken.Token).Token;

            await ExitGameAsync().AttachExternalCancellation(combinedToken);
        }
        catch (System.OperationCanceledException)
        {
            Debug.LogWarning("ゲーム終了処理がタイムアウトしました。強制終了します。");
            ForceQuit();
        }
    }

    // === 実行時設定変更用メソッド（デバッグ・テスト用） ===

    /// <summary>
    /// 設定を再読み込み
    /// </summary>
    public void ReloadSettings()
    {
        LoadSettings();
        SetupConfirmDialog();
    }

    /// <summary>
    /// 現在の保存パスを取得（デバッグ用）
    /// </summary>
    public string GetCurrentSavePath()
    {
        return GetSavePath();
    }

    /// <summary>
    /// Exit用Input Actionの設定
    /// </summary>
    public void SetExitInputAction(InputAction action)
    {
        if (exitAction != null)
        {
            exitAction.performed -= OnExitInput;
            exitAction.Disable();
        }

        exitAction = action;

        if (exitAction != null)
        {
            exitAction.performed += OnExitInput;
            exitAction.Enable();
        }
    }

    /// <summary>
    /// メニュー用Input Actionの設定
    /// </summary>
    public void SetMenuInputAction(InputAction action)
    {
        if (menuAction != null)
        {
            menuAction.performed -= OnMenuInput;
            menuAction.Disable();
        }

        menuAction = action;

        if (menuAction != null)
        {
            menuAction.performed += OnMenuInput;
            menuAction.Enable();
        }
    }

    /// <summary>
    /// 入力を一時的に無効化
    /// </summary>
    public void DisableInput()
    {
        DisableInputActions();
    }

    /// <summary>
    /// 入力を有効化
    /// </summary>
    public void EnableInput()
    {
        EnableInputActions();
    }

    /// <summary>
    /// 確認ダイアログの親キャンバスを設定
    /// </summary>
    public void SetTargetCanvas(Canvas canvas)
    {
        targetCanvas = canvas;
        
        // 既にダイアログが作成されている場合は再作成
        if (confirmDialog != null)
        {
            Destroy(confirmDialog);
            SetupConfirmDialog();
        }
    }

    /// <summary>
    /// 現在使用中のキャンバスを取得（デバッグ用）
    /// </summary>
    public Canvas GetCurrentCanvas()
    {
        if (confirmDialog != null && confirmDialog.transform.parent != null)
        {
            return confirmDialog.transform.parent.GetComponent<Canvas>();
        }
        return targetCanvas;
    }
}