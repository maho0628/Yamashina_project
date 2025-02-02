using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shageki_UI : MonoBehaviour
{
    public Text timerText;
    public Text ammoText;
    public Text countDownText;
    public Text scoreText;

    public void TimeTextUpdate(float _time)
    {
        float truncatedValue = Mathf.Round(_time * 100) / 100f; ;



        timerText.text = truncatedValue.ToString();
    }

    public void SetAmmoCount(int maxAmmo, int curAmmo)
    {
        ammoText.text = "‚½‚Ü:" + curAmmo.ToString() + "/" + maxAmmo.ToString();
    }

    public void SetscoreText(int _score)
    {
        scoreText.text = _score.ToString();
    }
}
