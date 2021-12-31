using UnityEngine;
using UnityEngine.SceneManagement;
using MoreMountains.NiceVibrations;
using LionStudios.Suite.Analytics;

public class GameManager : MonoBehaviour
{
    #region Properties
    public static GameManager Instance = null;

    [Header("Component Reference")]
    [SerializeField] public GameObject confetti;
    [SerializeField] public GameObject ballParent;


    [Header("Attributes")]
    [SerializeField] private int currentScore;
    [SerializeField] private int currentLevel;
    [SerializeField] private int maxLevels;
    [SerializeField] GameState currentState;
    [SerializeField] public int maxNumberOfBalls;
    [SerializeField] int currentNumberOfBalls;
    [SerializeField] float failTime = 5f;
    [SerializeField] float currentFailTime = 5f;

    [SerializeField] float waitTime = 10f;

    public bool isWaiting;
    public bool isFailing;
    private float waitStartTime;
    private float failStartTime;




    #endregion

    #region MonoBehaviour Functions
    private void Awake()
    {
        Application.targetFrameRate = 100;
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        Instance = this;
    }

    private void Start()
    {
        //SwitchCamera(CameraType.MatchStickCamera);
        LionAnalytics.GameStart();

        currentFailTime = failTime;
        currentLevel = PlayerPrefs.GetInt("level", 1);
        UIManager.Instance.UpdateLevel(currentLevel);
        maxLevels = 11;
        currentNumberOfBalls = maxNumberOfBalls;
        StartWaitCountdown();
        UIManager.Instance.UpdateCountDownText(currentFailTime+"");
        if (PlayerPrefs.GetInt("first", 1)==1)
        {
            UIManager.Instance.EnableInfoPanel();
            PlayerPrefs.SetInt("first", 0);
        }
        else
        {
            UpdateState(GameState.Draw);

        }

        LionAnalytics.LevelStart(currentLevel, 0, null);

    }

    private void Update()
    {
        if (currentState == GameState.Draw)
        {
            if (waitStartTime + waitTime < Time.time && isWaiting)
            {
                //Start fail time and show on screen
                UIManager.Instance.EnableCountDownText();
                failStartTime = Time.time;
                isFailing = true;
                isWaiting = false;
            }

            if (isFailing)
            {
                if(failStartTime + 1 < Time.time)
                {
                    currentFailTime--;
                    UIManager.Instance.UpdateCountDownText(""+currentFailTime);
                    failStartTime = Time.time;
                    if (currentFailTime <= 0)
                    {
                        isFailing = false;
                        LoseLevel();
                    }
                }
            }
            if (Input.GetMouseButtonDown(0))
            {
                ResetFailStart();
            }
        }

    }
    #endregion
    public void StartWaitCountdown()
    {
        waitStartTime = Time.time;
        isWaiting = true;
    }

    public void ResetFailStart()
    {
        //Hide fail countdown
        UIManager.Instance.DisableCountDownText();
        isFailing = false;
        currentFailTime = failTime;
        StartWaitCountdown();
    }
    public void StartLevel()
    {
        UpdateState(GameState.InGame);
        //    Spawner.Instance.enabled = true;
        DrawManager.Instance.UpdateMaterial(1);

//        GridManager.Instance.enabled = true;
    }

    public void StartDraw()
    {
        //TinySauce.OnGameStarted(currentLevel+"");

        UpdateState(GameState.Draw);
    }
    public void EndDraw()
    {
    }
    public void FinishSetup()
    {
        GridManager.Instance.ClearGrid();
        GridManager.Instance.enabled = false;

        //Enable ball drop
    }

    public void WinLevel()
    {
        if (currentState == GameState.InGame || currentState == GameState.Draw)
        {
            UpdateState(GameState.Win);
            PlayerPrefs.SetInt("level", currentLevel + 1);
            currentLevel++;
            confetti.SetActive(true);
            MMVibrationManager.Haptic(HapticTypes.Success);
            //TinySauce.OnGameFinished(true, 0);

            LionAnalytics.LevelComplete(currentLevel-1, 0, null, null);

            //AdManager.Instance.ShowInterstital();
        }
    }

    public void InkOverCheckCondition()
    {
        if (currentState == GameState.InGame || currentState == GameState.Draw)
        {
            UpdateState(GameState.Lose);
        }
    }
    public void LoseLevel()
    {
        if (currentState == GameState.InGame || currentState == GameState.Draw)
        {
            UpdateState(GameState.Lose);
//            TinySauce.OnGameFinished(false, 0);
            LionAnalytics.LevelFail(currentLevel - 1, 0, null, null);

            //  AdManager.Instance.ShowInterstital();


        }
    }

    public GameState GetCurrentGameState()
    {
        return currentState;
    }

    #region Scene Management



    public void ChangeLevel()
    {
        if (currentLevel > maxLevels)
        {
            int newId = currentLevel % maxLevels;
            if (newId == 0)
            {
                newId = maxLevels;
            }
            SceneManager.LoadScene("Level " + (newId));
        }
        else
        {
            SceneManager.LoadScene("Level " + currentLevel);
        }
    }

    public void LoadLevel(string s)
    {

        if (currentLevel > maxLevels)
        {
            int newId = currentLevel % maxLevels;
            if (newId == 0)
            {
                newId = maxLevels;
            }
            SceneManager.LoadScene("Level " + (newId));
        }
        else
        {
            SceneManager.LoadScene("Level " + currentLevel);
        }
    }

    #endregion


    #region Public Core Functions

    public void AddScore(int value)
    {
        currentScore += value;
        UIManager.Instance.UpdateScore(currentScore);
    }


    public void UpdateState (GameState state)
    {
        switch (state)
        {
            case GameState.Main:
                UIManager.Instance.SwitchUIPanel(UIPanelState.MainMenu);
                break;

            case GameState.Draw:
                UIManager.Instance.SwitchUIPanel(UIPanelState.Drawing);
                DrawManager.Instance.UpdateMaterial(1);

                break;
            case GameState.InGame:
                UIManager.Instance.SwitchUIPanel(UIPanelState.Gameplay);
                break;

            case GameState.Win:
                Invoke("ShowWinUI", 1.4f);
                break;

            case GameState.Lose:
                Invoke("ShowLoseUI", 1.4f);
                break;

        }
        currentState = state;

    }
    #endregion

    #region Invoke Functions

    void ShowWinUI()
    {
        UIManager.Instance.SwitchUIPanel(UIPanelState.GameWin);
    }

    void ShowLoseUI()
    {
        UIManager.Instance.SwitchUIPanel(UIPanelState.GameLose);
    }
    #endregion
}
