using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class SongItemUI : MonoBehaviour, IPoolable<SongItemUI>
{
    [SerializeField] private Image jacketImage;
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private Button selectButton;

    private string songId;
    private UIObjectPool<SongItemUI> pool;

    private void Awake()
    {
        if (jacketImage == null)
            jacketImage = transform.Find("BgmJacketImage")?.GetComponent<Image>();

        if (titleText == null)
            titleText = transform.Find("BGMName")?.GetComponent<TextMeshProUGUI>();
        Debug.Log(titleText.text);
        if (selectButton == null)
            selectButton = GetComponent<Button>();

        selectButton.onClick.AddListener(OnSelectButtonClicked);
    }

    public void Setup(BGMConfig config)
    {
        Debug.Log("セットアップ");
        Debug.Log(config);
        if (config == null) return;

        songId = config.BgmId;
        titleText.text = config.BgmDisplayName;
        Debug.Log(config.BgmDisplayName);


        Debug.Log(titleText.text + "タイトルテキスト表示");

        jacketImage.sprite = config.BgmJacketImage;
    }

    private void OnSelectButtonClicked()
    {
       

        var sceneDatabase=GameInitializer.Instance.GetSceneDatabase();
        string currentSceneName = SceneManager.GetActiveScene().name;

        var nextScene = sceneDatabase.GetNextScene(currentSceneName);
        SceneTransitionManager.Instance.TransitionTo(nextScene); 
       var stageConfigTable = StageManager.Instance.GetStageConfigTable();
        var allStageConfigs = StageManager.Instance.GetStageConfigTable().GetAllStageConfigs();
        foreach (var stageConfig in allStageConfigs)
        {
            if (stageConfig.StageBgm.BgmId == songId)
            {
                Debug.Log($"選択されたステージID: {stageConfig.StageId}（曲ID: {songId}）");
                StageManager.Instance.SetupStage(stageConfigTable, stageConfig.StageId);

                break; // 見つかったらループ終了
            }

        }

        Debug.Log($"[SongItemUI] 選択された曲: {songId}");
    }

    public void OnCreated(UIObjectPool<SongItemUI> pool)
    {
        this.pool = pool;
    }


}
