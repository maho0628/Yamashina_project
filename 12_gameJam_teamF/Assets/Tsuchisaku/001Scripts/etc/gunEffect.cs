using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Toshiki
{
    public class gunEffect : MonoBehaviour
    {
        public SpriteRenderer smoke;
        public SpriteRenderer llightObj;



        public float lifeTime = 1f;
        public float lifeTimer;

        public byte minus;

        private void Start()
        {
            StartCoroutine("Transparent");
        }


        private void Update()
        {
            lifeTimer += Time.deltaTime;


            if(lifeTimer > lifeTime)
            {
                Destroy(gameObject);
            }
        }

        IEnumerator Transparent()
        {
            for (int i = 0; i < 255; i++)
            {
                smoke.color = smoke.color - new Color32(0, 0, 0, minus);
                llightObj.color = llightObj.color - new Color32(0, 0, 0, minus);
                

                yield return new WaitForSeconds(0.01f);
            }
        }

    }
}