

#if UNITY_EDITOR
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using UnityEditor;

public static class TextAnimationPreviewer
{
    private static GameObject previewCanvasObj;
    private static GameObject previewTextObj;

    public static void Preview(TextAnimationConfig config)
    {
        CleanupPreviousPreview();
        CreatePreview(config);
    }




    /// <summary>
    /// 手動でプレビューを削除するメソッド
    /// </summary>
    public static void ClearPreview()
    {
        CleanupPreviousPreview();
        Debug.Log("Preview cleared manually");
    }

    private static void CleanupPreviousPreview()
    {
        if (previewCanvasObj != null)
        {
            Object.DestroyImmediate(previewCanvasObj);
        }

        var existingCanvas = GameObject.Find("PreviewCanvas");
        if (existingCanvas != null)
        {
            Object.DestroyImmediate(existingCanvas);
        }

        previewCanvasObj = null;
        previewTextObj = null;
    }

    private static void CreatePreview(TextAnimationConfig config)
    {
        previewCanvasObj = new GameObject("PreviewCanvas");
        var canvas = previewCanvasObj.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.sortingOrder = config.LayoutSettings.SortingOrder;

        var scaler = previewCanvasObj.AddComponent<CanvasScaler>();
        scaler.uiScaleMode = config.LayoutSettings.ScaleMode;
        scaler.referenceResolution = config.LayoutSettings.ReferenceResolution;

        if (scaler.uiScaleMode == CanvasScaler.ScaleMode.ScaleWithScreenSize)
        {
            scaler.screenMatchMode = config.LayoutSettings.ScreenMatchMode;
            scaler.matchWidthOrHeight = config.LayoutSettings.MatchWidthOrHeight;
        }

        previewCanvasObj.AddComponent<GraphicRaycaster>();

        previewTextObj = new GameObject("TextAnimationPreview");
        previewTextObj.transform.SetParent(previewCanvasObj.transform, false);

        var text = previewTextObj.AddComponent<TextMeshProUGUI>();
        text.font = config.BasicSettings.FontAsset;
        text.text = config.BasicSettings.AnimationText;
        text.color = config.BasicSettings.TextColor;
        text.fontSize = config.BasicSettings.FontSize;
        text.alignment = config.LayoutSettings.Alignment;
        text.fontStyle = config.BasicSettings.AnimationFontStyles;
        text.textWrappingMode = config.LayoutSettings.AnimationTextWrappingModes;

        var rect = text.GetComponent<RectTransform>();
        rect.anchorMin = config.LayoutSettings.AnchorMin;
        rect.anchorMax = config.LayoutSettings.AnchorMax;
        rect.anchoredPosition = Vector2.zero;

        Undo.RegisterCreatedObjectUndo(previewCanvasObj, "Create Preview Canvas");


        var sequence = DOTween.Sequence();
        sequence.Append(text.DOFade(config.TimingSettings.FadeInAlpha, config.TimingSettings.FadeInDuration));
        sequence.AppendInterval(config.TimingSettings.DisplayDuration);
        sequence.Append(text.DOFade(config.TimingSettings.FadeOutAlpha, config.TimingSettings.FadeOutDuration));

        sequence.Play();
    }


    /// <summary>
    /// プレビューが存在するかチェック
    /// </summary>
    public static bool HasActivePreview()
    {
        return previewCanvasObj != null;
    }

}
#endif
