using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreditPanel1 : MonoBehaviour
{
    //�p�l���֌W
    public GameObject mainPanel;
    public GameObject subPanel;
    public GameObject OptionPanel;
    public GameObject QuitPanel;
    public GameObject startPanel;
    [SerializeField] float speed = 1;


    //�p�l���A�j���[�V����

    public float corrutin;
    [SerializeField] float StartPanel_X;
    [SerializeField] float StartPanel_Y;
    [SerializeField] float StartPanel_Z;
    [SerializeField] float StartPanel_End_X;
    [SerializeField] float StartPanel_End_Y;
    [SerializeField] float StartPanel_End_Z;
    [SerializeField] float startButton_X;
    [SerializeField] float startButton_Y;
    [SerializeField] float startButton_Z;
    [SerializeField] float startButton_End_X;
    [SerializeField] float startButton_End_Y;
    [SerializeField] float startButton_End_Z;
    [SerializeField] float OptionPanel_X;
    [SerializeField] float OptionPanel_Y;
    [SerializeField] float OptionPanel_Z;
    [SerializeField] float OptionPanel_End_X;
    [SerializeField] float OptionPanel_End_Y;
    [SerializeField] float OptionPanel_End_Z;
    [SerializeField] float OptionButton_X;
    [SerializeField] float OptionButton_Y;
    [SerializeField] float OptionButton_Z;
    [SerializeField] float OptionButton_End_X;
    [SerializeField] float OptionButton_End_Y;
    [SerializeField] float OptionButton_End_Z;

    [SerializeField] float CreditPanel_X;
    [SerializeField] float CreditPanel_Y;
    [SerializeField] float CreditPanel_Z;
    [SerializeField] float CreditPanel_End_X;
    [SerializeField] float CreditPanel_End_Y;
    [SerializeField] float CreditPanel_End_Z;

    [SerializeField] float CreditButton_X;
    [SerializeField] float CreditButton_Y;
    [SerializeField] float CreditButton_Z;
    [SerializeField] float CreditButton_End_X;
    [SerializeField] float CreditButton_End_Y;
    [SerializeField] float CreditButton_End_Z;
    [SerializeField] float QuitPanel_X;
    [SerializeField] float QuitPanel_Y;
    [SerializeField] float QuitPanel_Z;
    [SerializeField] float QuitPanel_End_X;
    [SerializeField] float QuitPanel_End_Y;
    [SerializeField] float QuitPanel_End_Z;
    [SerializeField] float QuitButton_X;
    [SerializeField] float QuitButton_Y;
    [SerializeField] float QuitButton_Z;
    [SerializeField] float QuitButton_End_X;
    [SerializeField] float QuitButton_End_Y;
    [SerializeField] float QuitButton_End_Z;
    [SerializeField] float ContinueButton_X;
    [SerializeField] float ContinueButton_Y;
    [SerializeField] float ContinueButton_Z;
    [SerializeField] float ContinueButton_End_X;
    [SerializeField] float ContinueButton_End_Y;
    [SerializeField] float ContinueButton_End_Z;

    //�Q�[���I�u�W�F�N�g�̃{�^��(Setactive)
    [SerializeField] GameObject startButton;
    [SerializeField] GameObject ContinueButton;
    [SerializeField] GameObject quitButton;
    [SerializeField] GameObject CreditButton;
    [SerializeField] GameObject optionButton;
    [SerializeField] multiAudio multi;

    //�{�^���̃C�x���g�g���K�[�֘A
    [SerializeField] EventTrigger eventTrigger_start;
    [SerializeField] EventTrigger eventTrigger_quit;
    [SerializeField] EventTrigger eventTrigger_Credit;
    [SerializeField] EventTrigger eventTrigger_option;
    //[SerializeField] EventTrigger eventTrigger_Continue_;

    //�{�^���@�\�֘A�iInteractive)
    public Button start;

    //public Button Continue;
    public Button quit;
    public Button Credit;
    public Button option;
    [SerializeField] Audiovolume audiovolume;


    void Start()//�n�܂�Ƃ�
    {
        audiovolume = GameObject.FindAnyObjectByType<Audiovolume>().GetComponent<Audiovolume>();
        GameObject.FindWithTag("BGM").GetComponent<AudioSource>().volume = audiovolume.BGM;
        GameObject.FindWithTag("SE").GetComponent<AudioSource>().volume = audiovolume.SE;
        multi.ChooseSongs_BGM(0);

        //multi.playse();

        //�p�l���֌W
        mainPanel.SetActive(true);
        subPanel.SetActive(false);
        //multi.BGMSE();
        OptionPanel.SetActive(false);
        QuitPanel.SetActive(false);
        startPanel.SetActive(false);

        //�Q�[���I�u�W�F�N�g�̃{�^��(Setactive)
        startButton.SetActive(true);
        ContinueButton.SetActive(true);
        optionButton.SetActive(true);
        quitButton.SetActive(true);
        CreditButton.SetActive(true);

        //�V���O���g��
        //SceneManager.sceneLoaded += OnSceneLoaded;



        //�{�^���̃C�x���g�g���K�[�֘A
        eventTrigger_start.enabled = true;
        eventTrigger_quit.enabled = true;
        eventTrigger_option.enabled = true;
        eventTrigger_Credit.enabled = true;

        //�{�^���@�\�֘A�iInteractive)
        start.interactable = true;
        quit.interactable = true;
        Credit.interactable = true;
        option.interactable = true;
        //eventTrigger_Continue_.enabled = true;

    }
    //void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    //{
    //    PlayerInfo.Instance.SwitchUIVisibility(); ;
    //}
    public virtual void MainView()//���C����ʂ̂ݕ\��
    {
        if (startPanel.activeSelf)
        {
            StartSlideOut();
            //startPanel.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            //if()
            //startPanel.SetActive(false);

        }
        if (subPanel.activeSelf)
        {
            StartSlideOut();
            //subPanel.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            //subPanel.SetActive(false);

        }
        if (OptionPanel.activeSelf)
        {

            StartSlideOut();
            //OptionPanel.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            //OptionPanel.SetActive(false);

        }
        if (QuitPanel.activeSelf)
        {
            StartSlideOut();
            //QuitPanel.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

            //QuitPanel.SetActive(false);

        }
        //�p�l���֌W
        mainPanel.SetActive(true);

        //subPanel.SetActive(false);
        //OptionPanel.SetActive(false);
        //QuitPanel.SetActive(false);
        //startPanel.SetActive(false);

        Invoke(nameof(True_SetActive), 0.2f);

        //�{�^���@�\�֘A�iInteractive)
        start.interactable = true;
        quit.interactable = true;
        Credit.interactable = true;
        option.interactable = true;


        //�{�^���̃C�x���g�g���K�[�֘A
        eventTrigger_start.enabled = true;
        eventTrigger_quit.enabled = true;
        eventTrigger_option.enabled = true;
        eventTrigger_Credit.enabled = true;



    }

    public void True_SetActive()
    {
        //�Q�[���I�u�W�F�N�g�̃{�^��(Setactive)
        startButton.SetActive(true);
        ContinueButton.SetActive(true);
        optionButton.SetActive(true);
        quitButton.SetActive(true);
        CreditButton.SetActive(true);

    }
    public virtual void SubView() //�N���W�b�g��ʕ\��
    {
        //�p�l���֌W
        mainPanel.SetActive(true);
        subPanel.SetActive(true);
        OptionPanel.SetActive(false);
        QuitPanel.SetActive(false);
        startPanel.SetActive(false);

        //�Q�[���I�u�W�F�N�g�̃{�^��(Setactive)
        startButton.SetActive(false);
        ContinueButton.SetActive(false);
        optionButton.SetActive(false);
        quitButton.SetActive(false);
        CreditButton.SetActive(true);

        //�{�^���̃C�x���g�g���K�[�֘A
        //eventTrigger_start.enabled = false;
        //eventTrigger_quit.enabled = false;
        //eventTrigger_option.enabled = false;
        eventTrigger_Credit.enabled = false;
        //eventTrigger_Continue_.enabled = false;

        //�{�^���@�\�֘A�iInteractive)
        //start.interactable = false;
        //quit.interactable = false;
        Credit.interactable = false;
        //option.interactable = false;

        //�p�l���g�傷��O�i�N���W�b�g�p�l���j
        if (subPanel.activeSelf)
        {
            //subPanel.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            subPanel.transform.localPosition = new Vector3(CreditPanel_X, CreditPanel_Y, CreditPanel_Z);
            StartSlidein();
        }
    }

    public virtual void CreditView()//�I�v�V�����\��
    {
        //�p�l���֌W
        mainPanel.SetActive(true);
        subPanel.SetActive(false);
        OptionPanel.SetActive(true);
        QuitPanel.SetActive(false);
        startPanel.SetActive(false);

        //�Q�[���I�u�W�F�N�g�̃{�^��(Setactive)
        startButton.SetActive(false);
        ContinueButton.SetActive(false);
        optionButton.SetActive(true);
        quitButton.SetActive(false);
        CreditButton.SetActive(false);

        //�{�^���̃C�x���g�g���K�[�֘A
        //eventTrigger_start.enabled = false;
        //eventTrigger_quit.enabled = false;
        eventTrigger_option.enabled = false;
        //eventTrigger_Credit.enabled = false;
        //eventTrigger_Continue_.enabled = false;

        //�{�^���@�\�֘A�iInteractive)
        //start.interactable = false;
        //Continue.interactable = false;
        //Credit.interactable = false;
        //quit.interactable = false;
        option.interactable = false;

        //�p�l���g�傷��O�i�I�v�V�����p�l���j
        if (OptionPanel.activeSelf)
        {
            //OptionPanel.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            OptionPanel.transform.localPosition = new Vector3(OptionPanel_X, OptionPanel_Y, OptionPanel_Z);

            StartSlidein();
        }
    }

    public void QuitView()//�I����ʕ\��
    {
        //�p�l���֌W
        mainPanel.SetActive(true);
        subPanel.SetActive(false);
        OptionPanel.SetActive(false);
        QuitPanel.SetActive(true);
        startPanel.SetActive(false);

        //�Q�[���I�u�W�F�N�g�̃{�^��(Setactive)
        startButton.SetActive(false);
        ContinueButton.SetActive(false);
        optionButton.SetActive(false);
        quitButton.SetActive(true);
        CreditButton.SetActive(false);

        //�{�^���̃C�x���g�g���K�[�֘A
        //eventTrigger_start.enabled = false;
        eventTrigger_quit.enabled = false;
        //eventTrigger_option.enabled = false;
        //eventTrigger_Credit.enabled = false;
        //eventTrigger_Continue_.enabled = false;

        //�{�^���@�\�֘A�iInteractive)
        //start.interactable = false;
        //Continue.interactable = false;
        //Credit.interactable = false;
        quit.interactable = false;
        //option.interactable = false;

        //�p�l���g�傷��O�i�I���p�l���j
        if (QuitPanel.activeSelf)
        {
            //QuitPanel.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            QuitPanel.transform.localPosition = new Vector3(QuitPanel_X, QuitPanel_Y, QuitPanel_Z);
            StartSlidein();
        }
    }
    public void startView()//�X�^�[�g�p�l���\��
    {
        //�p�l���֌W
        mainPanel.SetActive(true);
        subPanel.SetActive(false);
        OptionPanel.SetActive(false);
        QuitPanel.SetActive(false);
        startPanel.SetActive(true);

        //�Q�[���I�u�W�F�N�g�̃{�^��(Setactive)
        startButton.SetActive(true);
        ContinueButton.SetActive(false);
        optionButton.SetActive(false);
        quitButton.SetActive(false);
        CreditButton.SetActive(false);

        //�{�^���̃C�x���g�g���K�[�֘A
        eventTrigger_start.enabled = false;
        //eventTrigger_quit.enabled = false;
        //eventTrigger_option.enabled = false;
        //eventTrigger_Credit.enabled = false;
        //eventTrigger_Continue_.enabled = false;

        //�{�^���@�\�֘A�iInteractive)
        start.interactable = false;
        //Continue.interactable = false;
        //Credit.interactable = false;
        //option.interactable = false;
        //quit.interactable = false;

        //�p�l���g�傷��O�i�X�^�[�g�p�l���j
        if (startPanel.activeSelf)
        {
            //startPanel.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            startPanel.transform.localPosition = new Vector3(StartPanel_X, StartPanel_Y, StartPanel_Z);
            StartSlidein();
        }
    }

    public void StartSlidein()//�p�l���g��J�n�̂��߂̊֐�
    {
        StartCoroutine(ChangePaneltoBigSize());
    }

    //�p�l���g��(�ėp)
    public IEnumerator ChangePaneltoBigSize()
    {
        var size = 0f;

        var size2 = 0f;
        var size3 = 0f;
        var size4 = 0f;


        //�p�l���g��i�X�^�[�g�p�l���j

        while (size <= 1.0f && startPanel.activeSelf)
        {
            //startPanel.transform.localScale = Vector3.Lerp(new Vector3(1.0f, 1.0f, 1.0f), new Vector3(1.5f, 1.5f, 1.5f), size);

            startPanel.transform.localPosition = Vector3.Lerp(new Vector3(StartPanel_X, StartPanel_Y, StartPanel_Z), new Vector3(StartPanel_End_X, StartPanel_End_Y, StartPanel_End_Z), size);
            size += speed * Time.deltaTime;
            ;
            //startPanel.transform.localPosition =
            yield return new WaitForSeconds(corrutin /** Time.deltaTime*/);
        }
        //�p�l���g��i�I�v�V�����p�l���j
        while (size2 <= 1.0f && OptionPanel.activeSelf)
        {
            //OptionPanel.transform.localScale = Vector3.Lerp(new Vector3(0.5f, 0.5f, 0.5f), new Vector3(1.0f, 1.0f, 1.0f), size2);
            OptionPanel.transform.localPosition = Vector3.Lerp(new Vector3(OptionPanel_X, OptionPanel_Y, OptionPanel_Z), new Vector3(OptionPanel_End_X, OptionPanel_End_Y, OptionPanel_End_Z), size2);
            size2 += speed * Time.deltaTime;

            yield return new WaitForSeconds(corrutin /** Time.deltaTime*/);
        }
        //�p�l���g��i�N���W�b�g�p�l���j

        while (size3 <= 1.0f && subPanel.activeSelf)
        {
            //subPanel.transform.localScale = Vector3.Lerp(new Vector3(0.5f, 0.5f, 0.5f), new Vector3(1.0f, 1.0f, 1.0f), size3);
            subPanel.transform.localPosition = Vector3.Lerp(new Vector3(CreditPanel_X, CreditPanel_Y, CreditPanel_Z), new Vector3(CreditPanel_End_X, CreditPanel_End_Y, CreditPanel_End_Z), size3);



            size3 += speed * Time.deltaTime;

            yield return new WaitForSeconds(corrutin /** Time.deltaTime*/);
        }

        //�p�l���g��i�I���p�l���j

        while (size4 <= 1.0f && QuitPanel.activeSelf)
        {
            //QuitPanel.transform.localScale = Vector3.Lerp(new Vector3(0.5f, 0.5f, 0.5f), new Vector3(1.0f, 1.0f, 1.0f), size4);
            QuitPanel.transform.localPosition = Vector3.Lerp(new Vector3(QuitPanel_X, QuitPanel_Y, QuitPanel_Z), new Vector3(QuitPanel_End_X, QuitPanel_End_Y, QuitPanel_End_Z), size4);
            size4 += speed * Time.deltaTime;

            yield return new WaitForSeconds(corrutin);
        }

    }

    public void StartSlideOut()//�p�l���k���J�n�̂��߂̊֐�
    {
        StartCoroutine(ChangePaneltoSmallSize());
        //StartCoroutine(ChangeButtontoSmallSize());
    }


    //�p�l���k��(�ėp)
    public IEnumerator ChangePaneltoSmallSize()
    {
        var size = 0f;
        var size2 = 0f;
        var size3 = 0f;
        var size4 = 0f;


       
        //�p�l���k���i�X�^�[�g�p�l���j

        while (size <= 1.0f && startPanel.activeSelf)
        {
            //startPanel.transform.localScale = Vector3.Lerp(new Vector3(1.5f, 1.5f, 1.5f), new Vector3(1.0f, 1.0f, 1.0f), size);
            startPanel.transform.localPosition = Vector3.Lerp(new Vector3(StartPanel_End_X, StartPanel_End_Y, StartPanel_End_Z), new Vector3(StartPanel_X, StartPanel_Y, StartPanel_Z), size);
            size += speed * Time.deltaTime;
            yield return new WaitForSeconds(corrutin);
        }
        Debug.Log("�ʂ���");

        //�p�l���k���i�I�v�V�����p�l���j
        while (size2 <= 1.0f && OptionPanel.activeSelf)
        {
            //OptionPanel.transform.localScale = Vector3.Lerp(new Vector3(1.0f, 1.0f, 1.0f), new Vector3(0.5f, 0.5f, 0.5f), size2);
            OptionPanel.transform.localPosition = Vector3.Lerp(new Vector3(OptionPanel_End_X, OptionPanel_End_Y, OptionPanel_End_Z), new Vector3(OptionPanel_X, OptionPanel_Y, OptionPanel_Z), size2);

            size2 += speed * Time.deltaTime;



            yield return new WaitForSeconds(corrutin);
        }
        //�p�l���k���i�I�v�V�����{�^���j

        //while (size2 <= 1.0f && !optionButton.activeSelf)
        //{
        //    optionButton.transform.localPosition = Vector3.Lerp(new Vector3(OptionButton_End_X, OptionButton_End_Y, OptionButton_End_Z), new Vector3(OptionButton_X, OptionButton_Y, OptionButton_Z), size);
        //    size += speed * Time.deltaTime;
        //    yield return new WaitForSeconds(corrutin);
        //}
        //�p�l���k���i�N���W�b�g�{�^���j

        //while (size2 <= 1.0f && !CreditButton.activeSelf)
        //{
        //    CreditButton.transform.localPosition = Vector3.Lerp(new Vector3(CreditButton_End_X, CreditButton_End_Y, CreditButton_End_Z), new Vector3(OptionButton_X, CreditButton_Y, CreditButton_Z), size);
        //    size += speed * Time.deltaTime;
        //    yield return new WaitForSeconds(corrutin);
        //}
        //�p�l���k���i�N���W�b�g�p�l���j

        while (size3 <= 1.0f && subPanel.activeSelf)
        {
            //subPanel.transform.localScale = Vector3.Lerp(new Vector3(1.0f, 1.0f, 1.0f), new Vector3(0.5f, 0.5f, 0.5f), size3);
            subPanel.transform.localPosition = Vector3.Lerp(new Vector3(CreditPanel_End_X, CreditPanel_End_Y, CreditPanel_End_Z), new Vector3(CreditPanel_X, CreditPanel_Y, CreditPanel_Z), size3);


            size3 += speed * Time.deltaTime;


            yield return new WaitForSeconds(corrutin);
        }

        //�p�l���g��i�I���p�l���j

        while (size4 <= 1.0f && QuitPanel.activeSelf)
        {

            //QuitPanel.transform.localScale = Vector3.Lerp(new Vector3(1.0f, 1.0f, 1.0f), new Vector3(0.5f, 0.5f, 0.5f), size4);
            QuitPanel.transform.localPosition = Vector3.Lerp(new Vector3(QuitPanel_End_X, QuitPanel_End_Y, QuitPanel_End_Z), new Vector3(QuitPanel_X, QuitPanel_Y, QuitPanel_Z), size4);

            size4 += speed * Time.deltaTime;

            yield return new WaitForSeconds(corrutin);
        }


        //while (size2 <= 1.0f && !quitButton.activeSelf)

        //{
        //    quitButton.transform.localPosition = Vector3.Lerp(new Vector3(QuitButton_End_X, QuitButton_End_Y, QuitButton_End_Z), new Vector3(QuitButton_X, QuitButton_Y, QuitButton_Z), size);
        //    size += speed * Time.deltaTime;
        //    yield return new WaitForSeconds(corrutin);
        //}

        //while (size2 <= 1.0f && !ContinueButton.activeSelf)

        //{
        //    ContinueButton.transform.localPosition = Vector3.Lerp(new Vector3(ContinueButton_End_X, ContinueButton_End_Y, ContinueButton_End_Z), new Vector3(ContinueButton_X, ContinueButton_Y, ContinueButton_Z), size);
        //    size += speed * Time.deltaTime;
        //    yield return new WaitForSeconds(corrutin);
        //}

        Debug.Log("�ʂ���");
        subPanel.SetActive(false);
        OptionPanel.SetActive(false);
        QuitPanel.SetActive(false);
        startPanel.SetActive(false);
        Debug.Log("�ʂ���");


    }

    //public IEnumerator ChangeButtontoSmallSize()
    //{
    //    var size = 0f;
    //    var size2 = 0f;
    //    var size3 = 0f;
    //    var size4 = 0f;
    //    //�p�l���k���i�X�^�[�g�{�^���j

    //    while (size <= 1.0f && !startButton.activeSelf)
    //    {
    //        startButton.transform.localPosition = Vector3.Lerp(new Vector3(startButton_X, startButton_Y, startButton_Z), new Vector3(startButton_End_X, startButton_End_Y, startButton_End_Z), size);
    //        size += speed * Time.deltaTime;
    //        yield return new WaitForSeconds(corrutin);
    //    }
    //    //�p�l���k���i�I�v�V�����{�^���j

    //    while (size2 <= 1.0f &&!optionButton.activeSelf)
    //    {
    //        optionButton.transform.localPosition = Vector3.Lerp(new Vector3(OptionButton_X, OptionButton_Y, OptionButton_Z), new Vector3(OptionButton_End_X, OptionButton_End_Y, OptionButton_End_Z), size);
    //        size += speed * Time.deltaTime;
    //        yield return new WaitForSeconds(corrutin);
    //    }



    //    //�p�l���k���i�N���W�b�g�{�^���j

    //    while (size3 <= 1.0f && !CreditButton.activeSelf)
    //    {
    //        CreditButton.transform.localPosition = Vector3.Lerp(new Vector3(CreditButton_X, CreditButton_Y, CreditButton_Z), new Vector3(OptionButton_End_X, CreditButton_End_Y, CreditButton_End_Z), size);
    //        size += speed * Time.deltaTime;
    //        yield return new WaitForSeconds(corrutin);
    //    }
    //    while (size2 <= 1.0f && ContinueButton.activeSelf)

    //    {
    //        ContinueButton.transform.localPosition = Vector3.Lerp(new Vector3(ContinueButton_X, ContinueButton_Y, ContinueButton_Z), new Vector3(OptionButton_End_X, ContinueButton_End_Y, ContinueButton_End_Z), size);
    //        size += speed * Time.deltaTime;
    //        yield return new WaitForSeconds(corrutin);
    //    }


    //    //�p�l���g��i�I���p�l���j

    //    while (size4 <= 1.0f && !quitButton.activeSelf)
    //    {

    //        //QuitPanel.transform.localScale = Vector3.Lerp(new Vector3(1.0f, 1.0f, 1.0f), new Vector3(0.5f, 0.5f, 0.5f), size4);
    //        quitButton.transform.localPosition = Vector3.Lerp(new Vector3(QuitButton_X, QuitButton_Y, QuitButton_Z), new Vector3(QuitButton_End_X, QuitButton_End_Y, QuitButton_End_Z), size4);

    //        size4 += speed * Time.deltaTime;

    //        yield return new WaitForSeconds(corrutin);
    //    }
    //}
        void Update()
    {
        //escape�L�[�Ή�
        if (Input.GetMouseButtonDown(1)||Input.GetKeyDown(KeyCode.Escape))
        {
            //�p�l���֌W
            OptionPanel.SetActive(false);
            mainPanel.SetActive(true);
            subPanel.SetActive(false);
            QuitPanel.SetActive(false);
            startPanel.SetActive(false);

            //�Q�[���I�u�W�F�N�g�̃{�^��(Setactive)
            startButton.SetActive(true);
            ContinueButton.SetActive(true);
            optionButton.SetActive(true);
            quitButton.SetActive(true);
            CreditButton.SetActive(true);

            //�{�^���@�\�֘A�iInteractive)
            start.interactable = true;
            //Continue.interactable = true;
            Credit.interactable = true;
            quit.interactable = true;
            option.interactable = true;

            //�{�^���̃C�x���g�g���K�[�֘A
            eventTrigger_start.enabled = true;
            eventTrigger_quit.enabled = true;
            eventTrigger_option.enabled = true;
            eventTrigger_Credit.enabled = true;
        }
    }
}

