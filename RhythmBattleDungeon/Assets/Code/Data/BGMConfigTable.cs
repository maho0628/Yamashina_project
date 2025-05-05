using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BGMConfig", menuName = "GameData/BGMConfigTable")]
/// <summary>
/// ゲーム内で使用するBGM設定の一覧を保持する ScriptableObject
/// </summary>
public class BGMConfigTable : ScriptableObject
{
    [SerializeField, Header("ゲーム内で使用するBGM設定の一覧")]
    private List<BGMConfig> bgmList;


    /// <summary>
    /// 全てのBGM設定を取得
    /// </summary>
    internal List<BGMConfig> GetAll()
    {
        return bgmList;
    }

    /// <summary>
    /// 指定したIDのBGM設定を取得
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    internal BGMConfig GetBgmConfig(string id)
    {
        return bgmList.Find(s => s.BgmId == id);
    }

}


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


