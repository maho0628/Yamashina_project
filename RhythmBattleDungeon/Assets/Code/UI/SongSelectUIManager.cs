using UnityEngine;

public class SongSelectUIManager : MonoBehaviour
{
    [SerializeField] private StageConfigTable stageConfigTable;
    [SerializeField] private UIObjectPool<SongItemUI> songItemPool;
    [SerializeField] private Transform contentParent;

    private void Start()
    {
        GenerateSongList();
    }

    private void GenerateSongList()
    {
        foreach (var config in stageConfigTable.GetAllStageConfigs())
        {
            var songUI = songItemPool.Get();
            songUI.transform.SetParent(contentParent, false);
            songUI.Pool = songItemPool;
            songUI.Setup(config.StageBgm);
        }
    }

    public void ClearSongList()
    {
        foreach (Transform child in contentParent)
        {
            if (child.TryGetComponent<SongItemUI>(out var ui))
            {
                ui.Deactivate();
            }
        }
    }
}
