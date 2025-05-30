using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

/// <summary>
/// 楽曲選択画面
/// 
/// </summary>
public class SongItemUI : MonoBehaviour, IPoolable<SongItemUI>
{

    #region フィールド定義

    /// <summary>
    /// ジャケット画像コンポーネントを設定するインスペクターで設定しない場合は自動取得
    /// </summary>
    [SerializeField,Header("ジャケット画像のイメージコンポーネントを設定")] private Image jacketImage;

    /// <summary>
    /// 
    /// </summary>
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private Button selectButton;

    private string songId;
    private UIObjectPool<SongItemUI> songItemUiPool;

    #endregion
    private void Awake()
    {

        if (jacketImage == null)
        {
            jacketImage = transform.Find("BgmJacketImage")?.GetComponent<Image>();

            if (jacketImage == null)
            {
                Debug.LogError($"[{gameObject.name}] BgmJacketImageが見つかりません。子オブジェクトの名前とImageコンポーネントを確認してください。");
            }
            else if (jacketImage.gameObject.name != "BgmJacketImage")
            {
                Debug.LogWarning($"[{gameObject.name}] オブジェクト名が'{jacketImage.gameObject.name}'です。'BgmJacketImage'に変更するかインスペクターで設定してください。");
            }
            else
            {
                Debug.Log($"[{gameObject.name}] BgmJacketImage取得完了");
            }
        }
        if (titleText == null)
        {
            titleText = transform.Find("BGMName")?.GetComponent<TextMeshProUGUI>();

            if (titleText == null)
            {
                Debug.LogError($"[{gameObject.name}] BGMNameが見つかりません。子オブジェクトの名前とTextMeshProUGUIコンポーネントを確認してください。");
            }
            else
            {
                Debug.Log($"[{gameObject.name}] BGMName取得完了: {titleText.text}");
            }
        }

        if (selectButton == null)
        {
            selectButton = GetComponent<Button>();

            if (selectButton == null)
            {
                Debug.LogError($"[{gameObject.name}] Buttonコンポーネントが見つかりません。");
            }
            else
            {
                Debug.Log($"[{gameObject.name}] Button取得完了");
            }
        }

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
                StageManager.Instance.SetStageSelected(true);

                break; // 見つかったらループ終了
            }

        }

        Debug.Log($"[SongItemUI] 選択された曲: {songId}");
    }

    public void OnCreated(UIObjectPool<SongItemUI> pool)
    {
        this.songItemUiPool = pool;
    }




}
