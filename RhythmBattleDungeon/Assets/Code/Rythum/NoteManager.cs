using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class NoteManager : SingletonMonoBehaviour<NoteManager>
{
    [SerializeField] private NoteUIPool notePool;
    [SerializeField] private RectTransform parentRect; // Canvas配下の親RectTransform
    private ChartData chartData;
    public event Action OnNotesSpawned;

    [SerializeField] private Image[] laneImages;

    private NoteScrollConfig scrollConfig;

    private bool notesSpawned = false;

    public bool NotesSpawned => notesSpawned;

    public event Action OnInitialized; // ← 新しいイベント
    private bool isInitialized = false;

    private int totalNoteCount ;

    internal int TotalNoteCount =>totalNoteCount;

    private void SetupLaneImages()
    {
        int laneCount = laneImages.Length;

        if (laneCount == 0)
        {
            Debug.LogError("[NoteManager] laneImages が空です。");
            return;
        }

        float totalWidth = parentRect.rect.width;
        float laneWidth = totalWidth / laneCount;
        float startX = -totalWidth / 2f + laneWidth / 2f;

        // scrollConfig.StartY を使用してレーンの開始位置（Y軸）を調整
        float startYPosition = scrollConfig.StartY; // 上からの位置
        float endYPosition = scrollConfig.EndY;     // 下からの位置

        for (int i = 0; i < laneCount; i++)
        {
            RectTransform laneRect = laneImages[i].rectTransform;
            // レーンの画像を設定
            Sprite laneSprite = scrollConfig.GetLaneSprite(i);
            if (laneSprite != null)
            {
                laneImages[i].sprite = laneSprite; // 画像を設定
            }
            // レーンの色を設定
            laneImages[i].color = scrollConfig.GetLaneColor(i);
            // レーンの Y 座標（startYと endYの間で調整）
            // Y位置を調整するため、scrollConfig.endYPosition を使って配置
            laneRect.anchoredPosition = new Vector2(startX + i * laneWidth, endYPosition);

            // 必要に応じてサイズを調整
            laneRect.sizeDelta = new Vector2(laneWidth, scrollConfig.LaneHeight);
        }
    }



    private void Start()
    {
        StartCoroutine(InitializeRoutine());
    }

    private IEnumerator InitializeRoutine()
    {
        // ステージ未初期化なら、テスト用ステージを強制ロード
        if (!StageManager.Instance.IsStageSelected)
        {
            var testTable = Resources.Load<StageConfigTable>("ScriptableObject/stageConfig");
            StageManager.Instance.SetupStage(testTable, "test");
        }

        string chartFileName = StageManager.Instance.GetCurrentChartFileName();
        if (string.IsNullOrEmpty(chartFileName))
        {
            Debug.LogWarning("[NoteManager] ChartFileName が設定されていません。");
            yield break;
        }

        scrollConfig = StageManager.Instance.GetCurrentStageConfig()?.ScrollConfig;
        if (scrollConfig == null)
        {
            Debug.LogError("[NoteManager] scrollConfig が null！");
            yield break;
        }

        chartData = ChartJsonLoader.LoadChartData(chartFileName);
        if (chartData == null)
        {
            Debug.LogError("[NoteManager] チャートデータの読み込みに失敗しました");
            yield break;
        }

        totalNoteCount = chartData?.Notes?.Length ?? 0;

        SetupLaneImages();
        SpawnNotes();

        isInitialized = true;
        OnInitialized?.Invoke(); // ← 初期化完了を通知
    }
    public void SpawnNotes()
    {
        if (chartData == null || chartData.Notes == null) return;

        foreach (var note in chartData.Notes)
        {
            SpawnNote(note);  // ノーツの出現タイミングとレーンでスポーン
        }
        notesSpawned = true; // フラグを立てる

        Debug.Log(OnNotesSpawned);
        OnNotesSpawned?.Invoke();
        Debug.Log("Invoke すべてのノーツ");
    }



    public void SpawnNote(Note noteData)
    {
        int lane = noteData.LaneNumber;
        float offsetTime = noteData.SpawnTime;

        if (lane < 0 || lane >= scrollConfig.LaneCount)
        {
            Debug.LogWarning($"[NoteManager] 無効なレーン番号: {lane}");
            return;
        }

        float totalWidth = parentRect.rect.width;
        float laneWidth = totalWidth / scrollConfig.LaneCount;
        float startX = -totalWidth / 2f + laneWidth / 2f + lane * laneWidth;

        Vector2 startPos = new Vector2(startX, scrollConfig.StartY);
        Vector2 endPos = new Vector2(startX, scrollConfig.EndY);

        var noteUI = notePool.Get();
        noteUI.transform.SetParent(parentRect, false);

        // noteData を NoteUI に渡す
        noteUI.Setup(offsetTime, scrollConfig.ScrollDuration, startPos, endPos, noteData);
    }

    public RectTransform GetNoteParentTransform()
    {
        Debug.Log($"parentRect:{parentRect}");
        return parentRect;

    }
    public Note GetNearestNoteByAction(string actionName, float currentTime, float maxJudgementTime)
    {
        int laneIndex = GetLaneIndexFromAction(actionName);
        if (laneIndex < 0) return null;

        return GetNearestNoteInLane(laneIndex, currentTime, maxJudgementTime);
    }

    private int GetLaneIndexFromAction(string actionName)
    {
        if (actionName.StartsWith("Lane"))
        {
            string numberStr = actionName.Substring("Lane".Length);
            if (int.TryParse(numberStr, out int num))
            {
                int index = num - 1;
                if (index >= 0 && index < scrollConfig.LaneCount)
                    return index;
            }
        }
        Debug.LogWarning($"[NoteManager] 無効なアクション名: {actionName}");
        return -1;
    }

    public int GetTotalNoteCount()
    {
        return totalNoteCount;  
    }
    public void ClearAllNotes()
    {
        foreach (Transform child in parentRect)
        {
            if (child.TryGetComponent<NoteUI>(out var note))
            {
                note.Deactivate();
            }
        }
    }
    public Vector2 GetLanePosition(int lane)
    {
        float totalWidth = parentRect.rect.width;
        float laneWidth = totalWidth / scrollConfig.LaneCount;
        float startX = -totalWidth / 2f + laneWidth / 2f + lane * laneWidth;
        return new Vector2(startX, scrollConfig.EndY);
    }

    private Note GetNearestNoteInLane(int laneId, float currentTime, float hitWindow)
    {
        if (chartData?.Notes == null) return null;

        Note nearestNote = null;
        float smallestDiff = hitWindow;

        foreach (var note in chartData.Notes)
        {
            if (note.LaneNumber != laneId || note.IsHit) continue;

            float diff = Mathf.Abs(note.SpawnTime - currentTime);
            if (diff < smallestDiff)
            {
                smallestDiff = diff;
                nearestNote = note;
            }
        }
        return nearestNote;
    }
}
