using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sakamoto;

public class CS_BigAiGoldFish : MonoBehaviour
{
    [SerializeField, Header("�|�C�̈ʒu")]
    private Transform poiTrs;
    private float speed;
    float defaltSpeed = 500;
    [SerializeField, Header("�����ꏊ")]
    private RectTransform[] instanceTrs;
    public Vector3 GetPos
    {
        get
        {
            return poiTrs.position;
        }
    }
    [SerializeField]
    Game_Maneger game_Maneger;
   

    // Boost
    float boostSpeed = 700;
    float timer = 0;
    float boostTimer = 1;

    // UneUne
    Quaternion initializeRotation = Quaternion.identity;
    float sin = 0;

    enum Action
    {
        Boost,
        UneUne
    }
    
   Action action = Action.UneUne;

    void Start()
    {
        // ������
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        // �ړ�����
        Move();
    }

    // �ړ��֐�
    private void Move()
    {
        if (game_Maneger.Kingyo_Hassei_f)
        {

            switch (action)
            {
                case Action.Boost:
                    // �����Ă�������ɐi��
                    transform.position += transform.up * speed * Time.deltaTime;
                    timer += Time.deltaTime;
                    // �r���ŉ�������
                    if (timer > boostTimer)
                    {
                        speed = boostSpeed;
                    }
                    break;
                case Action.UneUne:
                    transform.position += transform.up * speed * Time.deltaTime;
                    timer += Time.deltaTime;
                    break;
            }
        }
    }

    // �����_���Ɏ��̈ʒu�ɉ�]����
    private void RandomRot()
    {
        Vector3 pos = new Vector3(Random.Range(-9, 9),
            Random.Range(2, -2), 0);
        // �Ώە��ւ̃x�N�g�����Z�o
        Vector3 toDirection = pos - transform.position;
        // �Ώە��։�]����
        transform.rotation = Quaternion.FromToRotation(Vector3.up, toDirection);
        initializeRotation = transform.rotation;
    }

    private void Initialize()
    {
        int index = Random.Range(0, instanceTrs.Length);
        transform.position = instanceTrs[index].position;

        Vector3 toDirection = poiTrs.position - transform.position;
        // �Ώە��։�]����
        transform.rotation = Quaternion.FromToRotation(Vector3.up, toDirection);
        initializeRotation = transform.rotation;

        int tmp = Random.Range(-1, 2);

        if(tmp == 0)
        {
            action = Action.Boost;
        }
        else if(tmp == 1)
        {
            action = Action.UneUne;
            Invoke("RandomRot", 2);
        }
        // ��̃X�s�[�h�ɂ���
        speed = defaltSpeed;
        timer = 0;

        int rnd = Random.Range(10, 20);
        Invoke("Initialize", rnd);
    }
}
