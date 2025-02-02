using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Toshiki
{
    public class Shageki_ammoObj : MonoBehaviour
    {
        public float lifeTime = 1f;
        public float speed = 1f;//減少させる倍率

        public float gravity;
        public float upForce;

        public GameObject hitEffect;

        public void SetAmmoData(float _lifeTime, float _gravity)
        {
            lifeTime = _lifeTime;
            gravity = _gravity;
        }

        private void Start()
        {
            Vector3 tmp = new Vector3(0, upForce, 0);
            GetComponent<Rigidbody2D>().gravityScale = gravity;
            GetComponent<Rigidbody2D>().AddForce(tmp, ForceMode2D.Impulse);
        }

        private void Update()
        {
            lifeTime -= Time.deltaTime * speed;


            transform.localScale = new Vector3(lifeTime, lifeTime, lifeTime);

            if (lifeTime < 0)
            {
                Chakudan();
            }
        }

        void Chakudan()
        {
            Vector3 origin = transform.position; // 原点
            Vector3 direction = new Vector3(0, 0, 1); // X軸方向を表すベクトル

            Ray2D ray = new Ray2D(transform.position, direction); // Rayを生成、-transform.upは進行方向
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, 1f);//Raycastを生成

            if (hit.collider != null)
            {
                hit.collider.gameObject.GetComponent<IHit>().Hit();

                Instantiate(hitEffect, this.transform.position, Quaternion.identity);
            }


            Destroy(gameObject);
        }
    }


}