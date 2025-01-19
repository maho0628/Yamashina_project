using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialButton : MonoBehaviour
{
    [SerializeField] GameObject tutrialImage;
    public void onTutorialButton()
    {
        Instantiate(tutrialImage);
    }
}
