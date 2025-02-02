using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Toshiki
{
    public class SoundManager : MonoBehaviour
    {
        public static SoundManager instance;

        AudioSource audioSourceBGM;
        AudioSource[] audioSourceListSE = new AudioSource[30];
        [SerializeField] AudioClip TitleBGM;
        [SerializeField] AudioClip PlayBGM;
        [SerializeField] AudioClip ResultBGM;
        [SerializeField] AudioClip ShotSE;
        [SerializeField] AudioClip HitToEnemySE;
        [SerializeField] AudioClip ReadyGunSE;
        [SerializeField] AudioClip ReloadSE;
        [SerializeField] AudioClip ButtonClickSE;
        [SerializeField] AudioClip magInSE;
        [SerializeField] AudioClip chemberSE;
        [SerializeField] AudioClip emptySE;

        [SerializeField] AudioClip[] HitMetalSounds;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }

            audioSourceBGM = gameObject.AddComponent<AudioSource>();
            audioSourceBGM.loop = true;
            for (int i = 0; i < audioSourceListSE.Length; ++i)
            {
                audioSourceListSE[i] = gameObject.AddComponent<AudioSource>();
            }
        }

        //未使用のAudioSourceを取得
        public AudioSource GetUnusedAudioSource()
        {
            for (int i = 0; i < audioSourceListSE.Length; ++i)
            {
                if (audioSourceListSE[i].isPlaying == false) return audioSourceListSE[i];
            }

            return null; //未使用のAudioSourceはなし
        }

        //指定されたAudioClipを未使用のAudioSourceで再生
        public void Play(string name)
        {
            var audioSource = GetUnusedAudioSource();
            if (audioSource == null)
            {
                Debug.Log("BGM, SEが再生できない");
                return; //再生できない
            }

            switch (name)
            {
                //BGM
                case "TitleBGM":
                    audioSourceBGM.clip = TitleBGM;
                    audioSourceBGM.Play();
                    break;
                case "PlayBGM":
                    audioSourceBGM.clip = PlayBGM;
                    audioSourceBGM.Play();
                    break;
                case "ResultBGM":
                    audioSourceBGM.clip = ResultBGM;
                    audioSourceBGM.Play();
                    break;
                case "BGMストップ":
                    audioSourceBGM.Stop();
                    break;
                //効果音
                case "Shot":    //射撃
                    audioSource.PlayOneShot(ShotSE);
                    break;
                case "HitBullet":   //弾が敵に当たる
                    audioSource.PlayOneShot(HitToEnemySE);
                    break;
                case "ReadyGun":    //銃を構える
                    audioSource.PlayOneShot(ReadyGunSE);
                    break;
                case "Reload":    //銃を構える
                    audioSource.PlayOneShot(ReloadSE);
                    break;
                case "ButtonClick":    //ボタンクリック
                    audioSource.PlayOneShot(ButtonClickSE);
                    break;
                case "MetalHit":   //弾が敵に当たる
                    int tmp = Random.Range(0, 2);
                    audioSource.PlayOneShot(HitMetalSounds[tmp]);
                    break;
                case "magIn":    
                    audioSource.PlayOneShot(magInSE);
                    break;
                case "chember":    
                    audioSource.PlayOneShot(chemberSE);
                    break;
                case "empty":
                    audioSource.PlayOneShot(emptySE);
                    break;
            }
        }
    }
}