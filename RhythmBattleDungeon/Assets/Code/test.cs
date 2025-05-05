using UnityEngine;

public class test : MonoBehaviour
{
    [SerializeField]StageConfigTable
        stageConfigTable;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // ステージセットアップ
        StageManager.Instance.SetupStage(stageConfigTable, "stage1");

        // BGM再生
        string bgmId = StageManager.Instance.GetCurrentStageBGMId();
        AudioManager.Instance.PlayBGMById(bgmId);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
