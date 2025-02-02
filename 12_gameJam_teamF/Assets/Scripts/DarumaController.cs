using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Toshiki;

namespace Shageki
{
    public class DarumaController : MonoBehaviour, IHit 
    {
        public GameObject[] darumaParts; // ダルマの各パーツを格納するための配列
        public float gravityScaleMultiplier = 2.0f; // GravityScaleの倍率

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
                // パーツにRigidbody2Dを追加
                Rigidbody2D rb = part.AddComponent<Rigidbody2D>();

                // GravityScaleを強くする
                rb.gravityScale *= gravityScaleMultiplier;

                // パーツにランダムな力を加える
                Vector2 randomForce = new Vector2(Random.Range(-5f, 5f), Random.Range(5f, 10f));
                rb.AddForce(randomForce, ForceMode2D.Impulse);

                // パーツにランダムな回転を加える
                rb.AddTorque(Random.Range(-5000f, 5000f));
            }

            // 1秒後に各パーツを削除する処理
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
                // パーツを削除
                Destroy(part);
            }
        }
    }
}
