using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SceneDatabase", menuName = "Scene/SceneDatabase")]
public class SceneDatabase : ScriptableObject
{
    [SerializeField] private List<SceneData> scenes;

    [System.Serializable]
    public class SceneData
    {
        public SceneObject scene;
        public SceneObject nextScene;
        public SceneObject previousScene;
    }

    public SceneObject GetScene(string name)
    {
        return scenes.Find(s => (string)s.scene == name)?.scene;
    }

    public SceneObject GetNextScene(string name)
    {
        return scenes.Find(s => (string)s.scene == name)?.nextScene;
    }

    public SceneObject GetPreviousScene(string name)
    {
        return scenes.Find(s => (string)s.scene == name)?.previousScene;
    }
}
