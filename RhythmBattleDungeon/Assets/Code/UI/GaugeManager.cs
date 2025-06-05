using UnityEngine;



public class GaugeManager : SingletonMonoBehaviour<GaugeManager>    
{

    [SerializeField] private GaugeConfig defaultConfig;
    private GaugeConfig currentConfig;

    protected override void Awake()
    {
        base.Awake();   

        currentConfig = defaultConfig;
    }

    public void ApplyStageGaugeConfig(GaugeConfig config)
    {
        currentConfig = config;
    }

    public GaugeConfig GetCurrentConfig() => currentConfig;
}

