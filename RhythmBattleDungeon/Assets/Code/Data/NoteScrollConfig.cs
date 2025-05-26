using UnityEngine;

/// <summary>
/// �m�[�c�̃X�N���[���ݒ�Ɋւ���X�N���v�^�u���I�u�W�F�N�g
/// </summary>
[CreateAssetMenu(
    fileName = "NoteScrollConfig",
    menuName = "GameConfig/�m�[�c/�X�N���[���ݒ�"
)]
public class NoteScrollConfig : ScriptableObject
{
    #region �m�[�c�X�N���[���ݒ�Ɋւ�������Ǘ��p�ϐ�

    /// <summary> 1�`�Q���P���Ó�
    /// �m�[�c�����胉�C���ɓ��B����܂ł̎��ԁi�b�j
    /// </summary>
    [Tooltip("�m�[�c�X�N���[���ݒ�")]
    [SerializeField, Header("�m�[�c�����胉�C���ɓ��B����܂ł̎��ԁi�b�j �A1�`�Q�b�̊Ԃ��Ó�")]
    private float scrollDuration = 2f;

    /// <summary>
    /// �m�[�c�̏o��Y���W�i��j
    /// </summary>
    [SerializeField, Header("�m�[�c�̏o��Y���W�i��j")]
    private float startY = 500f;

    /// <summary>
    /// �m�[�c�̏I��Y���W�i���j
    /// </summary>
    [SerializeField, Header("�m�[�c�̏I��Y���W�i���j")]
    private float endY = -100f;

    #endregion


    #region ���[���̏��Ɋւ�������Ǘ��p�ϐ�

    /// <summary>
    /// �e���[���̉����ipx�j
    /// </summary>
    [SerializeField, Header("�e���[���̉����ipx�j")]
    private float laneWidth = 100f;

    /// <summary>
    /// �e���[���̍���
    /// </summary>
    [SerializeField, Header("�e���[���̍���")]
    private float laneHeight;

    /// <summary>
    /// ���[���̐F��ݒ�
    /// </summary>
    [SerializeField, Header("���[���̐F��ݒ�")]
    private Color[] laneColors;

    /// <summary>
    /// ���[�����Ƃ̉摜
    /// </summary>
    [SerializeField, Header("���[�����Ƃ̉摜")]
    private Sprite[] laneSprites;

    /// <summary>
    /// ���[�����B�����ݒ��4
    /// </summary>
    [SerializeField, Header("���[�����B�����ݒ��4")]
    [Min(1)]
    private int laneCount = 4;

    #endregion


    #region �ǂݎ���p�v���p�e�B(�m�[�c�X�N���[���ݒ�Ɋւ�������Ǘ��p�ϐ�)

    /// <summary>
    /// �m�[�c�����胉�C���ɓ��B����܂ł̎��ԁi�b�j�̓ǂݎ���p
    /// </summary>
    internal float ScrollDuration => scrollDuration;

    /// <summary>
    /// �m�[�c�̏o��Y���W�i��j�̓ǂݎ���p
    /// </summary>
    internal float StartY => startY;

    /// <summary>
    /// �m�[�c�̏I��Y���W�i���j�̓ǂݎ���p
    /// </summary>
    internal float EndY => endY;

    #endregion


    #region �ǂݎ���p�v���p�e�B(���[���̏��Ɋւ�������Ǘ��p�ϐ�)

    /// <summary>
    ///  �e���[���̉����ipx�j�̓ǂݎ���p
    /// </summary>
    internal float LaneWidth => laneWidth;

    /// <summary>
    /// �e���[���̍����̓ǂݎ���p
    /// </summary>
    internal float LaneHeight => laneHeight;

    /// <summary>
    /// ���[�����̓ǂݎ���p
    /// </summary>
    internal int LaneCount => laneCount;

    #endregion


    #region �Q�b�^�[���\�b�h

    /// <summary>
    /// laneIndex�̃��[���̐F��Ԃ�
    /// </summary>
    /// <param name="laneIndex"></param>
    /// <returns>Color</returns>
    internal Color GetLaneColor(int laneIndex)
    {
        if (laneIndex >= 0 && laneIndex < laneColors.Length)
            return laneColors[laneIndex];
        return Color.white;
    }

    /// <summary>
    /// laneIndex�̃��[�����Ƃ̉摜��Ԃ�
    /// </summary>
    /// <param name="laneIndex"></param>
    /// <returns>Sprite</returns>
    internal Sprite GetLaneSprite(int laneIndex)
    {
        if (laneIndex >= 0 && laneIndex < laneSprites.Length)
            return laneSprites[laneIndex];
        return null;
    }

    #endregion
}


