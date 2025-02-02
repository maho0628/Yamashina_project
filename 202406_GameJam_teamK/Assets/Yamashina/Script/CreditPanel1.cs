using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer.Internal;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreditPanel1 : MonoBehaviour
{
    //panelのオブジェクト
    public GameObject mainPanel;
    public GameObject subPanel;
    public GameObject OptionPanel;
    //public GameObject QuitPanel;
    //public GameObject RankingPanel;
    ButtonEffect buttonEffect;
    [SerializeField] float speed = 1;


    //�p�l���A�j���[�V����

    public float corrutin;


    //パネルのアニメーションの座標（始点と終点）
    [SerializeField] float OptionPanel_X;
    [SerializeField] float OptionPanel_Y;
    [SerializeField] float OptionPanel_Z;
    [SerializeField] float OptionPanel_End_X;
    [SerializeField] float OptionPanel_End_Y;
    [SerializeField] float OptionPanel_End_Z;
    //[SerializeField] float RankingPanel_X;
    //[SerializeField] float RankingPanel_Y;
    //[SerializeField] float RankingPanel_Z;
    //[SerializeField] float RankingPanel_End_X;
    //[SerializeField] float RankingPanel_End_Y;
    //[SerializeField] float RankingPanel_End_Z;

    [SerializeField] float CreditPanel_X;
    [SerializeField] float CreditPanel_Y;
    [SerializeField] float CreditPanel_Z;
    [SerializeField] float CreditPanel_End_X;
    [SerializeField] float CreditPanel_End_Y;
    [SerializeField] float CreditPanel_End_Z;


    //[SerializeField] float QuitPanel_X;
    //[SerializeField] float QuitPanel_Y;
    //[SerializeField] float QuitPanel_Z;
    //[SerializeField] float QuitPanel_End_X;
    //[SerializeField] float QuitPanel_End_Y;
    //[SerializeField] float QuitPanel_End_Z;


    //ボタンのSetActive関連
    [SerializeField] GameObject startButton;
    [SerializeField] GameObject quitButton;
    //[SerializeField] GameObject RankingButton;
    [SerializeField] GameObject CreditButton;
    [SerializeField] GameObject optionButton;

    enum Button_type
    {
        None,
        Option,
        //Ranking,
        Quit,
        Credit
    }
    Button_type button_Type = Button_type.None;




    [SerializeField] public GameObject[] b_Image;
    [SerializeField] GameObject[] hightlightImage;
    [SerializeField] Sprite hightLight;
    [SerializeField] Sprite hightLightOff;
    //イベントトリガーの切り替え
    [SerializeField] EventTrigger eventTrigger_start;
    [SerializeField] EventTrigger eventTrigger_quit;
    [SerializeField] EventTrigger eventTrigger_Credit;
    [SerializeField] EventTrigger eventTrigger_option;
    //[SerializeField] EventTrigger eventTrigger_Ranking;

    //ボタンのiInteractive切り替え
    public Button start;

    //public Button Ranking;
    public Button quit;
    public Button Credit;
    public Button option;
    //オーディオの音量管理のスクリプトのインスタンス
    [SerializeField] Audiovolume audiovolume;
    //オーディオの音源を選ぶためのスクリプトのインスタンス
    [SerializeField] multiAudio multi;

    void Start()//�n�܂�Ƃ�
    {
        //オーディオ関連
        audiovolume = GameObject.FindAnyObjectByType<Audiovolume>().GetComponent<Audiovolume>();
        GameObject.FindWithTag("BGM").GetComponent<AudioSource>().volume = audiovolume.BGM;
        GameObject.FindWithTag("SE").GetComponent<AudioSource>().volume = audiovolume.SE;
        multi.ChooseSongs_BGM(0);


        //panelのオブジェクト
        mainPanel.SetActive(true);
        OptionPanel.SetActive(false);
        //QuitPanel.SetActive(false);
        //RankingPanel.SetActive(false);
        //ボタンのSetactive
        startButton.SetActive(true);
        optionButton.SetActive(true);
        quitButton.SetActive(true);
        CreditButton.SetActive(true);
        eventTrigger_start.triggers.Add(new EventTrigger.Entry { eventID = EventTriggerType.PointerEnter });
        eventTrigger_start.triggers[0].callback.AddListener((data) => { OnRouteEnter(start); });
        eventTrigger_start.triggers.Add(new EventTrigger.Entry { eventID = EventTriggerType.PointerExit });
        eventTrigger_start.triggers[1].callback.AddListener((data) => { OnRouteExit(start); });




        eventTrigger_option.triggers.Add(new EventTrigger.Entry { eventID = EventTriggerType.PointerEnter });
        eventTrigger_option.triggers[0].callback.AddListener((data) => { OnRouteEnter(option); });
        eventTrigger_option.triggers.Add(new EventTrigger.Entry { eventID = EventTriggerType.PointerExit });
        eventTrigger_option.triggers[1].callback.AddListener((data) => { OnRouteExit(option); });
        Debug.Log(eventTrigger_option.enabled);


        eventTrigger_Credit.triggers.Add(new EventTrigger.Entry { eventID = EventTriggerType.PointerEnter });
        eventTrigger_Credit.triggers[0].callback.AddListener((data) => { OnRouteEnter(Credit); });
        eventTrigger_Credit.triggers.Add(new EventTrigger.Entry { eventID = EventTriggerType.PointerExit });
        eventTrigger_Credit.triggers[1].callback.AddListener((data) => { OnRouteExit(Credit); });

        eventTrigger_quit.triggers.Add(new EventTrigger.Entry { eventID = EventTriggerType.PointerEnter });
        eventTrigger_quit.triggers[0].callback.AddListener((data) => { OnRouteEnter(quit); });
        eventTrigger_quit.triggers.Add(new EventTrigger.Entry { eventID = EventTriggerType.PointerExit });
        eventTrigger_quit.triggers[1].callback.AddListener((data) => { OnRouteExit(quit); });

        //eventTrigger_Ranking.triggers.Add(new EventTrigger.Entry { eventID = EventTriggerType.PointerEnter });
        //eventTrigger_Ranking.triggers[0].callback.AddListener((data) => { OnRouteEnter(Ranking); });
        //eventTrigger_Ranking.triggers.Add(new EventTrigger.Entry { eventID = EventTriggerType.PointerExit });
        //eventTrigger_Ranking.triggers[1].callback.AddListener((data) => { OnRouteExit(Ranking); });



        //イベントトリガーのEnable関連

        eventTrigger_start.enabled = true;
        eventTrigger_quit.enabled = true;
        eventTrigger_option.enabled = true;
        eventTrigger_Credit.enabled = true;
        //eventTrigger_Ranking.enabled = true;
        //ボタンのiInteractive
        start.interactable = true;
        quit.interactable = true;
        Credit.interactable = true;
        option.interactable = true;
        //Ranking.interactable = true;
    }

    public void MainView()//初期画面表示
    {

        //初期画面に戻すためのアニメーション
        if (OptionPanel.activeSelf)
        {

            StartSlideOut();


        }
        //if (QuitPanel.activeSelf)
        //{
        //    StartSlideOut();


        //}
        if (subPanel.activeSelf)
        {
            StartSlideOut();

        }

        //if (RankingPanel.activeSelf)
        //{
        //    StartSlideOut();

        //}
        //panelのオブジェクト
        //mainPanel.SetActive(true);



        Invoke(nameof(True_SetActive), 0.2f);

        //ボタンのiInteractive
        start.interactable = true;
        quit.interactable = true;
        Credit.interactable = true;
        option.interactable = true;
        //Ranking.interactable = true;

        //イベントトリガーのEnable関連
        eventTrigger_start.enabled = true;
        eventTrigger_quit.enabled = true;
        eventTrigger_option.enabled = true;
        eventTrigger_Credit.enabled = true;
        //eventTrigger_Ranking.enabled = true;



    }
    public virtual void SubView() //クレジット表示
    {
        //パネルのアニメーション
        if (subPanel.activeSelf)
        {
            subPanel.transform.localPosition = new Vector3(CreditPanel_X, CreditPanel_Y, CreditPanel_Z);
            StartSlidein();
        }
        //panelのオブジェクト
        mainPanel.SetActive(true);
        subPanel.SetActive(true);
        OptionPanel.SetActive(false);
        //QuitPanel.SetActive(false);
        //RankingPanel.SetActive(false);
        //ボタンのSetactive

        startButton.SetActive(false);
        optionButton.SetActive(false);
        quitButton.SetActive(false);
        CreditButton.SetActive(true);
        //RankingButton.SetActive(false);

        //イベントトリガーのEnable関連

        eventTrigger_Credit.enabled = false;

        //ボタンのiInteractive

        Credit.interactable = false;





    }
    public void True_SetActive()
    {
        //ボタンのSetactive

        startButton.SetActive(true);
        optionButton.SetActive(true);
        quitButton.SetActive(true);
        CreditButton.SetActive(true);
        //RankingButton.SetActive(true);

    }

    //public void RankingView()//ランキングパネル表示
    //{
    //    //panelのオブジェクト

    //    if (RankingPanel.activeSelf)
    //    {
    //        RankingPanel.transform.localPosition = new Vector3(RankingPanel_X, RankingPanel_Y, RankingPanel_Z);
    //        StartSlidein();
    //    }
    //    mainPanel.SetActive(true);
    //    subPanel.SetActive(false);
    //    OptionPanel.SetActive(false);
    //    //QuitPanel.SetActive(false);
    //    RankingPanel.SetActive(true);
    //    //ボタンのSetactive

    //    startButton.SetActive(false);
    //    RankingButton.SetActive(true);
    //    optionButton.SetActive(false);
    //    quitButton.SetActive(false);
    //    CreditButton.SetActive(false);

    //    //イベントトリガーのEnable関連
    //    eventTrigger_Ranking.enabled = false;


    //    //ボタンのiInteractive
    //    Ranking.interactable = false;

    //    //パネルのアニメーション


    //}

    public virtual void CreditView()//オプション画面表示
    {
        //panelのオブジェクト
        if (OptionPanel.activeSelf)
        {
            OptionPanel.transform.localPosition = new Vector3(OptionPanel_X, OptionPanel_Y, OptionPanel_Z);

            StartSlidein();
        }
        mainPanel.SetActive(true);
        OptionPanel.SetActive(true);
        //QuitPanel.SetActive(false);
        subPanel.SetActive(false);

        //ボタンのSetactive

        startButton.SetActive(false);
        //RankingButton.SetActive(false);

        optionButton.SetActive(true);
        quitButton.SetActive(false);
        CreditButton.SetActive(false);

        //イベントトリガーのEnable関連

        eventTrigger_option.enabled = false;
        Debug.Log(eventTrigger_option.enabled);

        //ボタンのiInteractive

        option.interactable = false;

        //パネルのアニメーション

    }


    //public void QuitView()//終了画面表示
    //{
    //    if (QuitPanel.activeSelf)
    //    {
    //        QuitPanel.transform.localPosition = new Vector3(QuitPanel_X, QuitPanel_Y, QuitPanel_Z);
    //        StartSlidein();
    //    }
    //    //panelのオブジェクト
    //    mainPanel.SetActive(true);
    //    RankingPanel.SetActive(false);
    //    OptionPanel.SetActive(false);
    //    QuitPanel.SetActive(true);
    //    subPanel.SetActive(false);
    //    //ボタンのSetactive
    //    startButton.SetActive(false);
    //    RankingButton.SetActive(false);

    //    optionButton.SetActive(false);
    //    quitButton.SetActive(true);
    //    CreditButton.SetActive(false);

    //    //イベントトリガーのEnable関連

    //    eventTrigger_quit.enabled = false;

    //    //ボタンのiInteractive


    //    quit.interactable = false;

    //    //パネルのアニメーション


    //}


    public void StartSlidein()//コルーチンで処理を始めるための関数（スライドイン）
    {
        StartCoroutine(ChangePaneltoBigSize());
    }

    //コルーチンでスライドインさせる関数
    public IEnumerator ChangePaneltoBigSize()
    {
        var size1 = 0f;
        var size2 = 0f;
        var size3 = 0f;
        var size4 = 0f;


        while (size1 <= 1.0f && subPanel.activeSelf)
        {
            subPanel.transform.localPosition = Vector3.Lerp(new Vector3(CreditPanel_X, CreditPanel_Y, CreditPanel_Z), new Vector3(CreditPanel_End_X, CreditPanel_End_Y, CreditPanel_End_Z), size1);
            size1 += speed * Time.deltaTime;

            yield return new WaitForSeconds(corrutin /** Time.deltaTime*/);
        }

        //オプションパネルスライドイン

        while (size2 <= 1.0f && OptionPanel.activeSelf)
        {
            OptionPanel.transform.localPosition = Vector3.Lerp(new Vector3(OptionPanel_X, OptionPanel_Y, OptionPanel_Z), new Vector3(OptionPanel_End_X, OptionPanel_End_Y, OptionPanel_End_Z), size2);
            size2 += speed * Time.deltaTime;

            yield return new WaitForSeconds(corrutin /** Time.deltaTime*/);
        }
        //ランキングパネルスライドイン
        //while (size3 <= 1.0f && RankingPanel.activeSelf)
        //{
        //    RankingPanel.transform.localPosition = Vector3.Lerp(new Vector3(RankingPanel_X, RankingPanel_Y, RankingPanel_Z), new Vector3(RankingPanel_End_X, RankingPanel_End_Y, RankingPanel_End_Z), size3);
        //    size3 += speed * Time.deltaTime;

        //    yield return new WaitForSeconds(corrutin /** Time.deltaTime*/);
        //}

        //終了パネルスライドイン

        //while (size4 <= 1.0f && QuitPanel.activeSelf)
        //{
        //    //QuitPanel.transform.localScale = Vector3.Lerp(new Vector3(0.5f, 0.5f, 0.5f), new Vector3(1.0f, 1.0f, 1.0f), size4);
        //    QuitPanel.transform.localPosition = Vector3.Lerp(new Vector3(QuitPanel_X, QuitPanel_Y, QuitPanel_Z), new Vector3(QuitPanel_End_X, QuitPanel_End_Y, QuitPanel_End_Z), size4);
        //    size4 += speed * Time.deltaTime;

        //    yield return new WaitForSeconds(corrutin);
        //}

    }

    public void StartSlideOut()//コルーチンで処理を始めるための関数（スライドアウト）
    {
        StartCoroutine(ChangePaneltoSmallSize());
    }


    //コルーチンでスライドアウトさせる関数

    public IEnumerator ChangePaneltoSmallSize()
    {
        var size1 = 0f;
        var size2 = 0f;
        var size3 = 0f;
        var size4 = 0f;

        while (size1 <= 1.0f && subPanel.activeSelf)
        {
            subPanel.transform.localPosition = Vector3.Lerp(new Vector3(CreditPanel_End_X, CreditPanel_End_Y, CreditPanel_End_Z), new Vector3(CreditPanel_X, CreditPanel_Y, CreditPanel_Z), size1);

            size1 += speed * Time.deltaTime;



            yield return new WaitForSeconds(corrutin);
        }




        //オプションパネルスライドアウト

        while (size2 <= 1.0f && OptionPanel.activeSelf)
        {
            OptionPanel.transform.localPosition = Vector3.Lerp(new Vector3(OptionPanel_End_X, OptionPanel_End_Y, OptionPanel_End_Z), new Vector3(OptionPanel_X, OptionPanel_Y, OptionPanel_Z), size2);

            size2 += speed * Time.deltaTime;



            yield return new WaitForSeconds(corrutin);
        }
        //ランキングパネルスライドアウト

        //while (size3 <= 1.0f && RankingPanel.activeSelf)
        //{
        //    RankingPanel.transform.localPosition = Vector3.Lerp(new Vector3(RankingPanel_End_X, RankingPanel_End_Y, RankingPanel_End_Z), new Vector3(RankingPanel_X, RankingPanel_Y, RankingPanel_Z), size3);

        //    size3 += speed * Time.deltaTime;



        //    yield return new WaitForSeconds(corrutin);
        //}


        //終了パネルスライドアウト


        //while (size4 <= 1.0f && QuitPanel.activeSelf)
        //{

        //    QuitPanel.transform.localPosition = Vector3.Lerp(new Vector3(QuitPanel_End_X, QuitPanel_End_Y, QuitPanel_End_Z), new Vector3(QuitPanel_X, QuitPanel_Y, QuitPanel_Z), size4);

        //    size4 += speed * Time.deltaTime;

        //    yield return new WaitForSeconds(corrutin);
        //}




        //panelのオブジェクト


        OptionPanel.SetActive(false);
        //QuitPanel.SetActive(false);
        //RankingPanel.SetActive(false);

        subPanel.SetActive(false);
    }


    void Update()
    {
        //escapeキーと左クリックで閉じる
        if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.Escape))
        {


            //panelのオブジェクト
            OptionPanel.SetActive(false);
            mainPanel.SetActive(true);
            //RankingPanel.SetActive(false);
            //QuitPanel.SetActive(false);
            subPanel.SetActive(false);

            //ボタンのSetactive
            startButton.SetActive(true);
            //RankingButton.SetActive(true)
                ;
            optionButton.SetActive(true);
            quitButton.SetActive(true);
            CreditButton.SetActive(true);
            //ボタンのiInteractive

            start.interactable = true;
            //Ranking.interactable = true;
            Credit.interactable = true;
            quit.interactable = true;
            option.interactable = true;

            //イベントトリガーのEnable関連

            //eventTrigger_Ranking.enabled = true;
            eventTrigger_start.enabled = true;
            eventTrigger_quit.enabled = true;
            eventTrigger_option.enabled = true;
            eventTrigger_Credit.enabled = true;
        }
        Debug.Log(option.GetComponent<EventTrigger>().enabled);
        if (!option.interactable && Credit.interactable &&  quit.interactable)
        {
            button_Type = Button_type.Option;
        }
        else if (!Credit.interactable && option.interactable && quit.interactable)
        {
            button_Type = Button_type.Credit;
        }
        //else if ( Credit.interactable && option.interactable && quit.interactable)
        //{
        //    button_Type = Button_type.Ranking;

        //}
        else if (!quit.interactable && Credit.interactable && option.interactable)
        {
            button_Type = Button_type.Quit;
        }
        else
        {
            button_Type = Button_type.None;

        }

        switch (button_Type) {
            case Button_type.None:
                eventTrigger_option.enabled = true;
                b_Image[0].SetActive(true);
                eventTrigger_Credit.enabled = true;
                b_Image[1].SetActive(true);
                b_Image[2].SetActive(true);
                b_Image[3].SetActive(true);
                //b_Image[4].SetActive(true);
                //eventTrigger_Ranking.enabled = true;
                eventTrigger_quit.enabled = true;

                break;
            case Button_type.Option:
                hightlightImage[1].GetComponent<Image>().sprite = hightLightOff;
                eventTrigger_option.enabled = false;
                b_Image[0].SetActive(false);
                b_Image[3].SetActive(false);
                b_Image[2].SetActive(false);
                //b_Image[4].SetActive(false);
                break;
            case Button_type.Credit:
                hightlightImage[2].GetComponent<Image>().sprite = hightLightOff;
                eventTrigger_Credit.enabled = false;

                b_Image[0].SetActive(false);
                b_Image[1].SetActive(false);
                b_Image[3].SetActive(false);
                //b_Image[4].SetActive(false);
                break;
            //case Button_type.Ranking:
            //    hightlightImage[4].GetComponent<Image>().sprite = hightLightOff;
            //    b_Image[0].SetActive(false);
            //    b_Image[1].SetActive(false);
            //    b_Image[2].SetActive(false);
            //    b_Image[4].SetActive(false);

            //    eventTrigger_Ranking.enabled = false;
            //    break;
                case Button_type.Quit:
                hightlightImage[3].GetComponent<Image>().sprite = hightLightOff;
                b_Image[0].SetActive(false);
                b_Image[1].SetActive(false);
                b_Image[2].SetActive(false);
                b_Image[3].SetActive(false);
                eventTrigger_quit.enabled = false;
        
        break;

        }

      
    }
    void OnRouteEnter(Button button)
    {
        int i = -1;
        if (button == start) i = 0;

        if (button == option) i = 1;
        if (button == Credit) i = 2;
        if (button == quit) i = 3;

        //if (button == Ranking) i = 4;

        //b_Image[i].SetActive(true);

        hightlightImage[i].GetComponent<Image>().sprite = hightLight;

    }

    void OnRouteExit(Button button)
    {
        int i = -1;
        if (button == start) i = 0;

        if (button == option) i = 1;
        if (button == Credit) i = 2;
        if (button == quit) i = 3;
        //if (button == Ranking) i = 4;

        hightlightImage[i].GetComponent<Image>().sprite = hightLightOff;


    }
}


