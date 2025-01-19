using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable]
public class SelectLocationButtonInfo
{
    public Button button;
    public Sprite on_hover_sprite;
    public Sprite on_unhover_sprite;
    public Sprite locking_sprite;
    public int necessary_action_value;
    public SceneObject distination;
}

public class SetLocationPanelSystem : MonoBehaviour
{
    [SerializeField]
    List<SelectLocationButtonInfo> button_infos;
    [SerializeField]
    Button depature_button;
    [SerializeField]
    GameObject depature_text;@//ŽÄ“c’Ç‰Á•Ï”
    [SerializeField]
    Fading fading;

    SceneObject selectedLocation;

    private void Awake()
    {
        foreach (var info in button_infos)
        {
            info.button.onClick.AddListener(() => OnButtonSelected(info));
        }

        depature_button.onClick.AddListener(() =>
        {
            fading.Fade(Fading.type.FadeOut);
            fading.OnFadeEnd.AddListener(() => SceneManager.LoadScene(selectedLocation));
        });

        
    }

    private void Start()
    {
        PlayerInfo.Instance.OnMaxActionValueChange.AddListener(UpdateButtonState);
    }

    public void UpdateButtonState()
    {
        selectedLocation = null;
        depature_button.interactable = false;
        foreach (var info in button_infos)
        {
            if (PlayerInfo.Instance.MaxActionValue >= info.necessary_action_value)
            {
                info.button.image.sprite = info.on_unhover_sprite;
                info.button.interactable = true;

            }
            else
            {
                info.button.image.sprite = info.locking_sprite;
                info.button.interactable = false;
            }

        }
    }

    void OnButtonSelected(SelectLocationButtonInfo info)
    {
        UpdateButtonState();
        info.button.image.sprite = info.on_hover_sprite;
        selectedLocation = info.distination;
        //ŽÄ“c‚ªif•¶‚ð‘‚«‰Á‚¦‚Ü‚µ‚½
        if(PlayerInfo.Instance.ActionValue > 0)
        {
            depature_text.SetActive(false);
            depature_button.interactable = true;
        }
        else
        {
            depature_text.SetActive(true);
            depature_button.interactable = false;
        }
        //‚±‚±‚Ü‚Å
        
    }

    private void OnDestroy()
    {

        if (PlayerInfo.InstanceNullable)
        {
            PlayerInfo.Instance.OnMaxActionValueChange.RemoveListener(UpdateButtonState);
        }
        
        
    }
}
