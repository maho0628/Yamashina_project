using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonStateChange : MonoBehaviour
{
    //ボタンを押したときゲームステートをShowOptionにする
    public void ChangeStateShowOption()
    {
        GameManager.ChangeState(GameState.ShowOption);
    }
    public void ChangeStateShowHint()
    {
        GameManager.ChangeState(GameState.Hint);
    }
    //ボタンを押したときゲームステートをMainにする
    public void ChangeStateMain()
    {
        GameManager.ChangeState(GameState.Main);
    }
}
