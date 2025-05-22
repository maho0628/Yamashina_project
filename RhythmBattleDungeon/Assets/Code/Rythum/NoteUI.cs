using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class NoteUI : MonoBehaviour, IPoolable<NoteUI>
{
    [SerializeField] private RectTransform rectTransform;

    private float spawnTime;
    private float targetTime;
    private float scrollDuration;
    private Vector2 startPosition;
    private Vector2 endPosition;
    private Note linkedNote;

    private UIObjectPool<NoteUI> pool;

    public void Setup(float targetTime, float scrollDuration, Vector2 startPos, Vector2 endPos, Note note)
    {
        this.spawnTime = Time.time;
        this.targetTime = targetTime;
        this.scrollDuration = scrollDuration;
        this.startPosition = startPos;
        this.endPosition = endPos;
        this.linkedNote = note;

        rectTransform.anchoredPosition = startPos;
    }
    public Note GetLinkedNote() => linkedNote;

    private void Update()
    {
        float currentBgmTime = AudioManager.Instance.GetCurrentBGMTime();

        float spawnAt = targetTime - scrollDuration;
        if (currentBgmTime < spawnTime)
        {
            return;
        }
        float t = Mathf.Clamp01((currentBgmTime - spawnAt) / scrollDuration);

        rectTransform.anchoredPosition = Vector2.Lerp(startPosition, endPosition, t);

     
        // 完全にスクロールしたら非表示にする
        if (t >= 1f)
        {
            if(linkedNote != null)
            Deactivate();
        }
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
        pool?.Return(this);
    }
    public Vector2 GetPosition()
    {
        return rectTransform.anchoredPosition;
    }

    public void OnCreated(UIObjectPool<NoteUI> pool)
    {
        this.pool = pool;
    }
}
