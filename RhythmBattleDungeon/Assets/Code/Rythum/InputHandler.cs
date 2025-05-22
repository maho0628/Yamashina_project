using UnityEngine;
using UnityEngine.InputSystem;


public class InputHandler : MonoBehaviour
{
    private PlayerInputActions inputActions;
    private InputAction[] laneInputs;

    private int laneCount;
    private float maxJudgementTime; // 最も緩い判定の時間差（Miss 判定）

    private void Awake()
    {
        inputActions = new PlayerInputActions();
        inputActions.Gameplay.Enable();

        laneCount = StageManager.Instance.GetCurrentStageConfig()?.ScrollConfig?.LaneCount ?? 4;

        laneInputs = new InputAction[laneCount];

        for (int i = 0; i < laneCount; i++)
        {
            string actionName = $"Lane{i + 1}";
            Debug.Log(actionName);  
            laneInputs[i] = inputActions.FindAction(actionName);
            if (laneInputs[i] != null)
            {
                Debug.Log(laneInputs[i]+ "laneInputs");   
                string actionCopy = actionName; // クロージャ対応
                laneInputs[i].performed += ctx => TryHitNoteByAction(actionCopy);
            }
            else
            {
                Debug.LogWarning($"[InputHandler] アクション {actionName} が見つかりません");
            }
        }

    }

    public void Initialize()
    {
        maxJudgementTime = JudgementManager.Instance.GetMaxJudgementTime();
    }

    private void TryHitNoteByAction(string actionName)
    {
        float currentTime = AudioManager.Instance.GetCurrentBGMTime();
        var note = NoteManager.Instance.GetNearestNoteByAction(actionName, currentTime, maxJudgementTime);

        if (note == null) return;

        float diff = Mathf.Abs(note.SpawnTime - currentTime);
        var judgement = JudgementManager.Instance.GetJudgement(diff);
        Debug.Log($"判定: {judgement.JudgementName} (差分: {diff})");

        if (judgement != null)
        {
            note.IsHit = true;
            int score = judgement.ScoreValue; 

            ScoreManager.Instance.AddScore(score);

            if (judgement.BreaksCombo)
            {
                ComboManager.Instance.ResetCombo(); 
            }
            else
            {
                ComboManager.Instance.IncrementCombo();
                // 対応する NoteUI を削除
                foreach (Transform child in NoteManager.Instance.GetNoteParentTransform())
                {
                    if (child.TryGetComponent<NoteUI>(out var ui))
                    {
                        if (ui.GetLinkedNote() == note)
                        {
                            ui.Deactivate();
                            break;
                        }
                    }
                }
            }
                
        }
    }


 

    private void OnDestroy()
    {
        foreach (var action in laneInputs)
        {
            if (action != null)
                action.Disable();
        }
    }
}
