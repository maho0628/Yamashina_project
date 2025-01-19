using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShakeUI : MonoBehaviour
{
    
    public class ShakeInfo
    {
        public ShakeInfo(float duration,float amplitude,float strength = 1)
        {
            Duration = duration;
            Strength = strength;
            Amplitude = amplitude;
        }
        public float Duration { get; private set; }
        public float Strength { get; private set; } = 1;
        public float Amplitude { get; private set; }
        public UnityEvent OnShakeEnd { get; set; } = new UnityEvent();
    }

    public void Shake(GameObject target, ShakeInfo info)
    {
        StartCoroutine(ShakeCoroutine(target, info));
    }

    public IEnumerator ShakeCoroutine(GameObject target,ShakeInfo info)
    {
        print("start Coroutine");
        float time_sum = 0;
        Vector3 init_pos = target.transform.position;
        while (true)
        {
            time_sum += Time.deltaTime;
            if (time_sum >= info.Duration) break;

            float delta_x = (Mathf.PerlinNoise(time_sum * info.Strength, 0) * 2 - 1f) * info.Amplitude;
            float delta_y = (Mathf.PerlinNoise(0, time_sum * info.Strength) * 2 - 1f) * info.Amplitude;

            target.transform.position = init_pos + new Vector3(delta_x, delta_y, 0);


            

            yield return null;
        }
        target.transform.position = init_pos;
        if(info.OnShakeEnd != null)
        {
            info.OnShakeEnd.Invoke();
            info.OnShakeEnd.RemoveAllListeners();
        }
            
    }

}
