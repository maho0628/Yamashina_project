using TMPro;
using UnityEngine;

public class ResultUIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI perfectText;
    [SerializeField] private TextMeshProUGUI greatText;
    [SerializeField] private TextMeshProUGUI missText;

    public void ShowResult(int score, int perfect, int great, int miss)
    {
        scoreText.text = $"Score: {score}";
        perfectText.text = $"Perfect: {perfect}";
        greatText.text = $"Great: {great}";
        missText.text = $"Miss: {miss}";

        gameObject.SetActive(true);
    }
}
