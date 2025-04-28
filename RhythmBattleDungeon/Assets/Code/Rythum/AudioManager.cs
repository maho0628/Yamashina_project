using UnityEngine;

public class AudioManager : SingletonMonobehaviour<AudioManager>
{
    private AudioSource bgmSource;
    private AudioSource[] seSources;

    [SerializeField, Header("同時に鳴らせるSEチャンネル数")]
    private int seSourceCount = 3;

    private BGMConfigTable bgmConfigTable;
    private SEConfigTable seConfigTable;
    // BGMとSEの音量を別々に管理する
    private float bgmVolume = 1.0f;
    private float seVolume = 1.0f;

    public override void Awake()
    {
        base.Awake();
        InitializeAudioSources();
    }

    private void InitializeAudioSources()
    {
        bgmSource = gameObject.AddComponent<AudioSource>();
        seSources = new AudioSource[seSourceCount];
        for (int i = 0; i < seSourceCount; i++)
        {
            seSources[i] = gameObject.AddComponent<AudioSource>();
        }
    }

    public void SetupBGMConfigTable(BGMConfigTable table)
    {
        bgmConfigTable = table;
    }

    public void PlayBGMById(string bgmId)
    {
        if (bgmConfigTable == null)
        {
            Debug.LogError("[AudioManager] BGMConfigTableが設定されていません！");
            return;
        }

        BGMConfig bgmConfig = bgmConfigTable.GetBgmConfig(bgmId);
        if (bgmConfig == null)
        {
            Debug.LogError($"[AudioManager] BGMConfigが見つかりません！ ID: {bgmId}");
            return;
        }

        PlayBGM(bgmConfig.BgmAudioClip);

    }

    public void PlayBGM(AudioClip clip)
    {
        if (clip == null)
        {
            Debug.LogError("[AudioManager] 再生しようとしたBGMがnullです！");
            return;
        }

        if (bgmSource.clip == clip && bgmSource.isPlaying)
            return;

        bgmSource.clip = clip;
        bgmSource.loop = true;
        bgmSource.Play();
    }

    public void StopBGM()
    {
        if (bgmSource.isPlaying)
        {
            bgmSource.Stop();
        }
    }
    // セットアップ
    public void SetupSEConfigTable(SEConfigTable table)
    {
        seConfigTable = table;
    }

    // IDで効果音を鳴らす
    public void PlaySEById(string seId)
    {
        if (seConfigTable == null)
        {
            Debug.LogError("[AudioManager] SEConfigTableが設定されていません！");
            return;
        }

        SEConfig seConfig = seConfigTable.GetSeConfig(seId);
        if (seConfig == null)
        {
            Debug.LogError($"[AudioManager] SEConfigが見つかりません！ ID: {seId}");
            return;
        }
    }
    public void PlaySE(AudioClip clip)
    {
        if (clip == null)
        {
            Debug.LogWarning("[AudioManager] 再生しようとしたSEがnullです！");
            return;
        }

        foreach (var source in seSources)
        {
            if (!source.isPlaying)
            {
                source.PlayOneShot(clip);
                return;
            }
        }

        seSources[0].PlayOneShot(clip);
    }

    public float GetCurrentBGMTime()
    {
        return bgmSource != null && bgmSource.clip != null ? bgmSource.time : 0f;
    }

    public void SetBGMVolume(float volume)
    {
        bgmVolume = Mathf.Clamp01(volume);
        if (bgmSource != null)
        {
            bgmSource.volume = bgmVolume;
        }
    }

    public void SetSFXVolume(float volume)
    {
        seVolume = Mathf.Clamp01(volume);
        foreach (var source in seSources)
        {
            source.volume = seVolume;
        }
    }

    /// <summary>
    /// 現在のBGM音量を取得する
    /// </summary>
    public float GetBGMVolume()
    {
        return bgmVolume;
    }

    /// <summary>
    /// 現在のSE音量を取得する
    /// </summary>
    public float GetSEVolume()
    {
        return seVolume;
    }
}
