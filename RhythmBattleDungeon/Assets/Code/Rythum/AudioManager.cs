using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

/// <summary>
/// ゲーム全体の音声再生を管理するシングルトンコンポーネント。
/// BGM と SE（効果音）の再生、停止、ボリューム調整などを行う。
/// </summary>
public class AudioManager : SingletonMonoBehaviour<AudioManager>
{
    #region オーディオソース (コード管理／Inspector非表示)内部管理用変数

    /// <summary>
    /// BGM 再生用 AudioSource（コードで生成）
    /// </summary>
    private AudioSource bgmSource;

    /// <summary>
    /// 同時再生用 SE AudioSource 配列（コードで生成）
    /// </summary>
    private AudioSource[] seSources;

    #endregion


    #region その他の内部管理用変数

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

    /// <summary>
    /// 現在流れているBGMのID
    /// </summary>
    private BGMName currentBgmId;

    /// <summary>
    /// ゲームの初期設定
    /// </summary>
    private GameSettings gameSettings;

    #endregion


    #region ゲッターメソッド

    /// <summary>
    ///  現在の BGM 再生位置（秒）を返す。
    /// </summary>
    /// <returns>float</returns>
    internal float GetCurrentBGMTime()
    {
        //再生中ではない
        if (bgmSource == null || bgmSource.clip == null || !bgmSource.isPlaying)
            return 0f;

        //オーディオシステムの現在時刻-スタート時のオーディオソースのDspTimeを引いて経過時間を計算して返す
        return (float)(AudioSettings.dspTime - bgmStartDspTime);
    }

    /// <summary>
    /// 現在設定されている BGM 音量を返す。
    /// </summary>
    /// <returns>float</returns>
    internal float GetBGMVolume() => bgmVolume;

    /// <summary>
    /// 現在設定されている SE 音量を返す。
    /// </summary>
    /// <returns>float</returns>
    internal float GetSEVolume() => seVolume;

    /// <summary>
    /// 現在流れているBGMのIDを返す
    /// </summary>
    /// <returns>string</returns>
    internal BGMName GetCurrentBGMId()
    {
        return currentBgmId;
    }

    /// <summary>
    /// BGMが鳴り終わったかどうかを返す
    /// </summary>
    /// <returns>bool</returns>
    internal bool IsBGMFinished()
    {
        //BGMが鳴り終わった
        return bgmSource != null && !bgmSource.isPlaying && bgmSource.time > 0;
    }

    #endregion


    #region セッターメソッド

    /// <summary>
    /// BGM 音量を設定する (0.0 - 1.0)。
    /// </summary>
    /// <param name="volume">BGM音量</param>
    internal void SetBGMVolume(float volume)
    {
        // 引数で渡された音量値を0未満や1を超える値にならないように制限し、
        // その制限された値を bgmVolumeに代入してBGMの音量を設定する
        bgmVolume = Mathf.Clamp01(volume);

        //音量をBGMのオーディオソースに適応
        ApplyVolumes();
    }

    /// <summary>
    /// SE 音量を設定する (0.0 - 1.0)。
    /// </summary>
    /// <param name="volume">SE音量</param>
    internal void SetSFXVolume(float volume)
    {
        // 引数で渡された音量値を0未満や1を超える値にならないように制限し、
        // その制限された値をseVolumeに代入してSEの音量を設定する
        seVolume = Mathf.Clamp01(volume);

        //音量をSEのオーディオソースに適応
        ApplyVolumes();
    }

    /// <summary>
    /// BGM 設定テーブルを登録する。
    /// </summary>
    /// <param name="bgmTable">ScriptableObject で用意した BGM 設定テーブル</param>
    internal void SetupBGMConfigTable(BGMConfigTable bgmTable)
    {
        bgmConfigTable = bgmTable;
    }

    /// <summary>
    /// SE 設定テーブルを登録する。
    /// </summary>
    /// <param name="seTable">ScriptableObject で用意した SE 設定テーブル</param>
    internal void SetupSEConfigTable(SEConfigTable seTable)
    {
        seConfigTable = seTable;
    }

    #endregion



    /// <summary>
    /// 初期化処理：AudioSource のセットアップと初期音量を設定する。
    /// </summary>
    protected override void Awake()
    {
        base.Awake();
        DebugManager.Log("AudioManager Awake");
        gameSettings = GameInitializer.Instance.GetGameSettings();
        InitializeAudioSources();
        InitializeAudioVolumes();
    }


