using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace Yamashina
{
public class LoadScene : MonoBehaviour
{
    [SerializeField] public string sceneName;//SceneName‚Ì“o˜^

    public void LoadingScene()//Scene‚Ì‘JˆÚ
    {
        SceneManager.LoadScene(sceneName);

    }
}
}