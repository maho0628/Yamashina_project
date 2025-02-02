using System.Collections;
using System.Collections.Generic;
using Toshiki;
using UnityEngine;

public class CS_WaveController : MonoBehaviour
{
    [SerializeField, Header("���̊")]
    private float widthMax = 9;

    [SerializeField, Header("Y�̈ʒu")]
    private float[] instanceY;

    [SerializeField, Header("��������I�u�W�F�N�g")]
    private GameObject[] matoPrefabs;

    [SerializeField, Header("�������鐶���}�b�N�X")]
    private int instanceMax = 3;

    [SerializeField, Header("�؂�ւ��鎞��")]
    private float resetTime = 10;

    [SerializeField, Header("�I�u�W�F�N�g����������")]
    private float allMatoDestroyTime = 5;

    [SerializeField, Header("�����̂Ƃ��I�u�W�F�N�g����������")]
    private float allMatoDestroyTime2 = 3;

    [SerializeField, Header("�v���C����")]
    private float playTime = 60;

    private float outTimer = 0;
    private float timer = 0;
    private bool isWaveStart = false;
    private bool isRandomWave = false;
    public bool WaveEnd = false;

    GameObject[] game;

    void Start()
    {
    }

    public void WaveStart()
    {
        // ��������
        Spawner();
        isWaveStart = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (WaveEnd)
        {
            return;
        }
        if (game == null)
        {
            return;
        }
        if (isWaveStart)
        {
            outTimer += Time.deltaTime;
            if (isRandomWave)
            {
                // ���Ԍo�߂��v��
                timer += Time.deltaTime;
                if (outTimer > playTime / 2)
                {
                    // ���Z�b�g�^�C���𒴂����炷�ׂẴI�u�W�F�N�g���폜
                    if (timer > allMatoDestroyTime2)
                    {
                        AllMatoDestroy();
                    }
                }
                else
                {
                    // ���Z�b�g�^�C���𒴂����炷�ׂẴI�u�W�F�N�g���폜
                    if (timer > allMatoDestroyTime)
                    {
                        AllMatoDestroy();
                    }
                }
            }
            // �I�u�W�F�N�g�����݂��Ă��邩
            int tmp = 0;
            for (int i = 0; i < game.Length; ++i)
            {
                if (game[i] == null)
                {
                    tmp++;
                }
            }
            if (tmp >= game.Length)
            {
                Spawner();
            }

        }
    }

    // ��������
    private void Spawner()
    {
        game = new GameObject[3];
        // �����_���E�F�[�u�̏ꍇOff
        if (isRandomWave)
        {
            isRandomWave = false;
        }

        int wavePattern = Random.Range(0, matoPrefabs.Length);

        switch (wavePattern)
        {
            case 0:
                //�����_���X�|�[��
                RandomSpawan();
                break;
            case 1:
                MoveSpawan();
                break;
        }
        timer = 0;

    }

    // �����_������
    private void RandomSpawan()
    {
        for (int i = 0; i < instanceMax; ++i)
        {
            Vector3 pos = new Vector3(
                Random.Range(-widthMax, widthMax),
                instanceY[Random.Range(0, instanceY.Length)],
                0);
            int rnd = Random.Range(0, 2);
            Debug.Log(rnd);
            game[i] = Instantiate(matoPrefabs[rnd], pos, Quaternion.identity);
        }
        isRandomWave = true;
    }

    // �ړ�����
    private void MoveSpawan()
    {
        for (int i = 0; i < instanceMax; ++i)
        {
            int tmp = Random.Range(0, 2);
            float x = 0;
            if (tmp == 0)
            {
                x = widthMax + 1 + i;
            }
            else if (tmp != 0)
            {
                x = (widthMax + 1 + i) * -1;
            }
            Vector3 pos = new Vector3(
                x,
                instanceY[Random.Range(0, instanceY.Length)],
                0);
            game[i] = Instantiate(matoPrefabs[2], pos, Quaternion.identity);
            if (outTimer > playTime / 2)
            {
                game[i].GetComponent<CS_MoveMato>().Speed = 5.0f;

                game[i].GetComponent<Shageki_mato>().AddScore(Shageki_Manager.instance.point_Fast);
            }
            else
            {
                game[i].GetComponent<CS_MoveMato>().Speed = 2.0f;
            }

        }
    }

    // ���ׂẴI�u�W�F�N�g���폜
    private void AllMatoDestroy()
    {
        for (int i = 0; i < game.Length; ++i)
        {
            Destroy(game[i]);
        }
        //Invoke("Spawner", 1);
        Spawner();
    }
}
