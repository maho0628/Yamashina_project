using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData 
{
    public void MakeSaveData()
    {
        PlayerInfo info = null;
        if (PlayerInfo.InstanceNullable)
        {
            info = PlayerInfo.Instance;
            player_health = info.Health;
            player_hunger = info.Hunger;
            player_thirst = info.Thirst;
            player_luck = info.Luck;
            player_condition = info.GetConditionRawData();
            day = info.Day.Item2 ? info.Day.Item1 * 2 : (info.Day.Item1 * 2) + 1;
            weather = (int)info.weather;
            fire = info.Fire;
            water = info.Water;
            action = info.ActionValue;
            max_action = info.MaxActionValue;
            firstItemId = info.FirstItemId;
        }
        else
        {
            Debug.Log("PlayerInfoが存在しないためセーブデータを作成できません");
        }
    }
    public int player_health;
    public int player_hunger;
    public int player_thirst;
    public int player_luck;
    public uint player_condition;
    public int day;
    public int weather;
    public int fire;
    public int water;
    public int action;
    public int max_action;
    public int firstItemId;
}
