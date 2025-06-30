#if UNITY_EDITOR
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;

/// <summary>
/// テキストのアニメーションをプレビューするためのクラス
/// </summary>
public static class TextAnimationPreviewer
{
    /// <summary>
    /// アニメーションの再生を行うためのCanvas
    /// </summary>
    private static GameObject previewCanvasObj;

    /// <summary>
    /// アニメーションの再生を行うためのテキストオブジェクト
    /// </summary>
    private static GameObject previewTextObj;

    /// <summary>
    /// プレビュー用のTextMeshProUGUIコンポーネント
    /// </summary>
    private static TextMeshProUGUI previewText;

    /// <summary>
    /// テキストのアニメーションプレビューを行う
    /// </summary>
    /// <param name="config">テキストアニメーションの設定</param>
    public static void Preview(TextAnimationConfig config)
    {
        // 古いプレビューを削除して、表示が重ならないようにする
        CleanupPreviousPreview();

        // 指定された設定で新しいテキストアニメーションを表示する
        CreatePreview(config);
    }

    /// <summary>
    /// 手動でプレビューを削除するメソッド
    /// </summary>
    public static void ClearPreview()
    {
        CleanupPreviousPreview();
        DebugManager.Log("テキストプレビューが手動でクリアされました。");
    }

    /// <summary>
    /// 既に存在するプレビュー用のキャンバスとテキストオブジェクトを削除し、
    /// 新しいプレビュー作成時に古いオブジェクトが残らないようにします。
    /// </summary>
    private static void CleanupPreviousPreview()
    {
        // 共通クラスでアニメーション停止
        TextAnimationPlayer.StopAnimation(previewText);

        // すでに表示中のプレビュー画面があれば削除する（エディタ上ですぐに消える）
        if (previewCanvasObj != null)
        {
            Object.DestroyImmediate(previewCanvasObj);
        }

        // 念のため、画面上に残っているプレビュー用キャンバスを探して削除する
        var existingCanvas = GameObject.Find("PreviewCanvas");
        if (existingCanvas != null)
        {
            Object.DestroyImmediate(existingCanvas);
        }

        // 変数も初期状態に戻して、次のプレビューに備える
        previewCanvasObj = null;
        previewTextObj = null;
        previewText = null;
    }

    /// <summary>
    /// 指定された設定に基づいてプレビュー用のキャンバスとテキストを作成し、
    /// アニメーションを再生します。Unityエディタ上での確認用。
    /// </summary>
    /// <param name="config">テキストアニメーションの設定情報</param>
    private static void CreatePreview(TextAnimationConfig config)
    {
        // 設定の検証
        if (!ValidateConfig(config)) return;

        // UI要素を作成
        CreatePreviewUI(config);

        // アニメーションを再生（共通クラスを使用）
        _ = PlayPreviewAnimationAsync(config);
    }

    /// <summary>
    /// 設定の有効性を検証
    /// </summary>
    private static bool ValidateConfig(TextAnimationConfig config)
    {
        if (config == null)
        {
            DebugManager.LogError("TextAnimationConfig が null");
            return false;
        }

        if (config.Params.Basic == null || config.Params.Layout == null || config.Params.Timing == null)
        {
            DebugManager.LogError("TextAnimationConfig parameters が null");
            return false;
        }

        return true;
    }

    /// <summary>
    /// プレビュー用のUI要素を作成
    /// </summary>
    private static void CreatePreviewUI(TextAnimationConfig config)
    {
        var layoutSettings = config.Params.Layout;

        // Canvas作成
        previewCanvasObj = new GameObject("PreviewCanvas");
        var canvas = previewCanvasObj.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.sortingOrder = layoutSettings.SortingOrder;

        // CanvasScaler設定
        SetupCanvasScaler(layoutSettings);

        // GraphicRaycaster追加
        previewCanvasObj.AddComponent<GraphicRaycaster>();

        // テキストUI作成
        CreateTextUI(config);

        // Undo登録
        Undo.RegisterCreatedObjectUndo(previewCanvasObj, "Create Preview Canvas");
    }

    /// <summary>
    /// CanvasScalerの設定
    /// </summary>
    private static void SetupCanvasScaler(TextLayoutSettings layoutSettings)
    {
        var scaler = previewCanvasObj.AddComponent<CanvasScaler>();
        scaler.uiScaleMode = layoutSettings.ScaleMode;
        scaler.referenceResolution = layoutSettings.ReferenceResolution;

        if (scaler.uiScaleMode == CanvasScaler.ScaleMode.ScaleWithScreenSize)
        {
            scaler.screenMatchMode = layoutSettings.ScreenMatchMode;
            scaler.matchWidthOrHeight = layoutSettings.MatchWidthOrHeight;
        }
    }

    /// <summary>
    /// テキストUIの作成
    /// </summary>
    private static void CreateTextUI(TextAnimationConfig config)
    {
        var basicSettings = config.Params.Basic;
        var layoutSettings = config.Params.Layout;

        // メインオブジェクト作成
        previewTextObj = new GameObject("PreviewText");
        previewTextObj.transform.SetParent(previewCanvasObj.transform, false);

        // 背景とテキストのWrapper
        GameObject textWrapper = new GameObject("TextWrapper");
        textWrapper.transform.SetParent(previewTextObj.transform, false);

        // 背景作成
        CreateBackgroundImage(textWrapper, basicSettings, layoutSettings);

        // テキスト作成
        CreateTextMeshPro(textWrapper);
    }

    /// <summary>
    /// 背景画像の作成
    /// </summary>
    private static void CreateBackgroundImage(GameObject parent, TextBasicSettings basicSettings, TextLayoutSettings layoutSettings)
    {
        GameObject bgObj = new GameObject("TextBackground");
        bgObj.transform.SetParent(parent.transform, false);
        var bgImage = bgObj.AddComponent<Image>();
        bgImage.sprite = basicSettings.BackGroundImage;
        bgImage.rectTransform.sizeDelta = new Vector2(layoutSettings.TextBoxWidth, layoutSettings.TextBoxHeight);
    }

    /// <summary>
    /// TextMeshProの作成
    /// </summary>
    private static void CreateTextMeshPro(GameObject parent)
    {
        GameObject textObj = new GameObject("Text");
        textObj.transform.SetParent(parent.transform, false);
        previewText = textObj.AddComponent<TextMeshProUGUI>();
    }

    /// <summary>
    /// プレビューアニメーションを非同期で再生
    /// </summary>
    private static async System.Threading.Tasks.Task PlayPreviewAnimationAsync(TextAnimationConfig config)
    {
        if (previewText == null) return;

        try
        {
            // 共通クラスでアニメーション再生（プレビューモード）
            await TextAnimationPlayer.PlayTextAnimationAsync(previewText, config, isPreview: true);
        }
        catch (System.Exception e)
        {
            DebugManager.LogError($"プレビューアニメーション再生エラー: {e.Message}");
        }
    }
}
#endif