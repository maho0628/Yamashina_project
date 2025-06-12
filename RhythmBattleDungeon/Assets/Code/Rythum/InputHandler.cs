using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.STP;

/// <summary>
/// �v���C���[�̓��͂��󂯎��A�Ή����郌�[���̃m�[�c�Ƃ̃^�C�~���O������s���N���X�B
/// </summary>
public class InputHandler : MonoBehaviour
{
    #region �v���C���[�̓��͊֘A�̓����Ǘ��p�ϐ�


    /// <summary>
    /// Unity Input System�Ő������ꂽ�v���C���[���̓A�N�V�����̃C���X�^���X�B
    /// �Q�[���v���C���̃��[�����́iLane1, Lane2���j�̃}�b�s���O�Ɛ�����s���B
    /// </summary>
    private PlayerInputActions inputActions;

    /// <summary>
    ///  �e���[���ɑΉ�������̓A�N�V�����̔z��B
    /// �C���f�b�N�X�����[���ԍ��ɑΉ����A���̓C�x���g�̍w�ǂƊǗ����s���B
    /// </summary>
    private InputAction[] laneInputs;

    /// <summary>
    ///  ���݂̃X�e�[�W�ݒ肩��擾�������[�����B
    /// ���̓A�N�V�����z��̃T�C�Y����Ɏg�p�����B
    /// </summary>
    private int laneCount;

    /// <summary>
    /// �ł��ɂ�����̎��ԍ��iMiss ����j
    /// </summary>
    private float maxJudgementTime;

  
       
       
    #endregion



    /// <summary>
    /// �X�N���v�g���j�����ꂽ���̃A�N�V����������
    /// </summary>
    private void OnDestroy()
    {
        foreach (var action in laneInputs)
        {
            if (action != null)
                action.Disable();
        }
    }

    #region�@�O���ŌĂяo���\�Ȋ֐�(�v���C���[�̓��͊֘A�j

    /// <summary>
    /// �v���C���[�̓��͏����̏�����
    /// </summary>
    public void InitializeInput()
    {
        

    maxJudgementTime = JudgementManager.Instance.GetMaxJudgementTime();
        inputActions = new PlayerInputActions();
        inputActions.Gameplay.Enable();

        laneCount = StageManager.Instance.GetCurrentStageConfig()?.ScrollConfig?.GetLaneVisualConfig().LaneCount ?? 4;

        laneInputs = new InputAction[laneCount];

        for (int i = 0; i < laneCount; i++)
        {
            string actionName = $"Lane{i + 1}";
            DebugManager.Log(actionName);
            //actionName�ɑΉ�����C���v�b�g�A�N�V������T��
            laneInputs[i] = inputActions.FindAction(actionName);
            if (laneInputs[i] != null)
            {

                DebugManager.Log(laneInputs[i] + "laneInputs");

                //�����_���͕ϐ��́u�l�v�ł͂Ȃ��u�Q�Ɓv���L���v�`������̂œ���i���Q�Ƃ��Ă��܂��̂ň�x���[�J���ϐ��ɃR�s�[
                string actionCopy = actionName;

                laneInputs[i].performed += ctx => TryHitNoteByAction(actionCopy);
            }
            else
            {
                DebugManager.LogWarning($"[InputHandler] �A�N�V���� {actionName} ��������܂���");
            }
        }
     
    }

    #endregion

    /// <summary>
    /// ���͂ɑΉ������m�[�c�������邩���m�F����֐�
    /// </summary>
    /// <param name="actionName">�C���v�b�g�A�N�V������</param>
    private void TryHitNoteByAction(string actionName)
    {
        float currentTime = AudioManager.Instance.GetCurrentBGMTime();

        //
        var note = NoteManager.Instance.GetNearestNoteByAction(actionName, currentTime, maxJudgementTime);

        //�߂��m�[�c���Ȃ��̂Ń��^�[��
        if (note == null) return;

        float diff = Mathf.Abs(note.SpawnTime - currentTime);

        //���̓^�C�~���O�ɉ�����������擾
        var judgement = JudgementManager.Instance.EvaluateTiming(diff);

        //���茋�ʂ��Ȃ��̂Ń��^�[��
        if (judgement == null) return;

        Debug.Log($"Effect played: {judgement.Logic.JudgementName} on lane {note.LaneNumber}");

        if (note.IsHit) return; 

        note.IsHit = true;

        Debug.Log("note.IsHit" + note.IsHit);
        //���茋�ʂ�JudgementManager�ɓn���ăX�R�A�Ȃǂ𔽉f���Ă��炤
        JudgementManager.Instance.ApplyJudgement(judgement, note.LaneNumber);
        AnimationManager.Instance.ShowScoreEffect(judgement);
        AnimationManager.Instance.ShowJudgeEffect(judgement);
       

    }




}
