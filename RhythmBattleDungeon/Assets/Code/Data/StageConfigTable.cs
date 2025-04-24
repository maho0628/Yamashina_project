using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameData/Stage Config Table")]
// 各譜面ステージ専用のスクリプタブルオブジェクト
public class StageConfigTable : ScriptableObject
{
    //ステージ設定のリスト
    [SerializeField,Header("ステージ音源のリスト")] 
    private List<StageConfig> stagesBgmList;


    //ステージIDを探してStageConfigデータを返すメソッド
    internal StageConfig GetStageConfig(string stageid)
    {
        return stagesBgmList.Find(s => s.StageId == stageid);
    }

}
[System.Serializable]
// ステージ設定クラス
public class StageConfig
{
    //スプレッドシートで管理しているステージID名
    [SerializeField, Header("ステージID名")] 
    private string stageId;

    //BGM音源の設定内容
    [SerializeField, Header("BGM音源の設定内容")] 
    private BGMConfig stageBgm;

    //譜面データJsonファイル名
    [SerializeField, Header("譜面データJsonファイル名")]
    private string chartFileName;


    //ステージ名を読み取りをする為のゲッター
    internal string StageId => stageId;

    //BGMの名前の読み取りをする為のゲッター
    internal BGMConfig StageBgm => stageBgm;

    //譜面ファイル名の読み取りをする為のゲッター
    internal string ChartFileName => chartFileName;

}

