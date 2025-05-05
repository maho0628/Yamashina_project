using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SongItemUI : MonoBehaviour
{
    [SerializeField] private Image jacketImage;
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private Button selectButton;

    private string songId;
    public UIObjectPool<SongItemUI> Pool { get; set; }

    private void Awake()
    {
        if (jacketImage == null)
            jacketImage = transform.Find("BgmJacketImage")?.GetComponent<Image>();

        if (titleText == null)
            titleText = transform.Find("BGMName")?.GetComponent<TextMeshProUGUI>();

        if (selectButton == null)
            selectButton = GetComponent<Button>();

        selectButton.onClick.AddListener(OnSelectButtonClicked);
    }

    public void Setup(BGMConfig config)
    {
        if (config == null) return;

        songId = config.BgmId;
        titleText.text = config.BgmDisplayName;
        jacketImage.sprite = config.BgmJacketImage;
    }

    private void OnSelectButtonClicked()
    {
        Debug.Log($"[SongItemUI] ‘I‘ð‚³‚ê‚½‹È: {songId}");
    }

    public void Deactivate()
    {
        Pool?.Return(this);
    }
}
