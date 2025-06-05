using UnityEngine;


/// <summary>
/// 
/// </summary>
public static class GameManagerRetryExtensions
{
    /// <summary>
    /// ゲーム開始時にRetryManagerへ情報を保存
    /// </summary>
    public static void SaveRetryInfo(string stageId)
    {
        if (RetryManager.Instance != null)
        {
            RetryManager.Instance.SaveGameStartInfo(stageId);
        }
    }

    /// <summary>
    /// ゲーム終了時にリトライ情報をクリア（必要に応じて）
    /// </summary>
    public static void ClearRetryInfo()
    {
        if (RetryManager.Instance != null)
        {
            RetryManager.Instance.ClearRetryInfo();
        }
    }
}