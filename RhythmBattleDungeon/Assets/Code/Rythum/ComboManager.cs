/// <summary>
///ノーツのコンボ数を管理し、現在のコンボ・最大コンボ・フルコンボ判定を行うシングルトンクラス。
/// </summary>
public class ComboManager : SingletonMonoBehaviour<ComboManager>
{
    #region コンボ関連の

    /// <summary>
    /// ���݂̃R���{��
    /// </summary>
    private int currentCombo = 0;

    /// <summary>
    /// �R���{�̍ő�l
    /// </summary>
    private int maxCombo = 0;

    #endregion


    #region �ǂݎ���p�v���p�e�B(�R���{�֘A�̓����Ǘ��p�ϐ�)

    /// <summary>
    /// ���݂̃R���{���̓ǂݎ���p
    /// </summary>
    internal int CurrentCombo => currentCombo;

    /// <summary>
    /// �R���{�̍ő�l�̓ǂݎ���p
    /// </summary>
    internal int MaxCombo => maxCombo;

    #endregion


    #region �O���ŌĂяo���\�Ȋ֐�(�R���{�֘A)

    /// <summary>
    /// �R���{�������Z����֐�
    /// </summary>
    internal void IncrementCombo()
    {
        currentCombo++;
        if (currentCombo > maxCombo)
            maxCombo = currentCombo;
    }

    /// <summary>
    /// �R���{���r�؂ꂽ���Ƀ��Z�b�g����֐�
    /// </summary>
    internal void ResetCombo()
    {
        currentCombo = 0;
    }

    /// <summary>
    /// �t���R���{���ǂ����i�R���{�̍ő�l���ǂ����j��Ԃ�
    /// </summary>
    /// <returns>bool</returns>
    internal bool IsFullCombo()
    {
        return maxCombo == NoteManager.Instance.TotalNoteCount; // �Ō�܂�1�x���؂�ĂȂ���ΐ���
    }

    /// <summary>
    /// �R���{�̍ő�l�ƌ��݂̃R���{�������Z�b�g����
    /// </summary>
    internal void ResetAll()
    {
        currentCombo = 0;
        maxCombo = 0;
    }

    #endregion
}
