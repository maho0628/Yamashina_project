using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class NoteUI : MonoBehaviour, IPoolable<NoteUI>
{
    [SerializeField] private RectTransform rectTransform;

   
    private float targetTime;
    private float scrollDuration;
    private Vector2 startPosition;
    private Vector2 endPosition;
    private Note linkedNote;

    private UIObjectPool<NoteUI> noteUIPool;

    public void Setup(float targetTime, float scrollDuration, Vector2 startPos, Vector2 endPos, Note note)
    {
        
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
        float spawnAt = Mathf.Max(0f, targetTime - scrollDuration);

        if (currentBgmTime < spawnAt)
        {
            return;
        }


        float t = Mathf.Clamp01((currentBgmTime - spawnAt) / scrollDuration);
        rectTransform.anchoredPosition = Vector2.Lerp(startPosition, endPosition, t);

       
        if (t >= 1f )
        {
            if(linkedNote != null)
            Deactivate();
         

        }
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
        noteUIPool?.Return(this);
        linkedNote = null; 
    }

    public Vector2 GetPosition()
    {
        return rectTransform.anchoredPosition;
    }
  

    public void OnCreated(UIObjectPool<NoteUI> pool)
    {
        this.noteUIPool = pool;
    }
}
