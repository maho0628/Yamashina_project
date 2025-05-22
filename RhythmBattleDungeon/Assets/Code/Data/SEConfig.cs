using UnityEngine;

/// <summary>
/// 単一のSE（効果音）の設定データ
/// </summary>
[System.Serializable]
public class SEConfig
{
    #region SE設定に関する情報変数

    /// <summary>
    /// SEのID名
    /// </summary>
    [SerializeField, Header("SEのID名")]
    private string seId;

    /// <summary>
    /// 使用するSEオーディオクリップ
    /// </summary>
    [SerializeField, Header("使用するSEオーディオクリップ")]
    private AudioClip seAudioClip;

    /// <summary>
    /// SEの説明
    /// </summary>
    [SerializeField, Header("SEの説明")]
    private string description;  // 例：「ボタン押下音」など

    #endregion


    #region 読み取り専用プロパティ(SE設定に関する情報変数)

    /// <summary>
    /// SEのID名の読み取り専用
    /// </summary>
    internal string SeId => seId;

    /// <summary>
    /// 使用するSEオーディオクリップの読み取り専用
    /// </summary>
    internal AudioClip SeAudioClip => seAudioClip;

    /// <summary>
    /// SEの説明の読み取り専用
    /// </summary>
    internal string Description => description;

    #endregion
}
