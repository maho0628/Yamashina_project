using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoTrueEndCaseOfRaft : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKey(KeyCode.T))
        {
            SceneManager.LoadScene("TrueEndingScene_CaseOfRaft");

        }
        if (Input.GetKey(KeyCode.I))
        {
            PlayerInfo.Instance.Inventry.GetItem(Items.Item_ID.item_craft_raft, 1);
        }
    }
}
