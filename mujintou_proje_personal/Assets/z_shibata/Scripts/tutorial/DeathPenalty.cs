using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPenalty : MonoBehaviour
{
    public void DeathPenaltyButton()
    {
        PlayerInfo.Instance.Health = 0;
    }
}
