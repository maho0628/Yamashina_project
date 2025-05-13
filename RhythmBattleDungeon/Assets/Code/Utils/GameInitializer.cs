using UnityEngine;

public class GameInitializer : SingletonMonoBehaviour<GameInitializer>
{
    private BGMConfigTable bgmConfigTable;
    private SEConfigTable seConfigTable;

    private StageConfigTable stageConfigTable;

    private SceneDatabase sceneDatabase;

    private GameObject fadePrefab; // フェード用プレハブ

    private SceneReference initialScene;

    private GameSettings gameSettings;
    public SceneDatabase GetSceneDatabase() { return sceneDatabase; }
    private bool isInitialized = false;
    public bool Initialized => isInitialized;
    internal GameSettings GetGameSettings() { return gameSettings; }    
    public void SetUpGameInitialize()
    {
        if (isInitialized) return;

        Debug.Log("GameInitializer Awake");

        bgmConfigTable = Resources.Load<BGMConfigTable>("ScriptableObject/BGMConfig");
        seConfigTable = Resources.Load<SEConfigTable>("ScriptableObject/SEConfig");
        stageConfigTable = Resources.Load<StageConfigTable>("ScriptableObject/stageConfig");
        sceneDatabase = Resources.Load<SceneDatabase>("ScriptableObject/sceneDatabase");
        initialScene = Resources.Load<SceneReference>("ScriptableObject/TitleScene");
        gameSettings = Resources.Load<GameSettings>("ScriptableObject/gameSettings");
        if (initialScene == null)
        {
            Debug.LogError("初期シーンの SceneReference が見つかりません。Resources/Scenes/TitleScene.asset を確認してください。");
            return;
        }
        fadePrefab = Resources.Load<GameObject>("fadePrefab");
        // AudioManagerを強制的に先に生成
        var audio = AudioManager.Instance;

        // 設定テーブルを渡す
        audio.SetupBGMConfigTable(bgmConfigTable);
        audio.SetupSEConfigTable(seConfigTable);
        Debug.Log("AudioManager 初期化完了");

        
        StageManager.Instance.SetupStageTable(stageConfigTable);

        var sceneTransition = SceneTransitionManager.Instance;

        sceneTransition.SetFadePrefab(fadePrefab);  
        sceneTransition.SetInitialScene(initialScene); 
        isInitialized = true;

    }
}
