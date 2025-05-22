using UnityEngine;

/// <summary>
/// シーンごとのBGMの設定データ
/// </summary>
[System.Serializable]
public class SceneBGMConfig
{
    #region シーンBGMの設定に関する変数

    /// <summary>
    /// 対象のシーン名
    /// </summary>
    [SerializeField, Header("対象のシーン名")]
    private string sceneName;

    /// <summary>
    /// 再生するBGMのID
    /// </summary>
    [SerializeField, Header("再生するBGMのID")]
    private string bgmId;

    #endregion


    #region 読み取り専用プロパティ(シーンBGMの設定に関する変数)

    /// <summary>
    /// 対象のシーン名の読み取り専用
    /// </summary>
    internal string SceneName => sceneName;

    /// <summary>
    /// 再生するBGMのIDの読み取り専用
    /// </summary>
    internal string BgmId => bgmId;

    #endregion
}

