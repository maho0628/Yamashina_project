using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace Yamashina
{
public class LoadScene : MonoBehaviour
{
    [SerializeField] public string sceneName;//SceneName�̓o�^

    public void LoadingScene()//Scene�̑J��
    {
        SceneManager.LoadScene(sceneName);

    }
}
}