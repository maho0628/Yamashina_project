using UnityEngine;

[System.Serializable]
public class ScoreSetting
{
    [Header("判定ごとのスコア設定")]
    [SerializeField] private int perfectScore = 10000;
    [SerializeField] private int brilliantScore = 10000;
    [SerializeField] private int greatScore = 7000;
    [SerializeField] private int badScore = 1000;

    // 外部から読み取り専用でアクセスできるようにする

    internal int PerfectScore => perfectScore;
    internal int BrilliantScore => brilliantScore;
    internal int GreatScore => greatScore;
    internal int BadScore => badScore;
}



