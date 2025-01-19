using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OKButtonPurpose : MonoBehaviour
{
    GameObject ok;
    // Start is called before the first frame update
    void Start()
    {
        ok = GameObject.Find("TutorialPurpose");
    }
    public void PurposeButton(int num)
    {
        ok.GetComponent<PurposeSet>().PurSet(num);
    }
}
