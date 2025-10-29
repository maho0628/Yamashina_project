using System;
using UnityEngine;

/// <summary>
/// クレジット画面の設定
/// クレジット画面を制作予定のため前準備として作成
/// </summary>
[Serializable]
public class CreditConfig
{
    /// <summary>
    /// テキストの基本設定のデータ
    /// </summary>
    [SerializeField, Tooltip("テキストの基本設定")]
    private TextBasicSettings textSettings;

    /// <summary>
    /// レイアウト＆Canvas設定のデータ
    /// </summary>
    [SerializeField,Tooltip("レイアウト＆Canvas設定")]
    private TextLayoutSettings layoutSettings;

    /// <summary>
    /// テキストの基本設定のデータの読み取り専用
    /// </summary>
    internal TextBasicSettings TextSettings => textSettings;

    /// <summary>
    /// レイアウト＆Canvas設定の読み取り専用
    /// </summary>
    internal TextLayoutSettings LayoutSettings => layoutSettings;

}
