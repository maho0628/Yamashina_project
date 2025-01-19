using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TrueEndSceneController : MonoBehaviour
{
    [SerializeField]
    TextControl textControl;
    [SerializeField]
    SceneObject title_scene;
    [SerializeField]
    Fading fading;

    // Start is called before the first frame update
    void Start()
    {
        textControl.ClickEventAfterTextsEnd.AddListener(TextEnd);
        PlayerInfo.Instance.gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void TextEnd()
    {
        fading.Fade(Fading.type.FadeOut);
        fading.OnFadeEnd.AddListener(() => {
            PlayerInfo.Instance.gameObject.transform.GetChild(0).gameObject.SetActive(true);
            SceneManager.LoadScene(title_scene); 

        });
    }
}
