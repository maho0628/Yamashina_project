using UnityEngine;

[System.Serializable]
public class SceneBGMConfig
{
    [SerializeField, Header("‘ÎÛ‚ÌƒV[ƒ“–¼")]
    private string sceneName;

    [SerializeField, Header("Ä¶‚·‚éBGM‚ÌID")]
    private string bgmId;

    public string SceneName => sceneName;
    public string BgmId => bgmId;
}

