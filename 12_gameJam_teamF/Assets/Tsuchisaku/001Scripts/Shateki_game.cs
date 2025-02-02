using DG.Tweening;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace Toshiki
{
    public class Shateki_game : MonoBehaviour
    {
        [Header("(たまかず)")]
        public int maxAmmo;
        public int ammo;
        public float timeUntilHit;
        public float shootCoolTime;
        public float coolTimer;
        [SerializeField] bool nowReload = false;


        public Shageki_UI ui;
        public GameObject aimPoint;
        public Transform aimPointTr;
        public Transform trueAimPointTr;
        public GameObject ammoObj;
        public GameObject emptyAmmoPref;
        [Header("(Weapon)")]
        public Transform emptyAmmoPos;
        public Shageki_CharaAnimator anim;
        public GameObject gunEffect;

        [Header("(AimPoint)")]
        public Transform spineAimPoint;
        [Header("(shootPointB)")]
        public Transform shootPoint;
        public Vector3 spineAimStartPos;

        [Header("for ShakeCam")]
        public Camera cam;
        public float camShakeDuration = .2f;
        public float camShakeStrength = .2f;

        [Header("Score")]
        public static int scoreGameC;

        //public Shageki_AimCircleAnimation aimCircleAnimation;
        public Animator circleAnimator;


        // Start is called before the first frame update
        void Start()
        {
            SetAmmoCount();
            aimPoint.SetActive(false);
            spineAimStartPos = spineAimPoint.localPosition;
            maxAmmo = Shageki_Manager.instance.ammo_maxAmmo;


            //aimCircleAnimation.DoAnim();
        }

        public void LocalUpdate()
        {
            if (coolTimer > 0)
            {
                coolTimer -= Time.deltaTime;
            }

            SetAimPoint();

            if (nowReload)
            {
                if (anim.endReloadTrigger == true)
                {
                    aimPoint.SetActive(true);
                    anim.SetAnim(Anim.Hold);
                    nowReload = false;
                    anim.endReloadTrigger = false;
                }
            }

            if (nowReload)
                return;

            if (Input.GetMouseButton(0))
            {





                if (coolTimer > 0)
                {
                    //Debug.Log($"now coolTime");
                    return;
                }
                coolTimer = shootCoolTime;
                if (ammo <= 0)
                {
                    SoundManager.instance.Play("empty");
                    return;
                }
                Shoot();
            }
            else if (Input.GetKeyDown(KeyCode.R) || Input.GetMouseButtonDown(1))
            {
                if (ammo == maxAmmo)
                    return;

                Reload();
            }


        }

        void SetAmmoCount()
        {
            ui.SetAmmoCount(maxAmmo, ammo);
        }

        void SetAimPoint()
        {
            aimPoint.transform.position
             = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f));

            Vector3 offcet = new Vector3(0, Mathf.Sin(Time.time), 0);


            aimPointTr.localPosition = offcet;

            //Spineモデルのボーンと同期させる
            if (spineAimPoint == null)
                return;

            //変化量

            //ScreenCenerとの差
            Vector3 offcetA = aimPointTr.position - Vector3.zero;

            float _x = 0.18f;
            float _y = 0.3f;

            Vector3 result = new Vector3(offcetA.x * _x, offcetA.y * _y, 0);

            spineAimPoint.localPosition = spineAimStartPos + result;

        }

        public void Shoot()
        {
            SoundManager.instance.Play("Shot");
            anim.SetAnim(Anim.Shoot);

            ShakeCam();



            ammo--;
            if (ammo < 1)
            {
                Debug.Log($"boltStop");
                //boltStop
                anim.SetBoltStop(true);
            }

            SetAmmoCount();
            //TODO GunSound

            //Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 worldPos = aimPointTr.transform.position;

            worldPos.z = 10;


            Instantiate(emptyAmmoPref, emptyAmmoPos.position, Quaternion.identity);

            //弾の射出

            Instantiate(ammoObj, worldPos, Quaternion.identity).GetComponent<Shageki_ammoObj>().SetAmmoData(Shageki_Manager.instance.ammo_lifeTime, Shageki_Manager.instance.ammo_graity);

            //サイトのアニメーションを再生するs
            circleAnimator.SetTrigger("Shot");

            if (shootPoint != null)
            {
                Instantiate(gunEffect, shootPoint.position, emptyAmmoPos.rotation);
            }
        }

        public void Reload()
        {
            anim.SetBoltStop(false);
            aimPoint.SetActive(false);
            SoundManager.instance.Play("Reload");
            nowReload = true;
            anim.SetAnim(Anim.Reload);
            ammo = maxAmmo;
            SetAmmoCount();
        }

        void ShakeCam()
        {
            cam.gameObject.transform.DOShakePosition(camShakeDuration, camShakeStrength);
        }
    }
}