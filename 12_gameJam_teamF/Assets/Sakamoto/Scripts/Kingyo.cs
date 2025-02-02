using Sakamoto;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Skamoto
{
    public class Kingyo : MonoBehaviour

    {
        public Vector3 toDirection;
        [SerializeField] public Game_Maneger gameManeger;
        [SerializeField] public Transform Target;
        [SerializeField] public Transform[] HaseBasyo;
        int Kingyo_Hassei_Interval;
        bool Kingyo_f;
        float Kingyo_sp;
        public Vector3 Kingyo_pos;

        //Transform Kingyo_Trnsform;
        // Start is called before the first frame update
        void Start()
        {
            Kingyo_Hassei_Interval = Random.Range(0, 2);
            Kingyo_pos = this.transform.localPosition;
            Debug.Log("Start");
            Kingyo_f = false;
            Kingyo_Hassei();
        }

        // Update is called once per frame

        void Update()
        {
            if (gameManeger.Kingyo_Hassei_f)
            {
                if(Kingyo_Hassei_Interval < 0)
                {
                    Kingyo_Hassei();
                    Kingyo_Hassei_Interval = Random.Range(0, 2);
                }
                Kingyo_Hassei_Interval--;
                if (Kingyo_f)
                {
                   
                    transform.position += transform.up * 500.0f /** Kingyo_sp*/ * Time.deltaTime;
                    Kingyo_pos = transform.position; 
                    if (transform.localPosition.y < -800 || transform.localPosition.x < -1700 || 1700 < transform.localPosition.x )
                    {
                        Debug.Log("フラグがファルスになりました" +  transform.position.y);
                        Kingyo_f = false;
                    };
                }
                //if (UnityEngine.Input.GetMouseButtonUp(0))
                //{
                //    if (-200 < Kingyo_pos.x && Kingyo_pos.x < 200 && -360 < Kingyo_pos.y && Kingyo_pos.y < -60)
                //    {
                //        if (-100 < Kingyo_pos.x && Kingyo_pos.x < 100 && -250 < Kingyo_pos.y && Kingyo_pos.y < -50)
                //        {
                //            Debug.Log("金魚が取れた");
                //        }
                //        else
                //        {
                //            Debug.Log("ポイが破れた");
                //        }
                //    }
                //    else
                //    {
                //        Debug.Log("何も取れなかった");
                //    }
                //}
            }
        }

        void Kingyo_Hassei()
        {

            if (!Kingyo_f)
            {
                Debug.Log("金魚発生フラグが通りました");
                int Kingyo_Hassei_Patarn;

                //Kingyo_sp = Random.Range(1, 2);
                //Debug.Log("スピード乱数取得");
                Kingyo_Hassei_Patarn = Random.Range(0, 6);
                //Debug.Log("乱数取得");
                for (int i = 0; i < HaseBasyo.Length; i++)
                {
                    if (Kingyo_Hassei_Patarn == i)
                    {
                        Kingyo_pos = HaseBasyo[Kingyo_Hassei_Patarn].transform.position;
                        transform.position = Kingyo_pos;
                        //Debug.Log("金魚の場所設定");
                    }
                }

                Get_Vector();
                Kingyo_f = true;

                //Get_Kingyo_Rad(Kingyo_pos, Poi_pos);
                //Set_Kingyo_Spead();
            }

        }
        public void Get_Vector()
        {
            Debug.Log("Get_Vector in");
            toDirection = Target.transform.position - transform.position;
            transform.rotation = Quaternion.FromToRotation(Vector3.up, toDirection);
        }

        public void Send_Catch()
        {
            //kingyo.SetActive(false);
            transform.localPosition = new Vector2(transform.position.x, -1100f);
            //kingyo.SetActive(true);
        }

    }
}
