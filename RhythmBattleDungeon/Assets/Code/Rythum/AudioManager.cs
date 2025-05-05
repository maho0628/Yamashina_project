using UnityEngine;

/// <summary>
/// ゲーム全体の音声再生を管理するシングルトンコンポーネント。
/// BGM と SE（効果音）の再生、停止、ボリューム調整を行う。
/// </summary>
public class AudioManager : SingletonMonoBehaviour<AudioManager>
{
    #region オーディオソース (コード管理／Inspector非表示)

    // BGM 再生用 AudioSource（コードで生成）
    private AudioSource bgmSource;

    // 同時再生用 SE AudioSource 配列（コードで生成）
    private AudioSource[] seSources;

    #endregion

    #region プランナー向けパラメータ (Inspector表示)

    [SerializeField, Header("同時に再生できる効果音の数")]
    private int maxSeCount = 3;

    [SerializeField, Header("初期 BGM 音量 (0.0 - 1.0)")]
    private float initialBgmVolume = 1f;

    [SerializeField, Header("初期 SE 音量 (0.0 - 1.0)")]
    private float initialSeVolume = 1f;

    #endregion

    #region 内部管理用フィールド

    // 現在の BGM 音量 (0.0 - 1.0)
    private float bgmVolume;

    // 現在の SE 音量 (0.0 - 1.0)
    private float seVolume;

    // 登録されている BGM 設定テーブル
    private BGMConfigTable bgmConfigTable;

    // 登録されている SE 設定テーブル
    private SEConfigTable seConfigTable;

    #endregion

    /// <summary>
    /// 初期化処理：AudioSource のセットアップと初期音量を設定する。
    /// </summary>
    public override void Awake()
    {
        base.Awake();
        InitializeAudioSources();

        bgmVolume = Mathf.Clamp01(initialBgmVolume);
        seVolume = Mathf.Clamp01(initialSeVolume);
        ApplyVolumes();
    }

    /// <summary>
    /// BGM 設定テーブルを登録する。
    /// </summary>
    /// <param name="bgmTable">ScriptableObject で用意した BGM 設定テーブル</param>
    public void SetupBGMConfigTable(BGMConfigTable bgmTable)
    {
        bgmConfigTable = bgmTable;
    }

    /// <summary>
    /// SE 設定テーブルを登録する。
    /// </summary>
    /// <param name="seTable">ScriptableObject で用意した SE 設定テーブル</param>
    public void SetupSEConfigTable(SEConfigTable seTable)
    {
        seConfigTable = seTable;
    }

    /// <summary>
    /// 指定した BGM ID の曲をループ再生する。
    /// </summary>
    /// <param name="bgmId">BGMConfigTable に登録された識別子</param>
    public void PlayBGMById(string bgmId)
    {
        if (bgmConfigTable == null)
        {
            Debug.LogError("[AudioManager] BGMConfigTable が未設定です。");
            return;
        }

        var bgmConfig = bgmConfigTable.GetBgmConfig(bgmId);
        if (bgmConfig == null)
        {
            Debug.LogError($"[AudioManager] BGMConfig が見つかりません (ID: {bgmId})");
            return;
        }

        PlayClip(bgmSource, bgmConfig.BgmAudioClip, loop: true);
    }

    /// <summary>
    /// 現在再生中の BGM を停止する。
    /// </summary>
    public void StopBGM()
    {
        if (bgmSource.isPlaying)
        {
            bgmSource.Stop();
            Debug.Log("[AudioManager] BGM 停止");
        }
    }

    /// <summary>
    /// 指定した SE ID の効果音を再生する。
    /// </summary>
    /// <param name="seId">SEConfigTable に登録された識別子</param>
    public void PlaySEById(string seId)
    {
        if (seConfigTable == null)
        {
            Debug.LogError("[AudioManager] SEConfigTable が未設定です。");
            return;
        }

        var seConfig = seConfigTable.GetSeConfig(seId);
        if (seConfig == null)
        {
            Debug.LogError($"[AudioManager] SEConfig が見つかりません (ID: {seId})");
            return;
        }

        PlayClip(seSources, seConfig.SeAudioClip);
    }

    /// <summary>
    /// 現在の BGM 再生位置（秒）を返す。
    /// </summary>
    public float GetCurrentBGMTime()
    {
        return (bgmSource != null && bgmSource.clip != null)
            ? bgmSource.time
            : 0f;
    }

    /// <summary>
    /// BGM 音量を設定する (0.0 - 1.0)。
    /// </summary>
    /// <param name="volume">音量</param>
    public void SetBGMVolume(float volume)
    {
        bgmVolume = Mathf.Clamp01(volume);
        if (bgmSource != null)
            bgmSource.volume = bgmVolume;
    }

    /// <summary>
    /// SE 音量を設定する (0.0 - 1.0)。
    /// </summary>
    /// <param name="volume">音量</param>
    public void SetSFXVolume(float volume)
    {
        seVolume = Mathf.Clamp01(volume);
        ApplyVolumes();
    }

    /// <summary>
    /// 現在設定されている BGM 音量を返す。
    /// </summary>
    public float GetBGMVolume() => bgmVolume;

    /// <summary>
    /// 現在設定されている SE 音量を返す。
    /// </summary>
    public float GetSEVolume() => seVolume;

    #region プライベートメソッド

    /// <summary>
    /// AudioSource を初期化し、BGM と SE 用を生成する。
    /// </summary>
    private void InitializeAudioSources()
    {
        bgmSource = gameObject.AddComponent<AudioSource>();
        seSources = new AudioSource[maxSeCount];
        for (int i = 0; i < maxSeCount; i++)
            seSources[i] = gameObject.AddComponent<AudioSource>();
    }

    /// <summary>
    /// 単一の AudioSource でクリップを再生する共通処理。
    /// </summary>
    private void PlayClip(AudioSource source, AudioClip clip, bool loop = false)
    {
        if (clip == null)
        {
            Debug.LogError("[AudioManager] 再生対象の AudioClip が null です。");
            return;
        }

        if (source.clip == clip && source.isPlaying) return;

        source.clip = clip;
        source.loop = loop;
        source.Play();
    }

    /// <summary>
    /// 複数の AudioSource のいずれかで効果音を再生する共通処理。
    /// </summary>
    private void PlayClip(AudioSource[] sources, AudioClip clip)
    {
        if (clip == null)
        {
            Debug.LogWarning("[AudioManager] 再生対象の SE AudioClip が null です。");
            return;
        }

        foreach (var src in sources)
        {
            if (!src.isPlaying)
            {
                src.PlayOneShot(clip);
                return;
            }
        }

        // 全て使用中なら先頭で再生
        sources[0].PlayOneShot(clip);
    }

    /// <summary>
    /// 設定した BGM と SE の音量を適用する。
    /// </summary>
    private void ApplyVolumes()
    {
        if (bgmSource != null) bgmSource.volume = bgmVolume;
        if (seSources != null)
            foreach (var src in seSources)
                src.volume = seVolume;
    }

    #endregion
}
