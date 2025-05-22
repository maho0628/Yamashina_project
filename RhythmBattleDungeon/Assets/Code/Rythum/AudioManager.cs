using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ゲーム全体の音声再生を管理するシングルトンコンポーネント。
/// BGM と SE（効果音）の再生、停止、ボリューム調整などを行う。
/// </summary>
public class AudioManager : SingletonMonoBehaviour<AudioManager>
{
    #region オーディオソース (コード管理／Inspector非表示)

    /// <summary>
    /// BGM 再生用 AudioSource（コードで生成）
    /// </summary>
    private AudioSource bgmSource;

    /// <summary>
    /// 同時再生用 SE AudioSource 配列（コードで生成）
    /// </summary>
    private AudioSource[] seSources;

    #endregion


    #region 内部管理用フィールド

    /// <summary>
    ///  現在の BGM 音量 (0.0 - 1.0)
    /// </summary>
    private float bgmVolume;

    /// <summary>
    /// 現在の SE 音量 (0.0 - 1.0)
    /// </summary>
    private float seVolume;

    /// <summary>
    /// スタート時のオーディオソースの現在時刻（DspTime)
    /// </summary>
    private double bgmStartDspTime;


    /// <summary>
    /// 登録されている BGM 設定テーブル
    /// ScriptableObjectとして保持（元データ）
    /// </summary>
    private BGMConfigTable bgmConfigTable;

    /// <summary>
    /// 登録されている SE 設定テーブル
    /// ScriptableObjectとして保持（元データ）
    /// </summary>
    private SEConfigTable seConfigTable;
    private string currentBgmId;

    #endregion


    /// <summary>
    /// 現在の BGM 再生位置（秒）を返す。
    /// </summary>
    public float GetCurrentBGMTime()
    {
        if (bgmSource == null || bgmSource.clip == null || !bgmSource.isPlaying)
            return 0f;

        //オーディオシステムの現在時刻-スタート時のオーディオソースのDspTimeを引いて経過時間を計算して返す
        return (float)(AudioSettings.dspTime - bgmStartDspTime);
    }

    /// <summary>
    /// 現在設定されている BGM 音量を返す。
    /// </summary>
    public float GetBGMVolume() => bgmVolume;

    /// <summary>
    /// 現在設定されている SE 音量を返す。
    /// </summary>
    public float GetSEVolume() => seVolume;

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



    public bool IsBGMFinished()
    {
        return bgmSource != null && !bgmSource.isPlaying && bgmSource.time > 0;
    }

    /// <summary>
    /// 初期化処理：AudioSource のセットアップと初期音量を設定する。
    /// </summary>
    protected override void Awake()
    {
        base.Awake();
        Debug.Log("AudioManager Awake");

        InitializeAudioSources();

        var gameInitializerSetting = GameInitializer.Instance.GetGameSettings();
        bgmVolume = Mathf.Clamp01(gameInitializerSetting.InitialBgmVolume);
        seVolume = Mathf.Clamp01(gameInitializerSetting.InitialSeVolume);
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
    /// <param name="forceReplay">最初からBGMを流しなおすかどうか</param>
    private void PlayBGMById(string bgmId, bool forceReplay = false)
    {
        var bgmConfig = bgmConfigTable.GetBgmConfig(bgmId);
        if (bgmConfig == null)
        {
            Debug.LogError($"[AudioManager] BGM ID '{bgmId}' が見つかりません。");
            return;
        }

        PlayClip(bgmSource, bgmConfig.BgmAudioClip, loop: true, forceReplay: forceReplay);
        currentBgmId = bgmId;
    }


    public void PlayBGMIfNotPlaying(string bgmId)
    {
        Debug.Log(bgmId + "PlayBGMIfNotPlayingがもってる");
        if (string.IsNullOrEmpty(bgmId)) return;

        if (currentBgmId == bgmId && bgmSource.isPlaying)
        {
            // 同じ曲が流れていれば何もしない
            return;
        }

        PlayBGMById(bgmId, forceReplay: false);
    }
    public void ForcePlayBGM(string bgmId)
    {
        if (string.IsNullOrEmpty(bgmId)) return;

        PlayBGMById(bgmId, forceReplay: true);
        bgmSource.loop = false; 
    }
    public string GetCurrentBGMId()
    {
        return currentBgmId;
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


    #region プライベートメソッド

    /// <summary>
    /// AudioSource を初期化し、BGM と SE 用を生成する。
    /// </summary>
    private void InitializeAudioSources()
    {
        var gameInitializerSetting = GameInitializer.Instance.GetGameSettings();

        bgmSource = gameObject.AddComponent<AudioSource>();
        seSources = new AudioSource[gameInitializerSetting.MaxSeCount];
        for (int i = 0; i < gameInitializerSetting.MaxSeCount; i++)
            seSources[i] = gameObject.AddComponent<AudioSource>();
    }

    /// <summary>
    /// 単一の AudioSource でクリップを再生する共通処理。
    /// </summary>
    private void PlayClip(AudioSource source, AudioClip clip, bool loop = false, bool forceReplay = false)
    {
        if (clip == null)
        {
            Debug.LogError("[AudioManager] 再生対象の AudioClip が null です。");
            return;
        }

        if (!forceReplay && source.clip == clip && source.isPlaying) return;

        source.clip = clip;
        source.loop = loop;
        bgmStartDspTime = AudioSettings.dspTime;

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
