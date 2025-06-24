using DG.Tweening;
using UnityEngine;

[System.Serializable]
public class TextPunchSettings
{
    [SerializeField] private Vector3 punchPower = new Vector3(0.2f, 0.2f, 0f);
    [SerializeField] private float punchDuration = 0.6f;
    [SerializeField] private int punchVibrato = 3;
    [SerializeField] private Ease easeType = Ease.OutBack;

    public Vector3 PunchPower => punchPower;
    public float PunchDuration => punchDuration;
    public int PunchVibrato => punchVibrato;
    public Ease EaseType => easeType;
}
