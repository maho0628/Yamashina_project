using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using System.Text.RegularExpressions;

public class ResultSceneControler : MonoBehaviour
{



    [SerializeField]
    SceneObject title;
    [SerializeField] Text Txt_deador;
    [SerializeField] Text Txt_day;
    [SerializeField] Text Special;
    
    //[SerializeField]
    //RawImage RawImage;
    //[SerializeField] GameObject action;
    //[SerializeField]
    //GameObject ActionValueImagePrefab;
    [SerializeField]
    Image ItemImage;
    [SerializeField] public  GameObject Text_screen;
    public string gamepath;
    public string timeStamp;
    string screenShotPath;
    bool isDisplayed;
    [SerializeField] anotherSoundPlayer SEAudio;
    [SerializeField] Fade fade;
    private void Awake()
    {
        //生存日数
        Debug.Log(PlayerInfo.Instance.Day.day);
        Txt_day.text = PlayerInfo.Instance.Day.day.ToString() + "日";

        //生存かどうか
        if (PlayerInfo.Instance.Health <= 0)
        {
            Debug.Log(PlayerInfo.Instance.Health);
            Txt_deador.text = "失敗";

        }
        if (PlayerInfo.Instance.Health > 0)
        {
            Txt_deador.text = "成功";
        }

        //最初に手に入れたアイテムの名前
        Debug.Log(PlayerInfo.Instance.FirstItemId);
        int ID = PlayerInfo.Instance.FirstItemId;
        string name = PlayerInfo.Instance.Inventry.GetItemName((Items.Item_ID)ID);
        Special.text = name;

        //最初に手に入れたアイテムのイメージ
        ItemImage.sprite = SlotManager.GetItemData((Items.Item_ID)PlayerInfo.Instance.FirstItemId).icon;
        SEAudio = GameObject.FindAnyObjectByType<anotherSoundPlayer>().GetComponent<anotherSoundPlayer>();
    }

    private void Start()
    {
        //撮影したかどうか
        isDisplayed = false;
        fade.feadout_f = false;

        //どのくらいの秒数で自動でタイトル画面に戻るか
        //Invoke(nameof(ReToTitle), 50f);
    }
    public static class ScreenshotCaptor
    {

        /// <summary>
        /// スクリーンショットを撮る
        /// </summary>
        public static IEnumerator Capture(string imageName = "image.png", Action callback = null)
        {
            //実際の日付に変換
            DateTime date = DateTime.Now;
            imageName = date.ToString("yyyy-MM-dd-HH-mm-ss-fff");
             string path = "";

            // プロジェクトファイルのスクリーンショットフォルダの直下に作成
            path = "screenshot/" + imageName + ".png";
            string imagePath = Application.streamingAssetsPath + "/"+path;
            //iOS、Android実機の時はパスにApplication.persistentDataPathを追加
#if !UNITY_EDITOR && (UNITY_IOS || UNITY_ANDROID)
    imagePath = Path.Combine(Application.persistentDataPath, imageName);
#endif

            //前に撮ったスクショを削除
            File.Delete(imagePath);

            //スクリーンショットを撮る
            UnityEngine.ScreenCapture.CaptureScreenshot(imagePath);
            //スクリーンショットが保存されるまで待機(最大2秒)
            float latency = 0, latencyLimit = 2;
            while (latency < latencyLimit)
            {
                //ファイルが存在していればループ終了
                if (File.Exists(imagePath))
                {
                    break;
                }
                latency += Time.deltaTime;
                yield return null;
            }
            //待機時間が上限に達していたら警告表示(おそらくスクショが保存出来ていない時)
            if (latency >= latencyLimit)
            {
                Debug.LogWarning("待機時間が上限に達しました！正常にスクリーンショットが保存できていません！");
            }

            //コールバックが登録されていれば実行
            if (callback != null)
            {
                callback();
            }
        }
    }
    public void ReToTitle()
    {

        SEAudio.ChooseSongs_SE(0);

        Invoke(nameof(ResetTitle), 0.2f);
    }

    //実際にインスペクター上で撮影ボタンに設定している関数
    public void CaptureButtton()
    {
        SEAudio.ChooseSongs_SE(0);

        StartCoroutine(ScreenshotCaptor.Capture
            (imageName: "Screenshot.png", callback: Callback));
        GameObject.FindGameObjectWithTag("ClickEffect").SetActive(false);   


    }
    public void ResetTitle()
    {
        //var loaded = SceneManager.LoadSceneAsync(title);
        //loaded.allowSceneActivation = false;

        //プレイヤーの破壊をここに移動//
        if (PlayerInfo.InstanceNullable)
        {
            PlayerInfo.Instance.DestroySelf();
        }
        DataManager.ErasePlayerSaveData();

        //プレイヤーの破壊をここに移動//
        //loaded.allowSceneActivation = true;
        fade.feadout_f = true;
      
    }
    //撮影完了時に実行される（撮影しましたのテキスト表示）
    private void Callback()
    {
        Debug.Log("撮影完了");
        Text_screen.SetActive(true);
    }
}



// Start is called before the first frame update


