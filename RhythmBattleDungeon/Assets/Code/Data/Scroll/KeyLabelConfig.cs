using UnityEngine;
using TMPro;

/// <summary>
/// レーンのキーラベル設定に関するスクリプタブルオブジェクト
/// </summary>
[CreateAssetMenu(fileName = "KeyLabelConfig", menuName = "GameConfig/UI/キーラベル設定")]
public class KeyLabelConfig : ScriptableObject
{
    #region レーンのキーラベル設定に関する内部管理用変数

    /// <summary>
    /// レーンのキーラベルの文字列の配列
    /// </summary>
    [Header("▼キーラベルの表示設定")]
    [SerializeField, Tooltip("レーンのキーラベルの文字列")]
    private string[] keyLabels = { "S", "D", "F", "J", "K", "L" };

    [Space(15)]

    /// <summary>
    /// キーラベルの文字の大きさ
    /// </summary>
    [SerializeField, Tooltip("キーラベルの文字の大きさ")]
    private float fontSize = 40f;

    [Space(15)]

    /// <summary>
    /// キーラベルのTMPProのフォントアセット
    /// </summary>
    [SerializeField, Tooltip("キーラベルのTMPProのフォントアセット")]
    private TMP_FontAsset fontAsset;

    [Space(15)]

    /// <summary>
    /// キーラベルのフォントの色
    /// </summary>
    [SerializeField, Tooltip("キーラベルのフォントの色")]
    private Color fontColor = Color.white;

    [Space(15)]

    /// <summary>
    /// キーラベルのテキストのアライメント
    /// </summary>
    [SerializeField, Tooltip("キーラベルのテキストのアライメント")]
    private TextAlignmentOptions alignment = TextAlignmentOptions.Center;

    [Space(15)]

    /// <summary>
    /// レーンラベルのプレハブ (TextMeshPro付き)
    /// </summary>
    [Header("▼キーラベルのプレハブ設定")]
    [SerializeField, Tooltip("レーンラベルのプレハブ (TextMeshPro付き)")]
    private GameObject laneLabelPrefab;

    [Space(15)]

    /// <summary>
    /// ラベルのサイズ（Width, Height）
    /// </summary>
    [Header("▼ レーンラベル UI 調整")]
    [SerializeField, Tooltip("ラベルのサイズ（Width, Height）")]
    private Vector2 laneLabelSize = new Vector2(100f, 40f);

    #endregion


    #region  読み取り専用プロパティ(レーンのキーラベル設定に関する内部管理用変数)

    /// <summary>
    /// レーンのキーラベルの文字列の配列の読み取り専用
    /// </summary>
    internal string[] KeyLabels => keyLabels;

    /// <summary>
    /// キーラベルの文字の大きさの読み取り専用
    /// </summary>
    internal float FontSize => fontSize;

    /// <summary>
    /// キーラベルのTMPProのフォントアセットの読み取り専用
    /// </summary>
    internal TMP_FontAsset FontAsset => fontAsset;

    /// <summary>
    /// キーラベルのフォントの色の読み取り専用
    /// </summary>
    internal Color FontColor => fontColor;

    /// <summary>
    /// キーラベルのテキストのアライメントの読み取り専用
    /// </summary>
    internal TextAlignmentOptions Alignment => alignment;

    /// <summary>
    /// レーンラベルのプレハブ (TextMeshPro付き)の読み取り専用
    /// </summary>
    internal GameObject LaneLabelPrefab => laneLabelPrefab;

    /// <summary>
    /// ラベルのサイズ（Width, Height）の読み取り専用
    /// </summary>
    internal Vector2 LaneLabelSize => laneLabelSize;

    #endregion

}
