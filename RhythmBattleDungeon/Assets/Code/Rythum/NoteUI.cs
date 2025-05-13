using UnityEngine;

public class NoteUI : MonoBehaviour, IPoolable<NoteUI>
{
    [SerializeField] private RectTransform rectTransform;

    private float spawnTime;
    private float targetTime;
    private float scrollDuration;
    private Vector2 startPosition;
    private Vector2 endPosition;

    private UIObjectPool<NoteUI> pool;

    public void Setup(float targetTime, float scrollDuration, Vector2 startPos, Vector2 endPos)
    {
        this.spawnTime = Time.time;
        this.targetTime = targetTime;
        this.scrollDuration = scrollDuration;
        this.startPosition = startPos;
        this.endPosition = endPos;

        rectTransform.anchoredPosition = startPos;
    }

    private void Update()
    {
        // 現在の BGM 時間を取得
        float currentBgmTime = AudioManager.Instance.GetCurrentBGMTime();

        // ノーツがヒットするタイミングまでの経過時間
        float timeSinceSpawn = currentBgmTime - targetTime;

        // ノーツの位置を計算（時間ベースで滑らかに移動）
        float t = Mathf.Clamp01(timeSinceSpawn / scrollDuration);

        // Lerp を使って位置を計算
        rectTransform.anchoredPosition = Vector2.Lerp(startPosition, endPosition, t);

        // 完全にスクロールしたら非表示にする
        if (t >= 1f)
        {
            Deactivate();
        }
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
        pool?.Return(this);
    }

    public void OnCreated(UIObjectPool<NoteUI> pool)
    {
        this.pool = pool;
    }
}
