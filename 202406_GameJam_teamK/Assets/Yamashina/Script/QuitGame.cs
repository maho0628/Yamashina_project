using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGame : MonoBehaviour
{
    //�Q�[���I��:�{�^������Ăяo��
    public void EndGame()
    {
       

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;//�Q�[���v���C�I��
        //Application.quitting += VolumeSave;


#else
    Application.Quit();//�Q�[���v���C�I��
#endif
    }
    //public void VolumeSave()
    //{
    //    multiAudio.BgmSave();
    //    multiAudio.SeSave();
    //}
}