using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SEConfig", menuName = "GameData/SEConfigTable")]

/// <summary>
/// �Q�[�����Ŏg�p����SE�i���ʉ��j�̐ݒ�ꗗ���Ǘ�����ScriptableObject
/// </summary>
public class SEConfigTable : ScriptableObject
{
    #region SE�̃��X�g��f�B�N�V���i�������Ǘ��p�ϐ�

    /// <summary>
    /// �Q�[�����Ŏg�p����SE�ݒ�̃��X�g
    /// </summary>
    [SerializeField, Header("�Q�[�����Ŏg�p����SE�ݒ�̃��X�g")]
    private List<SEConfig> seLists= new List<SEConfig>();

    /// <summary>
    /// �Q�[�����Ŏg�p����SE�ݒ�̃��X�g�̃f�B�N�V���i��
    /// </summary>
    private Dictionary<string, SEConfig> seConfigDict;

    #endregion


    #region �ǂݎ���p�v���p�e�B(SE�̃��X�g��f�B�N�V���i�������Ǘ��p�ϐ�)

    /// <summary>
    /// �Q�[�����Ŏg�p����SE�ݒ�̃��X�g�̓ǂݎ���p
    /// </summary>
    internal List<SEConfig> SeLists => seLists;

    #endregion


    #region �Q�b�^�[���\�b�h

    /// <summary>
    /// �Q�[�����Ŏg�p����SE�ݒ�̃��X�g�������ׂĕԂ�
    /// </summary>
    /// <returns>SEConfig�̃��X�g</returns>
    internal List<SEConfig> GetAllSeConfig()
    {
        return seLists;
    }

    /// <summary>
    /// ���X�g����SEConfig��ID�ŒT���ĕԂ�
    /// </summary>
    /// <param name="id"></param>
    /// <returns>SEConfig�̃��X�g</returns>
    internal SEConfig GetSeConfig(string id)
    {
        if (seConfigDict == null)
        {
            InitializeDictionary();
        }

        seConfigDict.TryGetValue(id, out var config);
        return config;
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
        seConfigDict = new Dictionary<string, SEConfig>();
        foreach (var se in seLists)
        {
            //SE�ݒ�̈ꗗ�̃��X�g��SeId�ɕ����񂪓����Ă違�f�B�N�V���i���ɂ��̕�����i�L�[�j���܂܂�Ă��Ȃ��Ȃ�
            if (!string.IsNullOrEmpty(se.SeId) && !seConfigDict.ContainsKey(se.SeId))
            {
                // �f�B�N�V���i���ɂ��̕������ǉ�
                seConfigDict.Add(se.SeId, se);
                foreach (var key in seConfigDict.Keys)
                {
                    //�ǂ̃L�[���o�^����Ă��邩�̃f�o�b�O���O
                    Debug.Log($"�o�^����Ă���SE�L�[: {key}");
                }
            }
            else
            {
                //�����L�[��o�^���悤�Ƃ��Ă��邩JudgementName����
                Debug.LogWarning($"[SEConfigTable] �d���܂��͋��BGM ID: {se.SeId}");
            }
        }
    }

    #endregion

}


