using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BaseLocationNightController : MonoBehaviour
{
    [SerializeField]
    List<PanelBase> panels;
    [SerializeField]
    SceneObject next_base_location;

    // Start is called before the first frame update
    void Start()
    {
        var fade = ((Fading)GameObject.FindAnyObjectByType(typeof(Fading)));
        fade.Fade(Fading.type.FadeIn);
        var areaNameShow = (AreaNameText)GameObject.FindAnyObjectByType(typeof(AreaNameText));
        IEnumerator coroutine = areaNameShow.ShowAreaText();
        fade.OnFadeEnd.AddListener(() => { StartCoroutine(coroutine); });
    }

    // Update is called once per frame
    void Update()
    {
        //  print(PlayerInfo.Instance.Inventry.GetSlotItem(0).Value.id);
    }

    public void ActivatePanelSingle(int index)
    {
        if (index >= panels.Count) return;
        DeactivateAllPanels();
        panels[index].SetEnabled(true);
    }

    public void ActivatePanelAdditive(int index)
    {
        if (index >= panels.Count) return;
        panels[index].SetEnabled(true);
    }

    public void DeactivatePanel(int index)
    {
        if (index >= panels.Count) return;
        panels[index].SetEnabled(false);
    }

    public void DeactivateAllPanels()
    {
        for (int i = 0; i < panels.Count; i++)
        {
            panels[i].SetEnabled(false);
        }
    }

    public void SwitchActive(int index)
    {
        if (index >= panels.Count) return;
        panels[index].SwitchEnabaled();
    }



    public void ChangeBaseLocation()
    {
        var fade = ((Fading)GameObject.FindAnyObjectByType(typeof(Fading)));
        fade.Fade(Fading.type.FadeOut);
        fade.OnFadeEnd.AddListener(() => { SceneManager.LoadScene(next_base_location); });
        
    }

    public void ChangeScene()
    {

    }
}
