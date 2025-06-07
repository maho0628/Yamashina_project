using UnityEngine;

public class GameInitializer : SingletonMonoBehaviour<GameInitializer>
{
    private BGMConfigTable bgmConfigTable;
    private SEConfigTable seConfigTable;
    private StageConfigTable stageConfigTable;
    private SceneDatabase sceneDatabase;
    private GameObject fadePrefab; // フェード用プレハブ
    private SceneReference initialScene;
    private SceneBGMConfigTable sceneBGMConfigTable;
    private GameSettings gameSettings;
    private GameExitSettings gameExitSettings; // 追加

    private bool isInitialized = false;
    internal bool Initialized => isInitialized;
    internal GameSettings GetGameSettings() { return gameSettings; }
    internal SceneBGMConfigTable GetSceneBGMConfigTable() { return sceneBGMConfigTable; }
    internal GameExitSettings GetGameExitSettings() { return gameExitSettings; } // 追加
    internal SceneDatabase GetSceneDatabase() { return sceneDatabase; }

    internal BGMConfigTable GetBGMConfigTable() { return bgmConfigTable; }
    internal void SetUpGameInitialize()
    {
        if (isInitialized)return;   
        DebugManager.Log("GameInitializer Awake");

        // 既存のリソースロード
        bgmConfigTable = Resources.Load<BGMConfigTable>("ScriptableObject/BGMConfig");
        seConfigTable = Resources.Load<SEConfigTable>("ScriptableObject/SEConfig");
        stageConfigTable = Resources.Load<StageConfigTable>("ScriptableObject/stageConfig");
        sceneDatabase = Resources.Load<SceneDatabase>("ScriptableObject/sceneDatabase");
        initialScene = Resources.Load<SceneReference>("ScriptableObject/TitleScene");
        gameSettings = Resources.Load<GameSettings>("ScriptableObject/gameSettings");
        sceneBGMConfigTable = Resources.Load<SceneBGMConfigTable>("ScriptableObject/SceneBGMConfigTable");

        // GameExitSettingsの追加
        gameExitSettings = Resources.Load<GameExitSettings>("ScriptableObject/GameExitSettings");

        if (initialScene == null)
        {
            DebugManager.LogError("初期シーンの SceneReference が見つかりません。Resources/Scenes/TitleScene.asset を確認してください。");
            return;
        }

        if (gameExitSettings == null)
        {
            DebugManager.LogWarning("GameExitSettings が見つかりません。Resources/ScriptableObject/GameExitSettings.asset を確認してください。");
        }

        fadePrefab = Resources.Load<GameObject>("fadePrefab");

        // AudioManagerを強制的に先に生成
        var audio = AudioManager.Instance;
        // 設定テーブルを渡す
        audio.SetupBGMConfigTable(bgmConfigTable);
        audio.SetupSEConfigTable(seConfigTable);
        DebugManager.Log("AudioManager 初期化完了");

        StageManager.Instance.SetupStageTable(stageConfigTable);
        stageConfigTable.GetAllStageConfigs().ForEach(config => { config.InitializeBGMTable(bgmConfigTable); });

        var sceneTransition = SceneTransitionManager.Instance;
        sceneTransition.SetFadePrefab(fadePrefab);
        sceneTransition.SetInitialScene(initialScene);

        GameManagerRetryExtensions.ClearRetryInfo();

        isInitialized = true;
    }
}