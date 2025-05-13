using UnityEngine;

public class GameSceneInitializer : MonoBehaviour
{

    private void Awake()
    {

        if (!GameInitializer.Instance.Initialized)
        {
            GameInitializer.Instance.SetUpGameInitialize();
        }

       
    }

}
