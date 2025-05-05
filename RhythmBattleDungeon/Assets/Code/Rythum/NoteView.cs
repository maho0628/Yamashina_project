using UnityEngine;

public class NoteView : MonoBehaviour
{
    private float spawnTime;
    private float targetTime;
    private float scrollDuration;
    private RectTransform rectTransform;
    private Vector2 startPosition;
    private Vector2 endPosition;

    public void Initialize(float targetTime, float scrollDuration, Vector2 startPos, Vector2 endPos)
    {
        this.spawnTime = Time.time;
        this.targetTime = targetTime;
        this.scrollDuration = scrollDuration;
        this.startPosition = startPos;
        this.endPosition = endPos;

        rectTransform = GetComponent<RectTransform>();
        rectTransform.anchoredPosition = startPos;
    }

    void Update()
    {
        float t = (Time.time - spawnTime) / scrollDuration;
        rectTransform.anchoredPosition = Vector2.Lerp(startPosition, endPosition, t);

        // ƒm[ƒc‚ª’Ê‰ß‚µI‚¦‚½‚çíœ
        if (t > 1f)
            Destroy(gameObject);
    }
}
