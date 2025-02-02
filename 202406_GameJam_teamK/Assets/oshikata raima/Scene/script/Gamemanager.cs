using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.RuleTile.TilingRuleOutput;

enum Stete
{
    win,
    lose,
    end,
    start,
    play
}
public class Gamemanager : MonoBehaviour
{
    //void test()
    //{
    //    Vector3 currentPosition = transform.position;
    //    Vector3 enemyPosition = EnemyObject[enemyCount].transform.position;
    //    switch (stete)
    //    {
    //        case Stete.start:
    //            if (lapTime < fightStartTime && Input.GetKey(KeyCode.Space) && isStart && isSpace == false)
    //            {
    //                Debug.Log("�t���C���O�I�I");

    //                isSpace = true;
    //            }

    //            // �������J�n�A���ԃ��Z�b�g
    //            if (lapTime < fightStartTime)
    //            {
    //                return;
    //            }
    //            break;
    //        case Stete.lose:
    //            {
    //                //�ړ�

    //                if (Enemy[enemyCount] < playertime)
    //                {
    //                    transform.position = new Vector3(teleportDistancelose, currentPosition.y, currentPosition.z);
    //                    EnemyObject[enemyCount].transform.position = new Vector3(teleportDistancewin, enemyPosition.y, enemyPosition.z);
    //                    Text_Katimake.SetActive(true);

    //                    Debug.Log("����");
    //                    Text_Katimake.GetComponent<Image>().sprite = Make;  
    //                    //Syouri.text = "����";

    //                    fading.fading_time = 0.5f;
    //                    fading.Fade(Fading.type.FadeOut);
    //                    fading.OnFadeEnd.AddListener(() =>
    //                    {
    //                        SceneManager.LoadScene(scene);
    //                    });
    //                    //fading.Fade(Fading.type.FadeIn);
    //                }
    //                Reset_();
    //            }
    //            break;
    //        case Stete.end:
    //            if (EnemyObject[enemyCount] == EnemyObject[4])
    //            {
    //                Debug.Log("�N���A");

    //                fading.fading_time = 0.5f;
    //                fading.Fade(Fading.type.FadeOut);
    //                fading.OnFadeEnd.AddListener(() =>
    //                {
    //                    SceneManager.LoadScene(scene);
    //                });
    //                //fading.Fade(Fading.type.FadeIn);
    //            }
    //            EnemyObject[enemyCount].SetActive(true);

    //            if (!EnemyObject[enemyCount].activeSelf)
    //            {
    //                SceneManager.LoadScene(scene);
    //            }
    //            break;
    //        case Stete.win:

    //            if (Enemy[enemyCount] > playertime)
    //            {
    //                transform.position = new Vector3(teleportDistancewin, currentPosition.y, currentPosition.z);
    //                EnemyObject[enemyCount].transform.position = new Vector3(teleportDistancelose, enemyPosition.y, enemyPosition.z);
    //                Player.SetActive(false);
    //                WinPlayer.SetActive(true);
    //                fading.Fade(Fading.type.FadeOut);
    //                fading.OnFadeEnd.AddListener(() =>
    //                {
    //                    //  fading.fading_time = 3.0f;
    //                    Text_Katimake.SetActive(true);
    //                    Text_Katimake.GetComponent<Image>().sprite =Kati;

    //                    Debug.Log("����");
    //                    Mark.SetActive(false);




    //                    Debug.Log("Text_SetActive");
    //                    Invoke("Text_SetActive", 3.0f);

    //                    fading.Fade(Fading.type.FadeIn);
    //                    isStart = true;
    //                    EnemyObject[enemyCount].SetActive(false);
    //                    // �G�l�~�[�̃J�E���g��i�߂�
    //                    enemyCount++;
    //                    Debug.Log(enemyCount);

