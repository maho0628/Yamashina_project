using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forcedresolutiondesignation : MonoBehaviour

{ //実行ファイル起動時にエディターで表示しているUIの位置がおかしくなる問題→原因Unityが前に開いた時の画面の解像度を記憶していて変わらないことがある。どうしてもうまくいかない場合の解像度の強制指定スクリプト

    public int ScreenWidth;
    public int ScreenHeight;

    void Awake()
    {
        // PC向けビルドだったらサイズ変更
        if (Application.platform == RuntimePlatform.WindowsPlayer ||
        Application.platform == RuntimePlatform.OSXPlayer ||
        Application.platform == RuntimePlatform.LinuxPlayer)
        {
            Screen.SetResolution(ScreenWidth, ScreenHeight, false);
        }
    }
}