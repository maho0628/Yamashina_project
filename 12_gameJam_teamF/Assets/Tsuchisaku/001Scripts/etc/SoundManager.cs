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

        //���g�p��AudioSource���擾
        public AudioSource GetUnusedAudioSource()
        {
            for (int i = 0; i < audioSourceListSE.Length; ++i)
            {
                if (audioSourceListSE[i].isPlaying == false) return audioSourceListSE[i];
            }

            return null; //���g�p��AudioSource�͂Ȃ�
        }

        //�w�肳�ꂽAudioClip�𖢎g�p��AudioSource�ōĐ�
        public void Play(string name)
        {
            var audioSource = GetUnusedAudioSource();
            if (audioSource == null)
            {
                Debug.Log("BGM, SE���Đ��ł��Ȃ�");
                return; //�Đ��ł��Ȃ�
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
                case "BGM�X�g�b�v":
                    audioSourceBGM.Stop();
                    break;
                //���ʉ�
                case "Shot":    //�ˌ�
                    audioSource.PlayOneShot(ShotSE);
                    break;
                case "HitBullet":   //�e���G�ɓ�����
                    audioSource.PlayOneShot(HitToEnemySE);
                    break;
                case "ReadyGun":    //�e���\����
                    audioSource.PlayOneShot(ReadyGunSE);
                    break;
                case "Reload":    //�e���\����
                    audioSource.PlayOneShot(ReloadSE);
                    break;
                case "ButtonClick":    //�{�^���N���b�N
                    audioSource.PlayOneShot(ButtonClickSE);
                    break;
                case "MetalHit":   //�e���G�ɓ�����
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