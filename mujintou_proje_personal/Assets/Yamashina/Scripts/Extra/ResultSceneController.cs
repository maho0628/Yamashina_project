using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using System.Text.RegularExpressions;

/// <summary>
/// リザルト画面を表示させるためのスクリプト
/// スクリーンショット撮影やテキスト表示などを行う
/// </summary>
public class ResultSceneController : MonoBehaviour
{

    /// <summary>
    /// 脱出したかどうかを表示するためのテキストコンポーネント
    /// </summary>
    [SerializeField, Header("脱出したかどうかを表示するためのテキストコンポーネント")]
    private Text deadOrSurviveText;

    /// <summary>
    /// 生存日数のテキスト表示
    /// </summary>
    [SerializeField, Header("生存日数のテキスト表示するためのテキストコンポーネント")]
    private Text dayText;

    /// <summary>
    /// 無人島に持ち込んだアイテム名のテキスト表示
    /// </summary>
    [SerializeField, Header("無人島に持ち込んだアイテム名を表示するためののテキストコンポーネント")]
    private Text specialItemText;

    /// <summary>
    /// 無人島に持ち込んだアイテムイメージ
    /// </summary>
    [SerializeField, Header("無人島に持ち込んだアイテムイメージコンポーネント")]
    private Image specialItemImage;

    /// <summary>
    /// スクリーンショットができたことを表すテキスト表示
    /// </summary>
    [SerializeField, Header("スクリーンショットができたことを表すテキスト表示")]
    private GameObject screenshotTextObject;

    /// <summary>
    /// SEのマネージャーを取得
    /// </summary>
    [SerializeField, Header("anotherSoundPlayerクラスのオブジェクトを入れる")]
    private anotherSoundPlayer SEAudio;

    /// <summary>
    /// フェードクラスの取得
    /// </summary>
    [SerializeField, Header("フェードのクラスを入れる")]
    private Fade fade;

    /// <summary>
    /// リザルト画面の設定のスクリプタブルオブジェクト
    /// </summary>
    [SerializeField, Header("ResultSceneSettingsのスクリプタブルオブジェクトを入れる")]
    private ResultSceneSettings sceneSettings;

    private void Awake()
    {
        //生存日数を代入
        dayText.text = PlayerInfo.Instance.Day.day.ToString() + "日";

        //プレイヤーの体力があるかどうか
        if (PlayerInfo.Instance.Health <= 0)
        {
            //体力がないので脱出失敗
            deadOrSurviveText.text = "失敗";

        }
        else
        {
            //体力があるので脱出
            deadOrSurviveText.text = "成功";
        }

        //最初に手に入れたアイテムのアイテムID番号を取得
        int ID = PlayerInfo.Instance.FirstItemId;

        //そのアイテムIDのアイテム名を取得
        string name = PlayerInfo.Instance.Inventry.GetItemName((Items.Item_ID)ID);

        //アイテム名をテキストに代入
        specialItemText.text = name;

        //最初に手に入れたアイテムのイメージアイコンを取得し代入
        specialItemImage.sprite = SlotManager.GetItemData((Items.Item_ID)PlayerInfo.Instance.FirstItemId).icon;

        //SEのコントローラーを取得
        SEAudio = GameObject.FindAnyObjectByType<anotherSoundPlayer>().GetComponent<anotherSoundPlayer>();
    }

    private void Start()
    {
        //フェードアウトインをさせる必要ないので初期化
        fade.feadout_f = false;

    }

    /// <summary>
    /// スクリーンショットを撮るための関数
    /// </summary>
    public IEnumerator Capture(string imageName = "image.png", Action callback = null)
    {
        DateTime date = DateTime.Now;
        imageName = date.ToString("yyyy-MM-dd-HH-mm-ss-fff");

        string path = $"{sceneSettings.FolderName}/{imageName}.png";
        string imagePath = Path.Combine(Application.streamingAssetsPath, path);

#if !UNITY_EDITOR && (UNITY_IOS || UNITY_ANDROID)
            imagePath = Path.Combine(Application.persistentDataPath, imageName);
#endif

        if (File.Exists(imagePath))
            File.Delete(imagePath);

        ScreenCapture.CaptureScreenshot(imagePath);

        float latency = 0;
        while (latency < sceneSettings.SaveTimeOut)
        {
            if (File.Exists(imagePath))
                break;
            latency += Time.deltaTime;
            yield return null;
        }

        if (latency >= sceneSettings.SaveTimeOut)
            Debug.LogWarning($"スクリーンショット保存がタイムアウト（{sceneSettings.SaveTimeOut}秒）");

        callback?.Invoke();
    }

    /// <summary>
    /// タイトル画面に戻るための関数
    /// </summary>
    public void ReToTitle()
    {

        SEAudio.ChooseSongs_SE(0);

        Invoke(nameof(ResetTitle), sceneSettings.ReturnToTitleDelay);
    }

    //実際にインスペクター上で撮影ボタンに設定している関数
    public void CaptureButtton()
    {
        SEAudio.ChooseSongs_SE(0);

        StartCoroutine(
            Capture("Screenshot.png", Callback)
        );

        GameObject.FindGameObjectWithTag("ClickEffect")?.SetActive(false);
    }
    public void ResetTitle()
    {


        //プレイヤーの破壊をここに移動//
        if (PlayerInfo.InstanceNullable)
        {
            PlayerInfo.Instance.DestroySelf();
        }
        DataManager.ErasePlayerSaveData();


        fade.feadout_f = true;

    }
    //撮影完了時に実行される（撮影しましたのテキスト表示）
    private void Callback()
    {
        Debug.Log("撮影完了");
        screenshotTextObject.SetActive(true);
    }
}