    #region ゲッター、セッター以外の外部で呼び出し可能な関数（オーディオ関連)

    /// <summary>
    /// 指定されたBGMが未再生または異なる場合に再生を開始する
    /// </summary>
    /// <param name="bgmId">BGMConfigTable に登録された識別子</param>
    internal void PlayBGMIfNotPlaying(BGMName bgmId)
    {
        if (string.IsNullOrEmpty(bgmId.ToString())) return;

        if (currentBgmId == bgmId && bgmSource.isPlaying)
        {
            // 同じ曲が流れていれば何もしない
            return;
        }
        //
        PlayBGMById(bgmId,islooped:true, forceReplay: false);
    }

    /// <summary>
    /// 指定された BGM を強制的に初めから再生し、ループしない設定にする。
    /// </summary>
    /// <param name="bgmId">BGMConfigTable に登録された識別子</param>
    internal void ForcePlayBGM(BGMName bgmId)
    {
        if (string.IsNullOrEmpty(bgmId.ToString())) return;

        PlayBGMById(bgmId,islooped :false,forceReplay: true);
    }

    /// <summary>
    /// 現在再生中の BGM を停止する。
    /// </summary>
    internal void StopBGM()
    {
        if (bgmSource.isPlaying)
        {
            bgmSource.Stop();
            DebugManager.Log("[AudioManager] BGM 停止");
        }
    }

    /// <summary>
    /// 指定した SE ID の効果音を再生する。
    /// </summary>
    /// <param name="seId">SEConfigTable に登録された識別子</param>
    internal void PlaySEById(string seId)
    {
        if (seConfigTable == null)
        {
            DebugManager.LogError("[AudioManager] SEConfigTable が未設定です。");
            return;
        }

        var seConfig = seConfigTable.GetSeConfig(seId);
        if (seConfig == null)
        {
            DebugManager.LogError($"[AudioManager] SEConfig が見つかりません (ID: {seId})");
            return;
        }

        PlayClipsMultiAudioSources(seSources, seConfig.SeAudioClip);
    }

    #endregion


    #region プライベートメソッド

    /// <summary>
    /// AudioSource を初期化し、BGM と SE 用を生成する。
    /// </summary>
    private void InitializeAudioSources()
    {
        //  BGMとSEのオーディオソースを必要な数分新規生成
        bgmSource = gameObject.AddComponent<AudioSource>();
        seSources = new AudioSource[gameSettings.MaxSeCount];
        for (int i = 0; i < gameSettings.MaxSeCount; i++)
            seSources[i] = gameObject.AddComponent<AudioSource>();
    }

    /// <summary>
    /// BGM、SEの音量を初期化
    /// </summary>
    private void InitializeAudioVolumes()
    {
        bgmVolume = Mathf.Clamp01(gameSettings.InitialBgmVolume);
        seVolume = Mathf.Clamp01(gameSettings.InitialSeVolume);
        ApplyVolumes();
    }

    /// <summary>
    /// 単一の AudioSource でクリップを再生する処理。
    /// </summary>
    private void PlayClips(AudioSource source, AudioClip clip, bool loop = false, bool forceReplay = false)
    {
        if (clip == null)
        {
            DebugManager.LogError("[AudioManager] 再生対象の AudioClip が null です。");
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
    private void PlayClipsMultiAudioSources(AudioSource[] sources, AudioClip clip)
    {
        if (clip == null)
        {
            DebugManager.LogWarning("[AudioManager] 再生対象の SE AudioClip が null です。");
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
    /// 指定した BGM ID の曲をループ再生する。
    /// </summary>
    /// <param name="bgmId">BGMConfigTable に登録された識別子</param>
    /// <param name="forceReplay">最初からBGMを流しなおすかどうか</param>
    /// <param name="islooped">ループ対応させるかどうか</param>
    private void PlayBGMById(BGMName bgmId, bool islooped ,bool forceReplay = false)
    {
        var bgmConfig = bgmConfigTable.GetBgmConfig(bgmId);
        if (bgmConfig == null)
        {
            DebugManager.LogError($"[AudioManager] BGM ID '{bgmId}' が見つかりません。");
            return;
        }

        PlayClips(bgmSource, bgmConfig.BgmAudioClip, loop: islooped, forceReplay: forceReplay);
        currentBgmId = bgmId;
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
