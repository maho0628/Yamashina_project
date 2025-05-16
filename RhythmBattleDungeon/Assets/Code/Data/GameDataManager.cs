using UnityEngine;

public class GameDataManager : SingletonMonoBehaviour<GameDataManager>
{
    public BGMConfigTable BGMConfigTable { get; private set; }
    public SEConfigTable SEConfigTable { get; private set; }
    public StageConfigTable StageConfigTable { get; private set; }
    public SceneBGMConfigTable SceneBGMConfigTable { get; private set; }

    public void Setup(BGMConfigTable bgm, SEConfigTable se, StageConfigTable stage, SceneBGMConfigTable sceneBgm)
    {
        BGMConfigTable = bgm;
        SEConfigTable = se;
        StageConfigTable = stage;
        SceneBGMConfigTable = sceneBgm;
    }
}
