using TMPro;
using UnityEngine;

public class InGameUIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI perfectText;
    [SerializeField] private TextMeshProUGUI greatText;
    [SerializeField] private TextMeshProUGUI missText;

    private void Update()
    {
        scoreText.text = $"Score: {ScoreManager.Instance.GetCurrentScore()}";
        perfectText.text = $"Perfect: {JudgementManager.Instance.GetJudgementCount("Perfect")}";
        greatText.text = $"Great: {JudgementManager.Instance.GetJudgementCount("Great")}";
        missText.text = $"Miss: {JudgementManager.Instance.GetJudgementCount("Miss")}";
    }
}

