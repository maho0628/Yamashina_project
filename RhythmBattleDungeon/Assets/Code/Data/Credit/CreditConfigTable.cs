using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ゲーム内で使用するクレジット設定の一覧を保持する ScriptableObject
/// </summary>
[CreateAssetMenu(fileName = "CreditConfigTable", menuName = "Credits/CreditConfigTable")]
public class CreditConfigTable : ScriptableObject
{
    /// <summary>
    /// クレジット設定のリスト
    /// </summary>
    [SerializeField, Tooltip("クレジット設定のリスト")]
    private List<CreditConfig> creditConfigsLists;

    /// <summary>
    /// クレジット設定のリストの読み取り専用
    /// </summary>
    internal List<CreditConfig> CreditConfigsLists => creditConfigsLists;

}
