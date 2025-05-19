using UnityEngine;
using UnityEngine.UI;

public class NoteManager : SingletonMonoBehaviour<NoteManager>
{
    [SerializeField] private NoteUIPool notePool;
    [SerializeField] private RectTransform parentRect; // Canvas配下の親RectTransform
    private ChartData chartData;

    [SerializeField] private Image[] laneImages;
    [SerializeField] private int laneCount = 4; // 例えば、4レーン

    private NoteScrollConfig scrollConfig;      
    
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
        if (!StageManager.Instance.IsStageSelected)
        {
            var testTable = Resources.Load<StageConfigTable>("ScriptableObject/stageConfig");
            StageManager.Instance.SetupStage(testTable, "test");
        }
        
        string chartFileName = StageManager.Instance.GetCurrentChartFileName();
        if (string.IsNullOrEmpty(chartFileName))
        {
            Debug.LogWarning("[NoteManager] ChartFileName が設定されていません。シーン遷移前に SetupStage が呼ばれていない可能性があります。");
            return;
        }
        scrollConfig = StageManager.Instance.GetCurrentStageConfig()?.ScrollConfig;

        if (scrollConfig == null)
        {
            Debug.LogError("[NoteManager] scrollConfig が null！ステージの初期化が間に合ってないかも！");
            return;
        }
        chartData = ChartJsonLoader.LoadChartData(chartFileName);
        if (chartData == null)
        {
            Debug.LogError("[NoteManager] チャートデータの読み込みに失敗しました");
            return;
        }

        SetupLaneImages();

        SpawnNotes();
    }
    public void SpawnNotes()
    {
        if (chartData == null || chartData.Notes == null) return;

        foreach (var note in chartData.Notes)
        {
            SpawnNote(note.SpawnTime, note.LaneNumber);  // ノーツの出現タイミングとレーンでスポーン
        }
    }



    public void SpawnNote(float offsetTime, int lane)
    {
        if (lane < 0 || lane >= laneCount)
        {
            Debug.LogWarning($"[NoteManager] 無効なレーン番号: {lane}");
            return;
        }

        float totalWidth = parentRect.GetComponent<RectTransform>().rect.width;
        float laneWidth = totalWidth / laneCount;
        float startX = -totalWidth / 2f + laneWidth / 2f + lane * laneWidth;

        Vector2 startPos = new Vector2(startX, scrollConfig.StartY);
        Vector2 endPos = new Vector2(startX, scrollConfig.EndY);

        var note = notePool.Get();
        note.transform.SetParent(parentRect, false);

        // Setup に必要な情報を渡す
        note.Setup(offsetTime, scrollConfig.ScrollDuration, startPos, endPos);
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

    public Note GetNearestNoteInLane(int laneId, float currentTime, float hitWindow)
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
