using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using MoreMountains.NiceVibrations;
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
        currentLevel = PlayerPrefs.GetInt("level", 1);
        UIManager.Instance.UpdateLevel(currentLevel);
        currentState = GameState.Draw;
        UpdateState(currentState);
        maxLevels = 4;
        currentNumberOfBalls = maxNumberOfBalls;
    }
    #endregion

    public void StartLevel()
    {
        UpdateState(GameState.InGame);
        //    Spawner.Instance.enabled = true;
        DrawManager.Instance.UpdateMaterial(1);

//        GridManager.Instance.enabled = true;
    }

    public void StartDraw()
    {
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
