using System;
using UnityEngine;

[Serializable]
/// <summary>
/// 
/// </summary>
public class CreditConfig
{
    [Header("テキスト設定")]
    [SerializeField] private TextBasicSettings textSettings;
    [SerializeField] private TextLayoutSettings layoutSettings;

    public TextBasicSettings TextSettings => textSettings;
    public TextLayoutSettings LayoutSettings => layoutSettings;


    public float ScrollSpeed;
    public float DisplayDuration;
    public float FadeDuration;
    public GameObject PrefabReference;
}
