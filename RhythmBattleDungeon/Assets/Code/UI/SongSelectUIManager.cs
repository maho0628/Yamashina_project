using UnityEngine;

public class SongSelectUIManager : MonoBehaviour
{
    [SerializeField] private SongItemUIPool songItemPool;
    [SerializeField] private Transform contentParent;

    private bool isInitialized = false;


  
    private void Start()
    {
        GenerateSongList();
       
    }

    private void GenerateSongList()
    {
        var allConfigs = StageManager.Instance.GetStageConfigTable().GetAllStageConfigs();

        // 一度だけ生成
        if (isInitialized) return;
        isInitialized = true;

        foreach (var config in allConfigs)
        {
            var songUI = songItemPool.Get();
            songUI.transform.SetParent(contentParent, false);
            songUI.Setup(config.StageBgm);


       }
    }

    // 今は未使用だが再読み込みしたいとき用に残す
    public void ClearSongList()
    {
        foreach (Transform child in contentParent)
        {
            Destroy(child.gameObject); // 完全に削除する（任意）
        }

        isInitialized = false;
    }
}
