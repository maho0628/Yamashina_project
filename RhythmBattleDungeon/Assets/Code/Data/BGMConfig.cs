using UnityEngine;

[System.Serializable]
/// <summary>
/// 単一のBGMに関する設定データ
/// </summary>

public class BGMConfig
{
    [SerializeField, Header("BGMのID")]
    private string bgmId;

    [SerializeField, Header("使用するオーディオクリップ")]
    private AudioClip bgmAudioClip;

    [SerializeField, Header("BPM（Beats Per Minute）")]
    private float bgmBpm;

    [SerializeField, Header("ジャンル名")]
    private string bgmGenre;

    [SerializeField, Header("表示用の曲名")]
    private string bgmDisplayName;

    [SerializeField, Header("ジャケット画像")]
    private Sprite bgmJacketImage;

    // 以下は各データの読み取り専用プロパティ

    internal string BgmId => bgmId;
    internal AudioClip BgmAudioClip => bgmAudioClip;
    internal float BgmBpm => bgmBpm;
    internal string BgmGenre => bgmGenre;
    internal string BgmDisplayName => bgmDisplayName;
    internal Sprite BgmJacketImage => bgmJacketImage;
}
