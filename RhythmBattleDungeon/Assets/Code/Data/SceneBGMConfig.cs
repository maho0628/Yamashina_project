using UnityEngine;

/// <summary>
/// �V�[�����Ƃ�BGM�̐ݒ�f�[�^
/// </summary>
[System.Serializable]
public class SceneBGMConfig
{
    #region �V�[��BGM�̐ݒ�Ɋւ�������Ǘ��p�ϐ�

    /// <summary>
    /// �Ώۂ̃V�[����
    /// </summary>
    [SerializeField, Header("�Ώۂ̃V�[����")]
    private string sceneName;

    /// <summary>
    /// �Đ�����BGM��ID
    /// </summary>
    [SerializeField, Header("�Đ�����BGM��ID")]
    private string bgmId;

    #endregion


    #region �ǂݎ���p�v���p�e�B(�V�[��BGM�̐ݒ�Ɋւ�������Ǘ��p�ϐ�)

    /// <summary>
    /// �Ώۂ̃V�[�����̓ǂݎ���p
    /// </summary>
    internal string SceneName => sceneName;

    /// <summary>
    /// �Đ�����BGM��ID�̓ǂݎ���p
    /// </summary>
    internal string BgmId => bgmId;

    #endregion
}

