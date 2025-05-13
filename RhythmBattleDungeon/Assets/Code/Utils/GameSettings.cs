using UnityEngine;

[CreateAssetMenu(fileName = "GameSettings", menuName = "Scriptable Objects/GameSettings")]
public class GameSettings : ScriptableObject
{

    [SerializeField, Header("同時に再生できる効果音の数")]
    private int maxSeCount = 3;

    [SerializeField, Header("初期 BGM 音量 (0.0 - 1.0)")]
    private float initialBgmVolume = 1f;

    [SerializeField, Header("初期 SE 音量 (0.0 - 1.0)")]
    private float initialSeVolume = 1f;

    [SerializeField, Header("フェードの速度")] 
    private float fadeSpeed = 1f;

    internal int MaxSeCount => maxSeCount;
    internal float InitialBgmVolume => initialBgmVolume;
    internal float InitialSeVolume => initialSeVolume;

    internal float FadeSpeed => fadeSpeed;  
}
