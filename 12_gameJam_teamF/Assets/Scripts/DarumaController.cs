using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Toshiki;

namespace Shageki
{
    public class DarumaController : MonoBehaviour, IHit 
    {
        public GameObject[] darumaParts; // �_���}�̊e�p�[�c���i�[���邽�߂̔z��
        public float gravityScaleMultiplier = 2.0f; // GravityScale�̔{��

        public GameObject canvas;
        public Text score_text;
        public int score;
        public bool isHit = false;

        void Start()
        {
            score_text.text = score.ToString();
        }

        public void Hit()
        {
            if (isHit)
            {
                return;
            }
            isHit = true;

            Shageki_Manager.instance.AddScore(score);
            score_text.text = "";
            foreach (GameObject part in darumaParts)
            {
                // �p�[�c��Rigidbody2D��ǉ�
                Rigidbody2D rb = part.AddComponent<Rigidbody2D>();

                // GravityScale����������
                rb.gravityScale *= gravityScaleMultiplier;

                // �p�[�c�Ƀ����_���ȗ͂�������
                Vector2 randomForce = new Vector2(Random.Range(-5f, 5f), Random.Range(5f, 10f));
                rb.AddForce(randomForce, ForceMode2D.Impulse);

                // �p�[�c�Ƀ����_���ȉ�]��������
                rb.AddTorque(Random.Range(-5000f, 5000f));
            }

            // 1�b��Ɋe�p�[�c���폜���鏈��
            Invoke("DestroyDarumaParts", 1f);
        }

        public void AddScore(int _add)
        {
            score += _add;
            score_text.text = score.ToString();
        }


        void DestroyDarumaParts()
        {
            foreach (GameObject part in darumaParts)
            {
                // �p�[�c���폜
                Destroy(part);
            }
        }
    }
}
