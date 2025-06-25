using UnityEngine;
using TMPro;
using UnityEngine.UI;

/// <summary>
/// レイアウト＆Canvas設定クラス
/// </summary>
[System.Serializable]
public class TextLayoutSettings
{
    #region テキストのレイアウト設定の内部管理用変数

    /// <summary>
    /// テキストの整列位置（左揃え、中央揃え、右揃えなど）
    /// </summary>
    [Header("テキストのレイアウト設定")]
    [SerializeField, Tooltip("テキストの整列位置（左揃え、中央揃え、右揃えなど）")]
    private TextAlignmentOptions alignment = TextAlignmentOptions.Center;

    [Space(15)]

    /// <summary>
    /// テキストの改行設定（アニメーション表示に使用）
    /// </summary>
    [SerializeField, Tooltip("テキストの改行設定（アニメーション表示に使用）")]
    private TextWrappingModes animationTextWrappingModes;

    [Space(15)]

    #endregion


    #region テキストのRectTransform設定の内部管理用変数

    /// <summary>
    /// RectTransformのアンカー最小値（左下などの位置基準）
    /// </summary>
    [Header("【RectTransform設定】")]
    [SerializeField, Tooltip("RectTransformのアンカー最小値（左下などの位置基準）")]
    private Vector2 anchorMin = new Vector2(0.5f, 0.5f);

    [Space(15)]

    /// <summary>
    /// RectTransformのアンカー最大値（右上などの位置基準）
    /// </summary>
    [SerializeField, Tooltip("RectTransformのアンカー最大値（右上などの位置基準）")]
    private Vector2 anchorMax = new Vector2(0.5f, 0.5f);

    #endregion


    #region Canvas設定の内部管理用変数

    /// <summary>
    /// Canvas内での描画優先度（数値が高いほど手前に表示）
    /// </summary>
    [Header("【Canvas設定】")]
    [SerializeField, Tooltip("Canvas内での描画優先度（数値が高いほど手前に表示）")]
    private int sortingOrder = 1000;

    [Space(15)]

    /// <summary>
    /// Canvas Scaler で使用する基準解像度（デザイン基準となるサイズ）
    /// </summary>
    [SerializeField, Tooltip("Canvas Scaler で使用する基準解像度（デザイン基準となるサイズ）")]
    private Vector2 referenceResolution = new Vector2(1920, 1080);

    [Space(15)]

    /// <summary>
    /// Canvas Scalerのスケーリングモード（画面サイズに合わせて拡大縮小）
    /// </summary>
    [SerializeField, Tooltip("Canvas Scalerのスケーリングモード（画面サイズに合わせて拡大縮小）")]
    private CanvasScaler.ScaleMode scaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;

    [Space(15)]

    /// <summary>
    /// 画面の幅・高さのどちらにスケーリングを合わせるか
    /// </summary>
    [SerializeField, Tooltip("画面の幅・高さのどちらにスケーリングを合わせるか")]
    private CanvasScaler.ScreenMatchMode screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;

    [Space(15)]

    /// <summary>
    /// 幅（0）と高さ（1）のどちらにUIを適応させるか。0.5で中間
    /// </summary>
    [SerializeField, Tooltip("幅（0）と高さ（1）のどちらにUIを適応させるか。0.5で中間")]
    [Range(0, 1)]
    private float matchWidthOrHeight = 1f;

    #endregion


    #region 読み取り専用フィールド(テキストのレイアウト設定の内部管理用変数)


    /// <summary>
    /// テキストの整列位置（左揃え、中央揃え、右揃えなど）の読み取り専用
    /// </summary>
    internal TextAlignmentOptions Alignment => alignment;

    /// <summary>
    /// テキストの改行設定（アニメーション表示に使用）の読み取り専用
    /// </summary>
    internal TextWrappingModes AnimationTextWrappingModes => animationTextWrappingModes;

    #endregion


    #region 読み取り専用フィールド(テキストのRectTransform設定の内部管理用変数)

    /// <summary>
    /// RectTransformのアンカー最小値（左下などの位置基準）の読み取り専用
    /// </summary>
    internal Vector2 AnchorMin => anchorMin;

    /// <summary>
    ///  RectTransformのアンカー最大値（右上などの位置基準）の読み取り専用
    /// </summary>
    internal Vector2 AnchorMax => anchorMax;

    #endregion


    #region  読み取り専用フィールド(Canvas設定の内部管理用変数)

    /// <summary>
    ///  Canvas内での描画優先度（数値が高いほど手前に表示）の読み取り専用
    /// </summary>
    internal int SortingOrder => sortingOrder;

    /// <summary>
    /// Canvas Scaler で使用する基準解像度（デザイン基準となるサイズ）の読み取り専用
    /// </summary>
    internal Vector2 ReferenceResolution => referenceResolution;

    /// <summary>
    /// Canvas Scalerのスケーリングモード（画面サイズに合わせて拡大縮小の読み取り専用
    /// </summary>
    internal CanvasScaler.ScaleMode ScaleMode => scaleMode;

    /// <summary>
    /// 画面の幅・高さのどちらにスケーリングを合わせるかの読み取り専用
    /// </summary>
    internal CanvasScaler.ScreenMatchMode ScreenMatchMode => screenMatchMode;

    /// <summary>
    /// 幅（0）と高さ（1）のどちらにUIを適応させるか。0.5で中間の読み取り専用
    /// </summary>
    internal float MatchWidthOrHeight => matchWidthOrHeight;

    #endregion

}
