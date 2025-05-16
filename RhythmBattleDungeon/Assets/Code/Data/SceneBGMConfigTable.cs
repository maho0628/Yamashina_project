using System.Collections.Generic;
using UnityEngine;

public class SceneBGMConfigTable : ScriptableObject
{
    [SerializeField]
    private List<SceneBGMConfig> sceneBgmConfigs;

    private Dictionary<string, string> sceneToBgmIdMap;

    private void OnEnable()
    {
        sceneToBgmIdMap = new Dictionary<string, string>();

        foreach (var config in sceneBgmConfigs)
        {
            if (!string.IsNullOrEmpty(config.SceneName) && !string.IsNullOrEmpty(config.BgmId))
            {
                if (!sceneToBgmIdMap.ContainsKey(config.SceneName))
                {
                    sceneToBgmIdMap.Add(config.SceneName, config.BgmId);
                }
                else
                {
                    Debug.LogWarning($"[SceneBGMConfigTable] ÉVÅ[Éì '{config.SceneName}' ÇÕä˘Ç…ìoò^Ç≥ÇÍÇƒÇ¢Ç‹Ç∑ÅB");
                }
            }
        }
    }

    public string GetBgmIdForScene(string sceneName)
    {
        if (sceneToBgmIdMap == null || sceneToBgmIdMap.Count == 0)
        {
            OnEnable(); // èâä˙âªòRÇÍëŒçÙ
        }

        sceneToBgmIdMap.TryGetValue(sceneName, out var bgmId);
        return bgmId;
    }
}
