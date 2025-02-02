using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Toshiki
{
    public class Shageki_mato : MonoBehaviour, IHit
    {
        public bool isHit = false;

        public float lifeTime;
        public float timer;
        public GameObject hitEffect;

        public int score;

        public GameObject canvas;
        public Text score_text;

        private void Start()
        {
            score_text.text = score.ToString();
        }

        private void Update()
        {
            if (isHit)
            {
                lifeTime -= Time.deltaTime;

                if (lifeTime < 0)
                    Destroy(gameObject);
            }
        }

        public void Hit()
        {
            score_text.text = "";

            Shageki_Manager.instance.AddScore(score);
            //Instantiate(hitEffect, transform.position, Quaternion.identity);

            SoundManager.instance.Play("MetalHit");
            isHit = true;
            Rigidbody2D rb = gameObject.AddComponent<Rigidbody2D>();
            Destroy(transform.gameObject.GetComponent<Collider2D>());
            rb.AddForce(Vector2.up * 5f, ForceMode2D.Impulse);
            rb.AddTorque(Random.Range(-5000f, 5000f));
            rb.gravityScale = 3f;
        }

        public void AddScore(int _add)
        {
            score += _add;
            score_text.text = score.ToString();
        }
    }
}
