using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;

/// <summary>
/// ゲームスタートの演出を管理するシングルトンクラス
/// </summary>
public class UIManager : SingletonMonoBehaviour<UIManager>
{

    #region ゲームスタート演出の内部管理用変数（インスペクター設定）

    /// <summary>
    /// ReadyGoを表示させるプレハブ
    /// </summary>
    [Header("ゲームスタート演出設定")]
    [SerializeField, Tooltip("ゲームスタート演出設定")]
    private GameObject readyGoPanelPrefab;

    [Space(15)]

    /// <summary>
    /// Ready/Go 演出の詳細設定"
    /// </summary>
    [SerializeField, Tooltip("Ready/Go 演出の詳細設定")]
    private StartSignalConfig startSignalConfig;

    [Space(15)]

    /// <summary>
    /// ReadyGo パネルを生成する対象の Canvas
    /// 未設定時は自動取得
    /// </summary>
    [SerializeField, Tooltip("ReadyGo パネルを生成する対象の Canvas")]
    private Canvas targetCanvas;


    #endregion


    #region ReadyGo演出制御の内部管理用変数（実行時制御）

    /// <summary>
    /// 実行時に生成されるインスタンス
    /// </summary>
    private GameObject readyGoPanelInstance;

    /// <summary>
    /// 実際に表示するためのTextMeshProUGUI
    /// </summary>
    private TextMeshProUGUI readyGoText;

    #endregion

    #region 外部で呼び出し可能な関数(ReadyGo演出の制御)

    /// <summary>
    /// Ready → Go の演出を非同期で再生する
    /// </summary>
    /// <returns>非同期処理の完了を待つUniTask</returns>
    internal async UniTask ShowReadyGoAsync()
    {

        //ReadyとGoの待機時間を取得
        float intervalBetweenReadyGo = startSignalConfig.IntervalBetweenReadyGo;

        // キャンバスが入っていなければ自動取得
        if (targetCanvas == null)
            targetCanvas = GameObject.Find("ReadyGoPanelCanvas").GetComponent<Canvas>();

        // インスタンスが無ければ演出を生成
        if (readyGoPanelInstance == null)
            CreateReadyGoPanel();

        // 演出を表示させる
        readyGoPanelInstance.SetActive(true);

        // Ready演出（共通クラスを使用）
        await TextAnimationPlayer.PlayTextAnimationAsync(readyGoText, startSignalConfig.ReadyConfig);

        // ReadyとGoの間にウェイトを挟む
        if (intervalBetweenReadyGo > 0)
        {
            await UniTask.Delay(System.TimeSpan.FromSeconds(intervalBetweenReadyGo));
        }

        // Go演出（共通クラスを使用）
        await TextAnimationPlayer.PlayTextAnimationAsync(readyGoText, startSignalConfig.GoConfig);

        //演出が終了したので、非表示に
        readyGoPanelInstance.SetActive(false);

    }

    /// <summary>
    /// ReadyGo演出を強制停止する（DOTweenアニメーションも停止）
    /// </summary>
    internal void StopReadyGo()
    {
        // 共通クラスでアニメーション停止
        TextAnimationPlayer.StopAnimation(readyGoText);

        if (readyGoPanelInstance != null)
            // パネル非表示
            readyGoPanelInstance.SetActive(false);
    }

    #endregion

    #region プライベート関数（ReadyGoパネル生成関連）

    /// <summary>
    /// ReadyGoパネルをCanvas上に生成し、Textコンポーネントを取得する
    /// </summary>
    private void CreateReadyGoPanel()
    {
        //プレハブに何もないなら処理しない
        if (readyGoPanelPrefab == null)
        {
            DebugManager.LogError($"{readyGoPanelPrefab}が入っていません");
            return;
        }

        // プレハブからインスタンス生成
        readyGoPanelInstance = Instantiate(readyGoPanelPrefab, targetCanvas.transform);

        // Text コンポーネントを取得
        readyGoText = readyGoPanelInstance.GetComponentInChildren<TextMeshProUGUI>();

        //Text コンポーネントがないなら
        if (readyGoText == null)
        {
            DebugManager.LogError("ReadyGoPanel内にTextMeshProUGUIコンポーネントが見つかりません！");
        }

        // 初期状態は非表示
        readyGoPanelInstance.SetActive(false);
    }

    #endregion
   
    private void OnDestroy()
    {
        //演出を中止
        StopReadyGo();
    }
}