using UnityEngine;


public class StageManager : SingletonMonoBehaviour<StageManager>
{
    // 現在のステージID
    private string currentStageId;

    // ステージ設定テーブル
    private StageConfigTable stageConfigTable;

    // ステージをセットアップする
    public void SetupStage(StageConfigTable table, string stageId)
    {
        stageConfigTable = table;
        currentStageId = stageId;
    }

    // 現在のステージのBGMIDを取得する
    public string GetCurrentStageBGMId()
    {
        if (stageConfigTable == null)
        {
            Debug.LogError("[StageManager] StageConfigTableが設定されていません！");
            return null;
        }

        StageConfig stageConfig = stageConfigTable.GetStageConfig(currentStageId);
        if (stageConfig == null)
        {
            Debug.LogError($"[StageManager] ステージ設定が見つかりません！ ID: {currentStageId}");
            return null;
        }

        return stageConfig.StageBgm.BgmId;
    }

    // 現在の譜面ファイル名を取得する
    public string GetCurrentChartFileName()
    {
        if (stageConfigTable == null)
        {
            Debug.LogError("[StageManager] StageConfigTableが設定されていません！");
            return null;
        }

        StageConfig stageConfig = stageConfigTable.GetStageConfig(currentStageId);
        if (stageConfig == null)
        {
            Debug.LogError($"[StageManager] ステージ設定が見つかりません！ ID: {currentStageId}");
            return null;
        }

        return stageConfig.ChartFileName;
    }
}

