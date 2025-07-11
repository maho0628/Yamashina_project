using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResultUIManager : MonoBehaviour
{
    [SerializeField] private GameObject judgementTextPrefab;
    [SerializeField] private GameObject comboTextPrefab;
    [SerializeField] private GameObject scoreTextPrefab;


    [SerializeField] private Transform judgeParentTransform;
    [SerializeField] private Transform comboParentTransform;
    [SerializeField] private Transform scoreParentTransform;

    [SerializeField] private Button songSelectButton;
    [SerializeField] private Button titleReturnButton;
    [SerializeField] private Animator resultAnimator;
    private Dictionary<string, IResultEntryUI> activeJudgementUIs = new();
    private Dictionary<string, IResultEntryUI> activeComboUIs = new();
    private Dictionary<string, IResultEntryUI> activeScoreUIs = new();


    void Start()
    {
        SetUpResultUI();
        AnimationManager.Instance.InitResultAnimator(resultAnimator);
    }







    private void OnSongSelectRetryClicked()
    {
        DebugManager.Log("[ResultRetryButtons] 選曲画面リトライボタンが押されました");

        SetButtonsInteractable(false);

        try
        {
            RetryManager.Instance.StartRetryFromSongSelect();
        }
        catch (System.Exception e)
        {
            DebugManager.LogError($"[ResultRetryButtons] 選曲画面リトライでエラー: {e.Message}");
            SetButtonsInteractable(true);
        }
    }

    private void OnTitleReturnClicked()
    {
        DebugManager.Log("[ResultRetryButtons] タイトル戻りボタンが押されました");

        SetButtonsInteractable(false);

        try
        {
            RetryManager.Instance.StartReturnToTitle();
        }
        catch (System.Exception e)
        {
            DebugManager.LogError($"[ResultRetryButtons] タイトル戻りでエラー: {e.Message}");
            SetButtonsInteractable(true);
        }
    }

    private void SetButtonsInteractable(bool interactable)
    {
        if (songSelectButton != null) songSelectButton.interactable = interactable;
        if (titleReturnButton != null) titleReturnButton.interactable = interactable;
    }



    private void CreateResultEntry(
        GameObject prefab,
        Transform parent,
        string label,
        int value,
        Dictionary<string, IResultEntryUI> targetDictionary)
    {
        var result = Instantiate(prefab, parent);
        if (result.TryGetComponent<IResultEntryUI>(out var entry))
        {
            entry.Setup(label, value);
            targetDictionary[label] = entry;
            DebugManager.LogWarning($"[ResultUIManager] {prefab.name} に IResultEntryUI がある。");

        }
        else
        {
            DebugManager.LogWarning($"[ResultUIManager] {prefab.name} に IResultEntryUI がアタッチされていません。");
        }
    }

    void SetUpResultUI()
    {
        //判定数
        foreach (var config in JudgementManager.Instance.GetAllJudgements())
        {
            string label = config.Logic.JudgementName;
            DebugManager.Log(label);
            int count = JudgementManager.Instance.GetJudgementCount(label);
            CreateResultEntry(judgementTextPrefab, judgeParentTransform, label, count, activeJudgementUIs);
        }
        //コンボ数
        string comboLabel = ResultLabels.MaxCombo;
        int comboValue = ComboManager.Instance.MaxCombo;
        CreateResultEntry(comboTextPrefab, comboParentTransform, comboLabel, comboValue, activeComboUIs);


        //スコア
        string scoreLabel = ResultLabels.MaxScore;
        int scoreValue = ScoreManager.Instance.GetCurrentScore();

        CreateResultEntry(scoreTextPrefab, scoreParentTransform, scoreLabel, scoreValue, activeScoreUIs);
        //各ボタンのリスナー登録
        songSelectButton.onClick.AddListener(OnSongSelectRetryClicked);
        // タイトル戻りボタン
        if (titleReturnButton != null)
        {
            titleReturnButton.onClick.AddListener(OnTitleReturnClicked);
        }
        var stageID = StageManager.Instance.GetCurrentStageBGMId();
        GameManagerRetryExtensions.SaveRetryInfo(stageID);

    }


    // 例: あとから特定の値を更新したいとき
    public void UpdateJudgementCount(string label, int newCount)
    {
        if (activeJudgementUIs.TryGetValue(label, out var ui))
        {
            ui.SetValue(newCount);
        }
    }

    public void UpdateMaxCombo(int newCombo)
    {
        if (activeComboUIs.TryGetValue(ResultLabels.MaxCombo, out var ui))
        {
            ui.SetValue(newCombo);
        }
    }
    public void UpdateScoreCount(string label, int newCount)
    {
        if (activeScoreUIs.TryGetValue(ResultLabels.MaxScore, out var ui))
        {
            ui.SetValue(newCount);
        }
    }
}
