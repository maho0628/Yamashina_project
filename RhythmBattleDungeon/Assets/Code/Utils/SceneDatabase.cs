using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SceneDatabase", menuName = "Scene/SceneDatabase")]
public class SceneDatabase : ScriptableObject
{
    [SerializeField] private List<SceneData> scenes;

    [System.Serializable]
    public class SceneData
    {
        public SceneReference sceneReference;
        public SceneReference nextScene;
        public SceneReference previousScene;
    }

    public SceneReference GetSceneReference(string name)
    {
        return scenes.Find(s => s.sceneReference.sceneName == name)?.sceneReference;
    }

    public SceneReference GetNextScene(string name)
    {
        return scenes.Find(s => s.sceneReference.sceneName == name)?.nextScene;
    }

    public SceneReference GetPreviousScene(string name)
    {
        return scenes.Find(s => s.sceneReference.sceneName == name)?.previousScene;
    }
}
