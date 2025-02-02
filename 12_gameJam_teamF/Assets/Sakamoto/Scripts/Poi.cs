using Skamoto;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Sakamoto
{

    public class Poi : MonoBehaviour
    {
        [Header("金魚1匹の点数")]
        public int iScorepoint_kingyio;
        [Header("出目金1匹の点数(なぜか入力した数字の3倍が加算されます)")]
        public int iScorepoint_Demekin;
        //Kingyo_Posを使うため
        public Kingyo[] kingyo = new Kingyo[3];
        //public Kingyo2 kingyo2;
        public Game_Maneger gameManeger;
        [SerializeField] Animator[] Kingyo_Animation;
        [SerializeField] Animator Poi_Animation;
        [SerializeField] AudioSource Poi_SE;
        [SerializeField] AudioClip Get;
        [SerializeField] AudioClip Mis;
        Transform Poi_Trnsform;
        public Vector3 Poi_pos;
        //ポイのエリア内に金魚が入っているかのフラグ
        [SerializeField]
        bool Check_elia_f;
        bool Poi_f;
        float Poi_f_interval;
        [SerializeField] float Poi_f_interval_time;

        // Watanabe
        string name;
        [SerializeField]
        CS_BigAiGoldFish cs_BigAiGoldFish;
        [SerializeField]
        Animator anim_BigAiGoldFish;
        [SerializeField] bool poiBreak = false;
        bool isGet = false;

        // Start is called before the first frame update
        void Start()
        {
            Poi_f = true;
            Poi_Trnsform = this.transform;
            Poi_pos = Poi_Trnsform.position;
            Poi_SE = GetComponent<AudioSource>();

        }

        private void Update()
        {
            if (!Poi_f)
            {
                Poi_f_interval -= Time.deltaTime;
                if (Poi_f_interval <= 0)
                {
                    Poi_f = true;
                }
            }


            if (gameManeger.Game_Main_f)
            {

                if (UnityEngine.Input.GetMouseButtonDown(0) && Poi_f && !poiBreak)
                {
                    Poi_f = false;
                    for (int i = 0; i < kingyo.Length; i++)
                    {
                        // 出目金
                        if (Check_elia_f && (Vector3.Distance(new Vector3(Poi_pos.x, Poi_pos.y + 110, Poi_pos.z), cs_BigAiGoldFish.GetPos) < 140) && name == "BigAiGoldFish")
                        {
                            Poi_Animation.SetTrigger("Up_f");
                            gameManeger.AddScore(iScorepoint_Demekin);
                            anim_BigAiGoldFish.SetTrigger("Kingyo_Catch_f");

                            // 適当に画面がいにやる
                            cs_BigAiGoldFish.transform.position = new Vector3(0, -2000, 0);

                            Poi_SE.PlayOneShot(Get);
                            //Poi_f_interval = Poi_f_interval_time;
                            isGet = true;
                        }

                        if (Check_elia_f && (Vector3.Distance(new Vector3(Poi_pos.x, Poi_pos.y + 110, Poi_pos.z), kingyo[i].Kingyo_pos) < 140 ))
                        {
                            Poi_Animation.SetTrigger("Up_f");
                            //Debug.Log("捕まえた");
                            gameManeger.AddScore(iScorepoint_kingyio);
                            Kingyo_Animation[i].SetTrigger("Kingyo_Catch_f");


                            kingyo[i].Send_Catch();

                            Poi_SE.PlayOneShot(Get);
                            //Poi_f = false;
                            isGet = true;
                        }

                        if (Check_elia_f && !isGet)
                        {
                            // 魚を捕まえたらポイは割れない
                            //if (!isGet)
                            {
                                Poi_Animation.SetTrigger("Break_f");
                                Debug.Log("ポイが破れた");

                                Poi_SE.PlayOneShot(Mis);
                                poiBreak = true; // ポイが壊れた
                                                 //Poi_f = false;
                                                 //Poi_f_interval = Poi_f_interval_time;
                            }
                        }
                        else
                        {
                            Poi_Animation.SetTrigger("Up_f");
                            Debug.Log("空ぶった");
                            Poi_SE.PlayOneShot(Mis);
                            //Poi_f = false;
                            //Poi_f_interval = Poi_f_interval_time;

                        }

                    }
                    Poi_f_interval = Poi_f_interval_time;
                }
            }
        }

        private void PoiOk()
        {
            poiBreak = false;
        }

        private void GetFlgFalse()
        {
            isGet = false;
        }

        // オブジェクトが重なったとき
        void OnTriggerEnter2D(Collider2D other)
        {
            Check_elia_f = true;
            //Debug.Log("判定範囲に金魚が入った ");
            if (other.name == "BigAiGoldFish")
            {
                name = other.name;
            }
        }
        // オブジェクトが離れた時
        void OnTriggerExit2D(Collider2D other)
        {
            Check_elia_f = false;
            //Debug.Log("判定範囲から金魚が出た");
            if (other.name == "BigAiGoldFish")
            {
                name = "";
            }
        }
        // Update is called once per frame
    }
}
