using UnityEngine;

[System.Serializable]
public class SceneBGMConfig
{
    [SerializeField, Header("‘ÎÛ‚ÌƒV[ƒ“–¼")]
    private string sceneName;

    [SerializeField, Header("Ä¶‚·‚éBGM‚ÌID")]
    private string bgmId;

    internal string SceneName => sceneName;
    internal string BgmId => bgmId;
}

