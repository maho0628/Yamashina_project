using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CreditConfigTable", menuName = "Credits/CreditConfigTable")]
public class CreditConfigTable : ScriptableObject
{
    [Header("クレジットのリスト")]
    [SerializeField] private List<CreditConfig> creditConfigs;


    public List<CreditConfig> CreditConfigs => creditConfigs;
}
