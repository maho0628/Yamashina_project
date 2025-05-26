using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
/// <summary>
/// �X�e�[�W�̐ݒ�f�[�^
/// </summary>
public class StageConfig
{
    #region  �X�e�[�W�ݒ�Ɋւ�������Ǘ��p�ϐ�

    /// <summary>
    /// �X�e�[�WID��
    /// </summary>
    [SerializeField, Header("����ID��")]
    private string stageId;

    /// <summary>
    /// �X�e�[�W�Ŗ炷BGM�����̐ݒ���e
    /// </summary>
    [SerializeField, Header("BGM�����̐ݒ���e")]
    private BGMConfig stageBgm;

    /// <summary>
    /// ���ʃf�[�^Json�t�@�C����
    /// </summary>
    [SerializeField, Header("���ʃf�[�^Json�t�@�C����")]
    private string chartFileName;

    /// <summary>
    /// �m�[�c�̃X�N���[���ݒ�
    /// </summary>
    [SerializeField, Header("�m�[�c�̃X�N���[���ݒ�")]
    private NoteScrollConfig scrollConfig;

    /// <summary>
    /// ����ݒ�iPerfect / Good / Miss �Ȃǁj
    /// </summary>
    [SerializeField, Header("����ݒ�iPerfect / Good / Miss �Ȃǁj")]
    private List<JudgementConfig> judgementConfigs;

    /// <summary>
    /// �y�ȏI����̑J�ڑҋ@�b��
    /// </summary>
    [SerializeField, Header("�y�ȏI����̑J�ڑҋ@�b��")]
    private float delayBeforeResult = 2.0f;

    #endregion


    #region �ǂݎ���p�v���p�e�B(�X�e�[�W�ݒ�Ɋւ�������Ǘ��p�ϐ�)

    /// <summary>
    /// ����ID���̓ǂݎ���p
    /// </summary>
    internal string StageId => stageId;

    /// <summary>
    /// ����BGM�����̐ݒ���e�̓ǂݎ���p
    /// </summary>
    internal BGMConfig StageBgm => stageBgm;

    /// <summary>
    /// ���ʃf�[�^Json�t�@�C�����̓ǂݎ���p
    /// </summary>
    internal string ChartFileName => chartFileName;

    /// <summary>
    /// �m�[�c�̃X�N���[���ݒ�̓ǂݎ���p
    /// </summary>
    internal NoteScrollConfig ScrollConfig => scrollConfig;

    /// <summary>
    ///  ����ݒ�iPerfect / Good / Miss �Ȃǁj�̓ǂݎ���p
    /// </summary>
    internal List<JudgementConfig> JudgementConfigs => judgementConfigs;

    /// <summary>
    /// �y�ȏI����̑J�ڑҋ@�b���̓ǂݎ���p
    /// </summary>
    internal float DelayBeforeResult => delayBeforeResult;

    #endregion
}