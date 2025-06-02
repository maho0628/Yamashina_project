using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResultUIManager : MonoBehaviour
{
    [SerializeField] private GameObject judgementTextPrefab;
    [SerializeField] private GameObject comboTextPrefab;

    [SerializeField] private Transform judgeParentTransform;
    [SerializeField] private Transform comboParentTransform;

    private Dictionary<string, IResultEntryUI> activeJudgementUIs = new();
    private Dictionary<string, IResultEntryUI> activeComboUIs = new();

    void Start()
    {
        SetUpResultUI();
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
        }
        else
        {
            Debug.LogWarning($"[ResultUIManager] {prefab.name} に IResultEntryUI がアタッチされていません。");
        }
    }

    void SetUpResultUI()
    {
        foreach (var config in JudgementManager.Instance.GetAllJudgements())
        {
            string label = config.Logic.JudgementName;
            int count = JudgementManager.Instance.GetJudgementCount(label);
            CreateResultEntry(judgementTextPrefab, judgeParentTransform, label, count, activeJudgementUIs);
        }

        string comboLabel = ResultLabels.MaxCombo;
        int comboValue = ComboManager.Instance.MaxCombo;
        CreateResultEntry(comboTextPrefab, comboParentTransform, comboLabel, comboValue, activeComboUIs);
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
}
