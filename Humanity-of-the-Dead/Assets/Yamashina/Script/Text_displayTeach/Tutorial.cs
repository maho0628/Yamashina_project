using UnityEngine;
using UnityEngine.UI;
public enum TutorialState
{
    PlayerMove,
    PlayerGauge,
    PlayerDoNotMove,
    PlayerAttack,
    PlayerComfort,
    EnemyDrop,
    PlayerTransplant,
    Option
}
public class Tutorial : TextDisplay
{
    private TutorialSpawn tutorialSpawner;

    private const float POSITION_DONOT_MOVE = 21;

    [SerializeField, Header("チュートリアル画像を消すまでの時間")] private float tutorialDelete;

    private float tutorialTimer = 0;
    public static void NextState()
    {
        int nextIndex = (int)enGameState + 1; // 次のインデックス
        if (nextIndex < System.Enum.GetValues(typeof(TutorialState)).Length)
        {
            ChangeState((TutorialState)nextIndex);
        }
        else
        {
            Debug.Log("Tutorial finished!");
        }

        Debug.Log("Next State: " + enGameState);
    }

    static TutorialState enGameState = TutorialState.PlayerMove;
    static TutorialState previousGameState; // 前回のゲームステートを保存
    protected override void Start()
    {
        enGameState = TutorialState.PlayerMove;
        base.Start();
        tutorialSpawner = FindAnyObjectByType<TutorialSpawn>();
    }
    public static void ChangeState(TutorialState newState)
    {
        previousGameState = enGameState; // 現在のステートを前回のステートとして保存

        enGameState = newState;
        Debug.Log("ChangeState" + newState);
    }
    // ステートが変わったかを確認する関数
    public static bool HasStateChanged()
    {
        return enGameState != previousGameState;
    }
    public static TutorialState GetState()
    {
        return enGameState;
    }
    private void ChangeStateToDoNotMoveIfNeeded()
    {
        if (enGameState == TutorialState.PlayerGauge && Player.transform.position.x > POSITION_DONOT_MOVE)
        {
            ChangeState(TutorialState.PlayerDoNotMove);
        }
    }
    protected override void Update()
    {

        switch (GameManager.GetState())
        {

            case GameState.Main:
                base.Update();
                ChangeStateToDoNotMoveIfNeeded();

                break;
            case GameState.ShowText:
                base.Update();
                if (!TextArea.activeSelf)
                {
                    GameManager.ChangeState(GameState.Tutorial);
                    tutorialSpawner.SpawnTutorial();
                    Debug.Log(GameManager.GetState().ToString());

                }

                break;
            case GameState.Tutorial:
                if(tutorialSpawner.newImageObject != null) 
                {
                    Image enterUIImage = tutorialSpawner.newImageObject.transform.Find("EnterUI").gameObject.GetComponent<Image>();

                    Color enterUIcolor = enterUIImage.color;

                    if (tutorialTimer > tutorialDelete)
                    {
                        Debug.Log(tutorialSpawner.newImageObject?.transform.Find("EnterUI").gameObject);
                        Debug.Log(enterUIcolor);
                        Debug.Log(enterUIcolor.a);
                        enterUIcolor.a = 1f;
                        Debug.Log(enterUIcolor.a);
                        enterUIImage.color = enterUIcolor; // 変更後の色を適用

                        if (Input.GetKeyDown(KeyCode.Space))
                        {



                            tutorialSpawner.DestroyCanvasWithImage();

                            tutorialTimer = 0;
                        }
                    }

                }
               
                tutorialTimer += Time.deltaTime;

                if (GetState() == TutorialState.Option && tutorialSpawner.canvasObject == null)
                {
                    ShowGameClearUI();
                }

                break;
            case GameState.Hint:
                base.Update(); break;
            case GameState.AfterBoss:
                base.Update(); break;

            case GameState.Clear:
                base.Update();
                break;




        }
    }

    public override void ShowHintText()
    {
        base.ShowHintText();
    }

    protected override void FinishTextHint()
    {
        base.FinishTextHint();
    }
    public override void FinishTextShowText()
    {
        base.FinishTextShowText();
    }
    protected override void initCurrentTextDisplay()
    {
        base.initCurrentTextDisplay();
    }
    protected override void UpdateHintText()
    {
        base.UpdateHintText();
    }
    public override void ShowTextChange()
    {

        for (int i = 0; i < Position.Length; i++)
        {
            if (Player.transform.position.x > Position[i] && Flag[i] == false)
            {
                Flag[i] = true;

                GameManager.ChangeState(GameState.ShowText);    //GameStateがShowTextに変わる
                UpdateText();
                //テキスト表示域を表示域
                TextArea.SetActive(true);
                if (i != 0)
                {
                    NextState();
                }
            }


        }
    }
    //private void OnGUI()
    //{
    //    GUI.skin.label.fontSize = 30;  // 例えば30に設定
    //    GUI.skin.label.normal.textColor = Color.black;
    //    GUI.skin.label.fontStyle = FontStyle.Bold;
    //    GUI.Label(new Rect(10.0f, 400.0f, Screen.width, Screen.height), enGameState.ToString());
    //}
}
