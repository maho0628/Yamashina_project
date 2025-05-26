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

        // àÍìxÇæÇØê∂ê¨
        if (isInitialized) return;
        isInitialized = true;

        foreach (var config in allConfigs)
        {
            var songUI = songItemPool.Get();
            songUI.transform.SetParent(contentParent, false);
            songUI.Setup(config.StageBgm);


       }
    }

    
}
