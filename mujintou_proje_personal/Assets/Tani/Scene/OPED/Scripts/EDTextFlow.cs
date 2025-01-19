using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Localization.Settings;
using System.Threading.Tasks;

public class EDTextFlow : MonoBehaviour
{
    List<TextAsset> textAssets;
    [SerializeField]
    List<Sprite> image_list;
    [SerializeField]
    Text text_obj;
    [SerializeField]
    TextControl textControl;
    [SerializeField]
    Image image_obj;
    [SerializeField]
    Image tall_image_obj;
    [SerializeField]
    Fading fade;
    [SerializeField]
    GameObject TextBG;
    [SerializeField]
    Image last_text;
    [SerializeField]
    SceneObject title;
    [SerializeField]
    GameObject bgmAudio;
    // 追加：LocalizedTextAssetLoader
    [SerializeField] LocalizedTextAssetLoader localizedTextAssetLoader;

    private void Awake()
    {
        fade.Fade(Fading.type.FadeOut);

        Debug.Log(PlayerInfo.Instance.Day.day);
        textControl = GameObject.FindAnyObjectByType<TextControl>().GetComponent<TextControl>();
        Debug.Log("OPTextControl Start called");
        textControl.ResetTextData();
        textControl.ClickEventAfterTextsEnd.RemoveAllListeners();
        localizedTextAssetLoader = GameObject.FindAnyObjectByType<LocalizedTextAssetLoader>();
        LoadLocalizedTextAssets();
        AddTextDataToTextControl(0);
        //インベントリ開いたままtrueEndに飛ぶと開きっぱなしになるみたいなので加えました（柴田）
        var player = GameObject.FindAnyObjectByType<PlayerInfo>();
        player.Inventry.SetVisible(false);
        ///ここまで///
        GameData.CanReturnTitle = false;
        //if (PlayerInfo.InstanceNullable)
        //{
        //    PlayerInfo.Instance.DestroySelf();
        //}
        //DataManager.ErasePlayerSaveData();
        tall_image_obj.gameObject.SetActive(false);
        TextBG.SetActive(false);
        fade.fading_time = 3f;
        fade.Fade(Fading.type.FadeIn);
        fade.OnFadeEnd.AddListener(() =>
        {
            TextBG.SetActive(true);
            fade.fading_time = 0.5f;
            AddTextDataToTextControl(0);
            textControl.ClickEventAfterTextsEnd.AddListener(() =>
            {
                textControl.ClickEventAfterTextsEnd.RemoveAllListeners();
                fade.Fade(Fading.type.FadeOut);
                fade.OnFadeEnd.AddListener(() =>
                {
                    StartCoroutine(f1());
                });
            });
        });
    }

    IEnumerator f1()
    {
        image_obj.sprite = image_list[1];
        fade.Fade(Fading.type.FadeIn);
        //追加
        bgmAudio.GetComponent<anotherBGMPlayer>().ChangeBGM(0);
        //
        yield return null;
        fade.OnFadeEnd.AddListener(() =>
        {
            AddTextDataToTextControl(1);
            textControl.ClickEventAfterTextsEnd.AddListener(() =>
            {
                textControl.ClickEventAfterTextsEnd.RemoveAllListeners();
                fade.Fade(Fading.type.FadeOut);
                fade.OnFadeEnd.AddListener(() =>
                {
                    StartCoroutine(f2());
                });
            });
        });
    }

    IEnumerator f2()
    {
        tall_image_obj.gameObject.SetActive(true);
        tall_image_obj.sprite = image_list[2];
        fade.Fade(Fading.type.FadeIn);
        yield return null;
        fade.OnFadeEnd.AddListener(() =>
        {

            AddTextDataToTextControl(2);
            textControl.ClickEventAfterTextsEnd.RemoveAllListeners();
            textControl.ClickEventAfterTextsEnd.AddListener(() =>
            {
                StartCoroutine(f3());
            });

        });
    }

    IEnumerator f3()
    {
        yield return null;
        fade.Fade(Fading.type.FadeOut);
        fade.OnFadeEnd.AddListener(() =>
        {
            fade.Fade(Fading.type.FadeIn);
            StartCoroutine(f4());
        });
    }

    IEnumerator f4()
    {
        yield return null;
        fade.OnFadeEnd.AddListener(() =>
        {
            tall_image_obj.sprite = image_list[3];
            AddTextDataToTextControl(3);
            textControl.ClickEventAfterTextsEnd.RemoveAllListeners();
            textControl.ClickEventAfterTextsEnd.AddListener(() =>
            {
                textControl.ClickEventAfterTextsEnd.RemoveAllListeners();
                StartCoroutine(move());
            });
           
        });
    }
    IEnumerator move()
    {
        yield return new WaitForSeconds(3f);
        TextBG.SetActive(false);
        while (true)
        {
            tall_image_obj.gameObject.transform.Translate(
                new Vector3(0, -200 * Time.deltaTime, 0));
            if(tall_image_obj.gameObject.transform.position.y <= -720)
            {
                break;
            }
            yield return null;
        }

        last_text.gameObject.SetActive(true);
        float alpha = 0;
        while (true)
        {
            var color = last_text.color;
            color.a = alpha;
            alpha += Time.deltaTime / 3.0f;
            last_text.color = color;
            if (color.a >= 1) break;
            yield return null;
        }
        var loaded = SceneManager.LoadSceneAsync(title);
        loaded.allowSceneActivation = false;
        Debug.Log(PlayerInfo.Instance.Day.day);


        yield return new WaitForSeconds(5f);
      
        fade.fading_time = 3f;

        fade.Fade(Fading.type.FadeOut);
        fade.OnFadeEnd.AddListener(() =>
        {
            fade.Fade(Fading.type.FadeIn);

            loaded.allowSceneActivation = true;
        });
    }

    public async void ChangeLanguage(string locale)
    {
        await ChangeSelectedLocale(locale);
        Debug.Log(locale);
    }

    private async Task ChangeSelectedLocale(string locale)
    {
        var selectedLocale = LocalizationSettings.AvailableLocales.Locales.Find(l => l.Identifier.Code == locale);
        if (selectedLocale != null)
        {
            LocalizationSettings.SelectedLocale = selectedLocale;
            await LocalizationSettings.InitializationOperation.Task;
        }
        else
        {
            Debug.LogError($"Locale '{locale}' not found.");
        }
    }
    public void ReloadLocalizedText()
    {
        Debug.Log("ReloadLocalizedText called");

        textControl.ResetTextData();
        AddTextDataToTextControl(0); // ここでテキストを再読み込みします
    }
    private void LoadLocalizedTextAssets()
    {

        textAssets = localizedTextAssetLoader.LoadTextAssetsForCurrentLocale();
        AddTextDataToTextControl(0);

    }
    void AddTextDataToTextControl(int index)
    {
        if (index > textAssets.Count)
        {
            Debug.LogError("index out of Range int textAssets");
            return;
        }

        textControl.ResetTextData();
        textControl.EndEvent.RemoveAllListeners();
        textControl.ClickEventAfterTextsEnd.RemoveAllListeners();

        string rawData = textAssets[index].text;
        string[] splitedText = rawData.Split(char.Parse("\n"));
        foreach (var text in splitedText)
        {
            if (text == "") continue;
            textControl.AddTextData(text.Replace("**", "\n"));
        }
    }


}
