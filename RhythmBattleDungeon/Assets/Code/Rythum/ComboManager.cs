using UnityEngine;

public class ComboManager : SingletonMonoBehaviour<ComboManager>
{
    private int currentCombo = 0;
    private int maxCombo = 0;

    public int CurrentCombo => currentCombo;
    public int MaxCombo => maxCombo;



    public void IncrementCombo()
    {
        currentCombo++;
        if (currentCombo > maxCombo)
            maxCombo = currentCombo;
    }

    public void ResetCombo()
    {
        currentCombo = 0;
    }


    public bool IsFullCombo()
    {
        return currentCombo == NoteManager.Instance.GetTotalNoteCount(); // ÅŒã‚Ü‚Å1“x‚àØ‚ê‚Ä‚È‚¯‚ê‚Î¬—§
    }

    public void ResetAll()
    {
        currentCombo = 0;
        maxCombo = 0;
    }
}
