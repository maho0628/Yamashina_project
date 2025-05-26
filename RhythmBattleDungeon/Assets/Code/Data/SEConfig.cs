using UnityEngine;

/// <summary>
/// �P���SE�i���ʉ��j�̐ݒ�f�[�^
/// </summary>
[System.Serializable]
public class SEConfig
{
    #region SE�ݒ�Ɋւ�������Ǘ��p�ϐ�

    /// <summary>
    /// SE��ID��
    /// </summary>
    [SerializeField, Header("SE��ID��")]
    private string seId;

    /// <summary>
    /// �g�p����SE�I�[�f�B�I�N���b�v
    /// </summary>
    [SerializeField, Header("�g�p����SE�I�[�f�B�I�N���b�v")]
    private AudioClip seAudioClip;

    /// <summary>
    /// SE�̐���
    /// </summary>
    [SerializeField, Header("SE�̐���")]
    private string description;  // ��F�u�{�^���������v�Ȃ�

    #endregion


    #region �ǂݎ���p�v���p�e�B(SE�ݒ�Ɋւ�������Ǘ��p�ϐ�)

    /// <summary>
    /// SE��ID���̓ǂݎ���p
    /// </summary>
    internal string SeId => seId;

    /// <summary>
    /// �g�p����SE�I�[�f�B�I�N���b�v�̓ǂݎ���p
    /// </summary>
    internal AudioClip SeAudioClip => seAudioClip;

    /// <summary>
    /// SE�̐����̓ǂݎ���p
    /// </summary>
    internal string Description => description;

    #endregion
}
