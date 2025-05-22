using UnityEngine;

public class StageManager : SingletonMonoBehaviour<StageManager>
{
    // 現在のステージID
    private string currentStageId;

    // ステージ設定テーブル
    private StageConfigTable stageConfigTable;

    private bool isStageSelected;
    public bool IsStageSelected => isStageSelected;


    public void SetStageSelected(bool stageSelected )
    {
        isStageSelected = stageSelected;    
    }
    public StageConfigTable GetStageConfigTable()
    {
        return stageConfigTable;
    }

    public void SetupStageTable(StageConfigTable table)
    {
        stageConfigTable = table;

    }
    // ステージをセットアップする
    public void SetupStage(StageConfigTable table, string stageId)
    {
        stageConfigTable = table;
        currentStageId = stageId;

        // ステージ設定を取得
        StageConfig stageConfig = stageConfigTable.GetStageConfig(currentStageId);
        if (stageConfig == null)
        {
            Debug.LogError($"[StageManager] ステージ設定が見つかりません！ ID: {currentStageId}");
            return;
        }

        // ここでステージに関連するBGM、譜面などをセットアップする処理を追加
        Debug.Log($"ステージ設定完了: {currentStageId}, BGM: {stageConfig.StageBgm.BgmId}, 譜面: {stageConfig.ChartFileName}");
    }

    internal StageConfig GetCurrentStageConfig()
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

        return stageConfig;
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
