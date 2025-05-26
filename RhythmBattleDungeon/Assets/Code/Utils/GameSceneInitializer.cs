using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneInitializer : MonoBehaviour
{
    private StageConfig currentStage;

    private void Awake()
    {

        if (!GameInitializer.Instance.Initialized)
        {
            GameInitializer.Instance.SetUpGameInitialize();
        }

    }


    private void Start()
    {

        if (StageManager.Instance.IsStageSelected)
        {
            currentStage = StageManager.Instance.GetCurrentStageConfig();

            var judgementConfigs = currentStage.JudgementConfigs;
            Debug.Log(judgementConfigs.ToString());
            JudgementManager.Instance.Setup(judgementConfigs);
            if (currentStage != null)
            {
                // �X�e�[�W�p��BGM������Ȃ炻����Đ�
                AudioManager.Instance.ForcePlayBGM(currentStage.StageBgm.BgmId);

                Debug.Log($"[GameSceneInitializer] �X�e�[�W�pBGM���Đ�: {currentStage.StageBgm.BgmId}");
            }
            StartCoroutine(WaitForBGMThenInitialize());






        }
        else
        {
            string sceneName = SceneManager.GetActiveScene().name;
            SceneBGMConfigTable sceneBgmConfigTable = GameInitializer.Instance.GetSceneBGMConfigTable();
            string bgmId = sceneBgmConfigTable.GetSceneBgmConfigName(sceneName);
            Debug.Log($"�V�[����: {sceneName}, BGM ID: {bgmId}");
            AudioManager.Instance.PlayBGMIfNotPlaying(bgmId);
            Debug.LogWarning("�X�e�[�W���܂��I������Ă��܂���I");
            return;
        }

    }
    private IEnumerator WaitForBGMThenInitialize()
    {
        // BGM���Đ�����āAscrollDuration�b�ȏ�o�܂ő҂�
        var scrollDuration = currentStage.ScrollConfig.ScrollDuration;
        yield return new WaitUntil(() => AudioManager.Instance.GetCurrentBGMTime() > scrollDuration);

        // BGM���m���Ɏn�܂������ƁANoteManager������
        NoteManager.Instance.Initialize();
        Debug.Log("NoteManager ��������");

        // �C���v�b�g��������
        FindAnyObjectByType<InputHandler>()?.InitializeInput();
    }


 

}
