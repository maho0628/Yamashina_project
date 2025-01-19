using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScene : MonoBehaviour
{
    private void Start()
    {
        isstart = false;
        audioVolume = GameObject.FindAnyObjectByType<Audiovolume>().GetComponent<Audiovolume>();
    }
    private void Update()
    {


        if (!isstart)
        {
            if (!DataManager.DoesSaveExist())
            {
                Debug.Log(DataManager.DoesSaveExist());
                //image.SetActive(true);

                string colorString = "#999999"; // 灰色の16進数文字列
                Color newColor;
                ColorUtility.TryParseHtmlString(colorString, out newColor); // 新しくColorを作成
                Text.color = newColor;
                Debug.Log(newColor);

                eventTrigger.enabled = false;
                continueButton.interactable = false;
               
                Debug.Log("通った");
            }
            else
            {


            }
        }

       
      
        //SceneManager.activeSceneChanged += AfterTrueEndScene;


        //image.SetActive(false);
        //if (CreditPanel1 = null)
        //{
        //    CreditPanel1 = GameObject.FindAnyObjectByType<CreditPanel1>();
        //}
        //else
        //{
        //    Debug.Log("credit is not null");

        //}
    }
    //[SerializeField] multiAudio multiAudio;
    [SerializeField] Fade fade;
    [SerializeField] Text Text;
    [SerializeField] EventTrigger eventTrigger;
    [SerializeField] Button continueButton;
    [SerializeField] CreditPanel1 CreditPanel1;
   [SerializeField] GameObject playerInfo;
    [SerializeField] bool isstart;
    [SerializeField]Audiovolume audioVolume;    
    //[SerializeField] GameObject image;
    // Update is called once per frame
    public void Text_of_each_places(int num = 0)
    {
        if (!DataManager.DoesSaveExist())
        {
            AfterStart();

        }
        else
        {
            CreditPanel1.startView();
        }



    }
    public void AfterStart(int num = 0)
    {
        fade.scene_name_num = num;
        //Instantiate(playerInfo);
        //PlayerInfo.Instance.StartGame(true);
        isstart = true;
        fade.feadout_f = true;
       
        if (fade.feadout_f == false)
        {
            GameObject.FindWithTag("BGM").GetComponent<AudioSource>().volume = audioVolume.BGM;
           GameObject.FindWithTag("SE").GetComponent<AudioSource>().volume=audioVolume.SE  ;
            Debug.Log(audioVolume.BGM);
            Debug.Log(audioVolume.SE);

        }


        //SceneManager.sceneUnloaded += VolumeSave_loadsecene;




    }
    public void Continue(int num)
    {
        if (!DataManager.DoesSaveExist())
        {

            string colorString = "#999999"; // 赤色の16進数文字列
            Color newColor;
            ColorUtility.TryParseHtmlString(colorString, out newColor); // 新しくColorを作成
            Text.color = newColor;
            eventTrigger.enabled = false;
            continueButton.enabled = false;
            Debug.Log("通った");


        }
        else
        {
            fade.scene_name_num = num;
            Instantiate(playerInfo);
            PlayerInfo.Instance.StartGame(false);
            fade.feadout_f = true;
            
            if (fade.feadout_f == false)
            {
                GameObject.FindWithTag("BGM").GetComponent<AudioSource>().volume = audioVolume.BGM;
          GameObject.FindWithTag("SE").GetComponent<AudioSource>().volume = audioVolume.SE;
                Debug.Log(audioVolume.BGM);
                Debug.Log(audioVolume.SE);
            }

        }




    }
    //private void Awake()
    //{
    //    if (!DataManager.DoesSaveExist())
    //    {
    //        Debug.Log(DataManager.DoesSaveExist());
    //        //image.SetActive(true);

    //        string colorString = "#999999"; // 灰色の16進数文字列
    //        Color newColor;
    //        ColorUtility.TryParseHtmlString(colorString, out newColor); // 新しくColorを作成
    //        Text.color = newColor;
    //        Debug.Log(newColor);

    //        eventTrigger.enabled = false;
    //        continueButton.interactable = false;


    //        Debug.Log("通った");
    //    }
    //}
       
    
    // void AfterTrueEndScene(Scene scene, Scene nextScene)
    //{
    //    Debug.Log(nextScene.name);
    //    if (!DataManager.DoesSaveExist()&&nextScene.name== "Title")
    //    {

    //        //image.SetActive(true);

    //        string colorString = "#999999"; // 赤色の16進数文字列
    //        Color newColor;
    //        ColorUtility.TryParseHtmlString(colorString, out newColor); // 新しくColorを作成
    //        Text.color = newColor;
    //        //eventTrigger.enabled = false;
    //        //continueButton.enabled = false;


    //        Debug.Log("通った");
    //    }
    //}

}

