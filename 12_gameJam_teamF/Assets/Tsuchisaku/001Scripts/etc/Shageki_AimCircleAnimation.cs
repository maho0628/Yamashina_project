using JetBrains.Annotations;
using System.Collections;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Shageki_AimCircleAnimation : MonoBehaviour
{
    [SerializeField] private float timer;

    public float incTime;
    public float decTime;

    public float incTimes = 0.2f;
    public float decTimes = 0.2f;

    public bool nowAnim;
    public bool nowinc = false;


    public void DoAnim()
    {
        StartCoroutine(Anim());
    }




    IEnumerator Anim()
    {
        nowAnim = true;
        nowinc = true;

        yield return new WaitForSeconds(incTime);
        nowinc = false;

        yield return new WaitForSeconds(decTime);

        nowAnim = false;
        transform.localScale = Vector3.one;

        yield return null;
    }


    private void Update()
    {
        if (nowAnim)
        {
            if (nowinc)
            {
                float tmp = incTime * Time.deltaTime;
                transform.localScale += new Vector3(tmp, tmp, 0);
            }
            else
            {
                float tmp2 = decTime * Time.deltaTime;
                transform.localScale += new Vector3(tmp2, tmp2, 0);
            }

        }
    }
}
