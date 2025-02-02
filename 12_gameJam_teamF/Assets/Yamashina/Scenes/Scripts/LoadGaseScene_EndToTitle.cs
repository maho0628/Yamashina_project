using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yamashina;

public class LoadGaseScene_EndToTitle : LoadScene
{
    // Start is called before the first frame update
    public void Start()
    {
        Invoke();
    }
    private void OnDestroy()
    {
        // DestroyŽž‚É“o˜^‚µ‚½Invoke‚ð‚·‚×‚ÄƒLƒƒƒ“ƒZƒ‹
        CancelInvoke();
    }
    public void Invoke()
    {
        Invoke(nameof(LoadingScene), 5.5f);

    }
    public void Loaded()
    {
        LoadingScene();
    }
}
 