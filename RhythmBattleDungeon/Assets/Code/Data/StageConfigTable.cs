using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameData/Stage Config Table")]
/// <summary>
/// �e���ʃX�e�[�W�̐ݒ���Ǘ�����ScriptableObject
/// </summary>
public class StageConfigTable : ScriptableObject
{
    #region �X�e�[�W���X�g��f�B�N�V���i�������Ǘ��p�ϐ�

    /// <summary>
    /// �X�e�[�W�����̃��X�g
    /// </summary>
    [SerializeField, Header("�X�e�[�W�����̃��X�g")]
    private List<StageConfig> stagesBgmLists;

    /// <summary>
    /// �Q�[�����Ŏg�p����SE�ݒ�̃��X�g�̃f�B�N�V���i��
    /// </summary>
    private Dictionary<string, StageConfig> stagesBgmDict;

    #endregion


    #region �ǂݎ���p�v���p�e�B(�X�e�[�W���X�g��f�B�N�V���i�������Ǘ��p�ϐ�)

    /// <summary>
    /// �Q�[�����Ŏg�p����SE�ݒ�̃��X�g�̓ǂݎ���p
    /// </summary>
    internal List<StageConfig> StagesBgmList => stagesBgmLists;

    #endregion


    #region �Q�b�^�[���\�b�h

    /// <summary>
    /// �w�肳�ꂽ�X�e�[�WID�ɑΉ�����StageConfig�f�[�^���擾
    /// </summary>
    /// <param name="id">�X�e�[�W��ID</param>
    /// <returns>StageConfig�f�[�^</returns>
    internal StageConfig GetStageConfig(string id)
    {
        if (stagesBgmDict == null)
        {
            InitializeDictionary();
        }

        stagesBgmDict.TryGetValue(id, out var config);
        return config;
    }

    /// <summary>
    /// �X�e�[�W�ɑΉ�����BGM�̃��X�g�������ׂĕԂ�
    /// </summary>
    /// <returns>StageConfig�f�[�^</returns>
    /// 
    internal List<StageConfig> GetAllStageConfigs()
    {
        return stagesBgmLists;
    }

    #endregion



    private void OnEnable()
    {
        // ScriptableObject �ēǂݍ��ݎ��ɂ��Ή�
        InitializeDictionary();
    }


    #region �v���C�x�[�g���\�b�h

    /// <summary>
    /// �f�B�N�V���i��������
    /// </summary>
    private void InitializeDictionary()
    {
        stagesBgmDict = new Dictionary<string, StageConfig>();
        foreach (var stageBgm in stagesBgmLists)
        {
            //�X�e�[�W�����̃��X�g��StageID�ɕ����񂪓����Ă違�f�B�N�V���i���ɂ��̕�����i�L�[�j���܂܂�Ă��Ȃ��Ȃ�
            if (!string.IsNullOrEmpty(stageBgm.StageId) && !stagesBgmDict.ContainsKey(stageBgm.StageId))
            {
                // �f�B�N�V���i���ɂ��̕������ǉ�
                stagesBgmDict.Add(stageBgm.StageId, stageBgm);
                foreach (var key in stagesBgmDict.Keys)
                {
                    //�ǂ̃L�[���o�^����Ă��邩�̃f�o�b�O���O
                    Debug.Log($"�o�^����Ă���X�e�[�WBGM�L�[: {key}");
                }
            }
            else
            {
                //�����L�[��o�^���悤�Ƃ��Ă��邩BGMID����
                Debug.LogWarning($"[StageConfigTable] �d���܂��͋��BGM ID: {stageBgm.StageId}");
            }
        }
    }

    #endregion

}





