using UnityEngine;

public class StageManager : SingletonMonoBehaviour<StageManager>
{
    #region �X�e�[�W�𐧌䂷�邽�߂ɕK�v�ȕϐ�

    /// <summary>
    /// ���݂̃X�e�[�WID
    /// </summary>
    private string currentStageId;

    /// <summary>
    /// �X�e�[�W�ݒ�e�[�u��
    /// </summary>
    private StageConfigTable stageConfigTable;

    /// <summary>
    /// �X�e�[�W���Z���N�g���ꂽ���ǂ���
    /// </summary>
    private bool isStageSelected;

    #endregion

    #region �X�e�[�W�𐧌䂷�邽�߂ɕK�v�ȕϐ��̓ǂݎ���p

    /// <summary>
    /// �X�e�[�W���Z���N�g���ꂽ���ǂ����̓ǂݎ���p
    /// </summary>
    public bool IsStageSelected => isStageSelected;

    #endregion

    #region �Q�b�^�[

    /// <summary>
    /// �X�e�[�W�ݒ胊�X�g��Ԃ�
    /// </summary>
    /// <returns>StageConfigTable</returns>
    public StageConfigTable GetStageConfigTable()
    {
        return stageConfigTable;
    }

    /// <summary>
    /// ���݂̃X�e�[�W�ݒ����Ԃ�
    /// </summary>
    /// <returns>StageConfig</returns>
    internal StageConfig GetCurrentStageConfig()
    {

        if (stageConfigTable == null)
        {
            return null;
        }

        StageConfig stageConfig = stageConfigTable.GetStageConfig(currentStageId);
        if (stageConfig == null)
        {
            return null;
        }

        return stageConfig;
    }

    /// <summary>
    /// ���݂̃X�e�[�W��BGMID���擾����
    /// </summary>
    /// <returns>string</returns>
    public string GetCurrentStageBGMId()
    {
        if (stageConfigTable == null)
        {
            return null;
        }

        StageConfig stageConfig = stageConfigTable.GetStageConfig(currentStageId);
        if (stageConfig == null)
        {
            return null;
        }

        return stageConfig.StageBgm.BgmId.ToString();
    }

    /// <summary>
    /// ���݂̕��ʃt�@�C�������擾����
    /// </summary>
    /// <returns>string</returns>
    public string GetCurrentChartFileName()
    {
        if (stageConfigTable == null)
        {
            return null;
        }

        StageConfig stageConfig = stageConfigTable.GetStageConfig(currentStageId);
        if (stageConfig == null)
        {
            return null;
        }

        return stageConfig.ChartFileName;
    }

    #endregion


    #region �Z�b�^�[

    /// <summary>
    /// �X�e�[�W���Z���N�g���ꂽ����ݒ肷��֐�
    /// </summary>
    /// <param name="stageSelected">�X�e�[�W���Z���N�g���ꂽ��</param>
    public void SetStageSelected(bool stageSelected)
    {
        isStageSelected = stageSelected;
    }

    /// <summary>
    /// �X�e�[�W�ݒ�e�[�u����ݒ肷��֐�
    /// </summary>
    /// <param name="table"></param>
    public void SetupStageTable(StageConfigTable table)
    {
        stageConfigTable = table;

    }

    /// <summary>
    /// �X�e�[�W����ݒ肷��֐�
    /// </summary>
    /// <param name="table">�X�e�[�W���</param>
    /// <param name="stageId">�X�e�[�WID</param>
    public void SetupStage(StageConfigTable table, string stageId)
    {
        stageConfigTable = table;
        currentStageId = stageId;

        // �X�e�[�W�ݒ���擾
        StageConfig stageConfig = stageConfigTable.GetStageConfig(currentStageId);
        if (stageConfig == null)
        {
            return;
        }

        // �����ŃX�e�[�W�Ɋ֘A����BGM�A���ʂȂǂ��Z�b�g�A�b�v���鏈����ǉ�
    }

    #endregion

}
