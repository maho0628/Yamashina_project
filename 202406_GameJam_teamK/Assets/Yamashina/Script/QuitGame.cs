using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGame : MonoBehaviour
{
    //ゲーム終了:ボタンから呼び出す
    public void EndGame()
    {
       

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;//ゲームプレイ終了
        //Application.quitting += VolumeSave;


#else
    Application.Quit();//ゲームプレイ終了
#endif
    }
    //public void VolumeSave()
    //{
    //    multiAudio.BgmSave();
    //    multiAudio.SeSave();
    //}
}