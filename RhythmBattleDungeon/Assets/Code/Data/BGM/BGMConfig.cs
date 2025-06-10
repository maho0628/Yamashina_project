using UnityEngine;

/// <summary>
/// 単一のBGMに関する設定データ
/// </summary>
[System.Serializable]
public class BGMConfig
{
    #region  BGMの内部管理用変数

    /// <summary>
    /// BGMのID
    /// </summary>
    [SerializeField, Tooltip("BGMのID")]
    private BGMName bgmId;

    /// <summary>
    /// 使用するオーディオクリップ
    /// </summary>
    [SerializeField, Tooltip("使用するオーディオクリップ")]
    private AudioClip bgmAudioClip;

    /// <summary>
    /// BPM（Beats Per Minute)
    /// </summary>
    [SerializeField, Tooltip("BPM（Beats Per Minute）")]
    private float bgmBpm;

    /// <summary>
    /// ジャンル名
    /// </summary>
    [SerializeField, Tooltip("ジャンル名")]
    private string bgmGenre;

    /// <summary>
    /// 表示用の曲名
    /// </summary>
    [SerializeField, Tooltip("表示用の曲名")]
    private string bgmDisplayName;

    /// <summary>
    /// ジャケット画像
    /// </summary>
    [SerializeField, Tooltip("ジャケット画像")]
    private Sprite bgmJacketImage;

    #endregion


    #region  読み取り専用プロパティ (BGMの内部管理用変数)

    /// <summary>
    /// BGMのIDの読み取り専用
    /// </summary>
    internal BGMName BgmId => bgmId;

    /// <summary>
    /// 使用するオーディオクリップの読み取り専用
    /// </summary>
    internal AudioClip BgmAudioClip => bgmAudioClip;

    /// <summary>
    /// BPM（Beats Per Minute）の読み取り専用
    /// </summary>
    internal float BgmBpm => bgmBpm;

    /// <summary>
    /// ジャンル名の読み取り専用
    /// </summary>
    internal string BgmGenre => bgmGenre;

    /// <summary>
    /// 表示用の曲名の読み取り専用
    /// </summary>
    internal string BgmDisplayName => bgmDisplayName;

    /// <summary>
    /// ジャケット画像の読み取り専用
    /// </summary>
    internal Sprite BgmJacketImage => bgmJacketImage;

    #endregion
}