    //                    EnemyObject[enemyCount].SetActive(true);
    //                    Debug.Log(enemyCount);
    //                    transform.position = new Vector3(-teleportDistancewin, currentPosition.y, currentPosition.z);
    //                    WinPlayer.SetActive(false);
    //                    Player.SetActive(true);
    //                });
    //                //�}�b�v�ړ�
    //                if (EnemyObject[enemyCount] == EnemyObject[1])
    //                {
    //                    fading.fading_time = 0.5f;
    //                    fading.Fade(Fading.type.FadeOut);
    //                    fading.OnFadeEnd.AddListener(() =>
    //                    {
    //                        MainVisual.GetComponent<Image>().sprite = Back1;
    //                        EnemyObject[enemyCount].SetActive(true);
    //                    });
    //                    //fading.Fade(Fading.type.FadeIn);
    //                }
    //                //�}�b�v�ړ�����
    //                if (EnemyObject[enemyCount] == EnemyObject[3])
    //                {
    //                    fading.fading_time = 0.5f;
    //                    fading.Fade(Fading.type.FadeOut);
    //                    fading.OnFadeEnd.AddListener(() =>
    //                    {
    //                        MainVisual.GetComponent<Image>().sprite = Back2;
    //                        EnemyObject[enemyCount].SetActive(true);
    //                        multi.ChooseSongs_BGM(1);
    //                    });
    //                }
    //                Reset_();
    //            }
    //                break;
    //        case Stete.play:
    //            // �X�R�A�����Z
    //            score += playertime;
    //            Score.text = score.ToString();
    //            // Invoke("TextScoreRestart", 0.5f);
    //            Reset_();
    //            break;
    //    }
    //}
    Stete stete;
    public GameObject[] EnemyObject;
    public GameObject[] winEnemyObject;
    public GameObject[] loseEnemyObject;
    public GameObject WinPlayer;
    public GameObject Player;
    public GameObject losePlayer;
    [SerializeField] GameObject MainVisual;
    [SerializeField] Sprite Back1;
    [SerializeField] Sprite Back2;
    [SerializeField] SceneObject scene;
    [SerializeField] SceneObject scene1;
    [SerializeField] multiAudio multi;
    //[SerializeField] Text Syouri;
    [SerializeField] GameObject Text_Katimake;
    [SerializeField] GameObject Mark;
    [SerializeField] Text Score;

    [SerializeField] Sprite Kati;
    [SerializeField] Sprite Make;
    [SerializeField] Sprite Hikiwake;


    //[SerializeField] Sprite Back3;
    //���������ړ�����
    public float teleportDistancewin = 7f;
    //���������ړ�����
    public float teleportDistancelose = 4f;

    // ���ԂɂȂ����瑁����
    public float lapTime;
    // �w�����鎞��
    public float fightStartTime = 5;
    //�@�^�C�������Z�b�g
    public bool resettime;
    //�G�����̓|������
    public int enemyCount = 0;

    //�@�G�̍U��
    public float[] Enemy;
    //�@player�̍U��
    public float playertime;

    public bool isSpace = false;

    public static float score = 0.0f;
    public Fading fading;
    [SerializeField] GameObject ScoreOb;
    //  �X�^�[�g
    public bool isStart = false;

    void Start()
    {
        multi = GameObject.FindAnyObjectByType<multiAudio>().GetComponent<multiAudio>();
        multi.ChooseSongs_BGM(0);
        fading = GameObject.FindAnyObjectByType<Fading>().GetComponent<Fading>();
        lapTime = 0;
        resettime = false;
        EnemyObject[enemyCount].SetActive(true);
        //EnemyObject[enemyCount].SetActive(false);
        fightStartTime = Random.Range(4.0f, 7.0f);
        score = 0.0f;
        WinPlayer.SetActive(false);
        Player.SetActive(true);
        fading.Fade(Fading.type.FadeIn);
    }

