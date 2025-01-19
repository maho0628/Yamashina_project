using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurposeSet : MonoBehaviour
{
    [SerializeField] GameObject[] purpose;
    public void PurSet(int num)
    {
        switch (num)
        {
            case 0:
                purpose[num].SetActive(true);
                break;
            case 1:
                purpose[num-1].SetActive(false);
                purpose[num].SetActive(true);
                break;
            case 2:
                purpose[num-1].SetActive(false);
                break;
            case 3:
                purpose[num].SetActive(true);
                break;

        }

    }
}
