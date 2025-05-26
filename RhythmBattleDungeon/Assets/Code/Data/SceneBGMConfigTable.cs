using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �V�[���ɑΉ�����BGM�ݒ�̈ꗗ��ێ����� ScriptableObject
/// </summary>
public class SceneBGMConfigTable : ScriptableObject
{
    #region �V�[��BGM�̃��X�g��f�B�N�V���i���̓����Ǘ��p�ϐ�

    /// <summary>
    /// �V�[���ɑΉ�����BGM�̃��X�g
    /// </summary>
    [SerializeField, Header("�V�[���ɑΉ�����BGM�̃��X�g")]
    private List<SceneBGMConfig> sceneBgmConfigLists= new List<SceneBGMConfig>();


    /// <summary>
    /// �V�[���ɑΉ�����BGM�̃��X�g�̃f�B�N�V���i��
    /// </summary>
    private Dictionary<string, string> sceneToBgmIdDict;

    #endregion


    #region �ǂݎ���p�v���p�e�B�i�V�[��BGM�̃��X�g��f�B�N�V���i���̓����Ǘ��p�ϐ�)

    /// <summary>
    /// �V�[���ɑΉ�����BGM�̃��X�g�̓ǂݎ���p
    /// </summary>
    internal List<SceneBGMConfig> SceneBgmConfigLists=> sceneBgmConfigLists;

    #endregion


    #region �Q�b�^�[���\�b�h

    /// <summary>
    /// �V�[���ɑΉ�����BGM�̃��X�g�������ׂĕԂ�
    /// </summary>
    /// <returns>SceneBGMConfig��List</returns>
    internal List<SceneBGMConfig> GetAllSceneBGMConfig()
    {
        return sceneBgmConfigLists;
    }

    /// <summary>
    /// sceneName�ɑΉ�����BGMID��Ԃ�
    /// </summary>
    /// <param name="sceneName"></param>
    /// <returns>string</returns>
    internal string GetSceneBgmConfigName(string sceneName)
    {
        if (sceneToBgmIdDict == null)
        {
            InitializeDictionary();
        }

        sceneToBgmIdDict.TryGetValue(sceneName, out var bgmId);
        return bgmId;
    }

    #endregion



    private void OnEnable()
    {
        // ScriptableObject �ēǂݍ��ݎ��ɂ��Ή�
        InitializeDictionary();
    }


    #region �v���C�x�[�g���\�b�h

    /// <summary>
    /// �f�B�N�V���i���̏�����
    /// </summary>
    private void InitializeDictionary()
    {
        sceneToBgmIdDict = new Dictionary<string, string>();
        foreach (var sceneBgm in sceneBgmConfigLists)
        {
            //�R���t�B�O�̃V�[���̖��O��BgmId�������Ƃ��󗓂łȂ��Ȃ�
            if (!string.IsNullOrEmpty(sceneBgm.SceneName) && !string.IsNullOrEmpty(sceneBgm.BgmId))
            {
                //�f�B�N�V���i������SceneName���Ȃ��Ȃ�
                if (!sceneToBgmIdDict.ContainsKey(sceneBgm.SceneName))
                {
                    // �f�B�N�V���i������SceneName��BgmId��ǉ�

                    sceneToBgmIdDict.Add(sceneBgm.SceneName, sceneBgm.BgmId);
                }
                else
                {
                    //�G���[�o���ďI��
                    Debug.LogWarning($"[SceneBGMConfigTable] �V�[�� '{sceneBgm.SceneName}' �͊��ɓo�^����Ă��܂��B");
                }
            }
        }
    }

    #endregion


}