    void Update()
    {
        if (lapTime < fightStartTime && Input.GetKey(KeyCode.Space) && isStart && isSpace == false)
        {
            Debug.Log("�t���C���O�I�I");
            Player.SetActive(false);
            WinPlayer.SetActive(true);
            isSpace = true;
            multi.ChooseSongs_SE(1);
        }
        if (isStart == true)
        {
            lapTime += Time.deltaTime;
        }



        //�e�X�g
        //Debug.Log("�o�ߎ���: " + LapTime.ToString("F1") + " �b");


        // �������J�n�A���ԃ��Z�b�g
        if (lapTime < fightStartTime)
        {
            return;
        }
        if (lapTime >= fightStartTime)
        {
            Debug.Log("!!");
            Mark.GetComponent<AudioSource>().clip = multi.audioClipSE[0];
            Mark.SetActive(true);

        }

        // �v���C���[�̎��Ԃ𑝉�
        playertime += Time.deltaTime;
        //Debug.Log("!!");
        //multi.ChooseSongs_SE(0);

        // ���������̔���
        if (Input.GetKey(KeyCode.Space) && isSpace == false)
        {
            //����U������
            multi.ChooseSongs_SE(2);
            //�ړ�
            Vector3 currentPosition = transform.position;
            Vector3 enemyPosition = EnemyObject[enemyCount].transform.position;
            Debug.Log("�o�ߎ���: " + playertime.ToString("F1") + " �b");
            //��������
            if (Enemy[enemyCount] > playertime)
            {
                transform.position = new Vector3(teleportDistancewin, currentPosition.y, currentPosition.z);
                Debug.Log(transform.position);
                //EnemyObject[enemyCount].transform.position = new Vector3(teleportDistancelose, enemyPosition.y, enemyPosition.z);
                Player.SetActive(false);
                WinPlayer.SetActive(true);
                EnemyObject[enemyCount].SetActive(false);
                loseEnemyObject[enemyCount].SetActive(true);
                fading.Fade(Fading.type.FadeOut);
                fading.OnFadeEnd.RemoveAllListeners(); // �ǉ�����O�ɃN���A

                fading.OnFadeEnd.AddListener(() =>
                {
                    //  fading.fading_time = 3.0f;
                    Text_Katimake.SetActive(true);

                    Debug.Log("����");
                    //Syouri.text = "����";
                    Mark.SetActive(false);




                    Debug.Log("Text_SetActive");
                    Invoke("Text_SetActive", 2.0f);


                    fading.Fade(Fading.type.FadeIn);
                    isStart = true;



                    loseEnemyObject[enemyCount].SetActive(false);
                    // �G�l�~�[�̃J�E���g��i�߂�
                    enemyCount++;
                    Debug.Log(enemyCount);
                    EnemyObject[enemyCount].SetActive(true);
                    loseEnemyObject[enemyCount].SetActive(false);
                    Debug.Log(enemyCount);
                    transform.position = currentPosition;
                    Debug.Log(transform.position);
                    WinPlayer.SetActive(false);
                    Player.SetActive(true);


                });
                //�}�b�v�ړ�
                if (EnemyObject[enemyCount] == EnemyObject[1])
                {
                    fading.fading_time = 3.3f;
                    fading.Fade(Fading.type.FadeOut);

                    fading.OnFadeEnd.AddListener(() =>
                    {
                        MainVisual.GetComponent<Image>().sprite = Back1;
                        //EnemyObject[enemyCount].SetActive(true);
                    });
                    //fading.Fade(Fading.type.FadeIn);
                }
                //�}�b�v�ړ�����
                if (EnemyObject[enemyCount] == EnemyObject[3])
                {
                    fading.fading_time = 3.3f;
                    fading.Fade(Fading.type.FadeOut);

                    fading.OnFadeEnd.AddListener(() =>
                    {
                        MainVisual.GetComponent<Image>().sprite = Back2;
                        //EnemyObject[enemyCount].SetActive(true);
                        multi.ChooseSongs_BGM(1);
                    });
                }
                //�N���A
                if (EnemyObject[enemyCount] == EnemyObject[4])
                {
                    Debug.Log("�N���A");
                    fading.fading_time = 3.3f;
                    fading.Fade(Fading.type.FadeOut);
                    fading.OnFadeEnd.RemoveAllListeners(); // �ǉ�����O�ɃN���A

                    fading.OnFadeEnd.AddListener(() =>
                    {
                        SceneManager.LoadScene(scene);
                    });
                    //fading.Fade(Fading.type.FadeIn);
                }

                //if (!EnemyObject[enemyCount].activeSelf)
                //{
                //    SceneManager.LoadScene(scene);
                //}
                // �X�R�A�����Z
                score += playertime;
                Score.text = "�X�R�A��" + score.ToString("F1");
                // Invoke("TextScoreRestart", 0.5f);

            }
            else if (Enemy[enemyCount] == playertime)
            {
                Text_Katimake.SetActive(true);
                Debug.Log("��������");
                Text_Katimake.GetComponent<Image>().sprite = Hikiwake;

                //Syouri.text = "��������";


            }
            Reset_();
        }
        if (lapTime - fightStartTime > Enemy[enemyCount])
        {
            Text_Katimake.SetActive(true);
            Player.SetActive(false);
            WinPlayer.SetActive(false);
            losePlayer.SetActive(true);
            EnemyObject[enemyCount].SetActive(false);
            winEnemyObject[enemyCount].SetActive(true);
            Debug.Log("����");
            //Syouri.text = "����";
            Mark.SetActive(false);
            Text_Katimake.GetComponent<Image>().sprite = Make;

            fading.fading_time = 3.3f;
            fading.Fade(Fading.type.FadeOut);
            fading.OnFadeEnd.RemoveAllListeners(); // �ǉ�����O�ɃN���A

            fading.OnFadeEnd.AddListener(() =>
            {
                SceneManager.LoadScene(scene1);
            });
            //fading.Fade(Fading.type.FadeIn);

            Reset_();
        }

    }

    /// <summary>
    /// ���Z�b�g
    /// </summary>
    void Reset_()
    {
        Debug.Log("Rest");
        lapTime = 0;
        playertime = 0;
        isStart = false;
        isSpace = false;
        fightStartTime = Random.Range(4.0f, 7.0f);
    }
    public void Text_SetActive()
    {
        Text_Katimake.SetActive(false);

    }
    public void TextScoreRestart()
    {
        ScoreOb.SetActive(false);
    }
}
