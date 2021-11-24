using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MoreMountains.Feedbacks;
using TMPro;
public class UIManager : MonoBehaviour
{
    #region Properties
    public static UIManager Instance = null;

    [Header("Components Reference")]

    [SerializeField] private GameObject PointText;
    [SerializeField] private GameObject AwesomeText;


    [Header("UI Panel")]
    [SerializeField] private GameObject mainMenuUIPanel = null;
    [SerializeField] private GameObject gameplayUIPanel = null;
    [SerializeField] private GameObject drawUIPanel = null;

    [SerializeField] private GameObject gameOverWinUIPanel = null;
    [SerializeField] private GameObject gameOverLoseUIPanel = null;
    [SerializeField] private GameObject infoPanel = null;


    [Header ("Texts")]
    [SerializeField] private TextMeshProUGUI scoreText = null;
    [SerializeField] private Text mainLevelText = null;
    [SerializeField] private Text inGameLevelText = null;
    [SerializeField] private Text countdownText;


    [Header("Inventory")]
    [SerializeField] private Text currentWalls = null;

    [Header("Ink")]
    [SerializeField] private Image inkFill = null;


    #endregion

    #region MonoBehaviour Functions
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        Instance = this;
    }

    private void Start()
    {
        //SwitchControls(Controls.Touch);
    }
    #endregion

    #region Getter And Setter

    #endregion

    #region Public Core Functions
    public void SwitchUIPanel(UIPanelState state)
    {
        switch (state)
        {
            case UIPanelState.MainMenu:
                mainMenuUIPanel.SetActive(true);
                gameplayUIPanel.SetActive(false);
                gameOverWinUIPanel.SetActive(false);
                gameOverLoseUIPanel.SetActive(false);
                drawUIPanel.SetActive(false);
                break;

            case UIPanelState.Drawing:
                mainMenuUIPanel.SetActive(false);
                gameplayUIPanel.SetActive(false);
                gameOverWinUIPanel.SetActive(false);
                gameOverLoseUIPanel.SetActive(false);
                drawUIPanel.SetActive(true);

                break;
            case UIPanelState.Gameplay:
                mainMenuUIPanel.SetActive(false);
                gameplayUIPanel.SetActive(true);
                gameOverWinUIPanel.SetActive(false);
                gameOverLoseUIPanel.SetActive(false);
                drawUIPanel.SetActive(false);

                break;
            case UIPanelState.GameWin:
                mainMenuUIPanel.SetActive(false);
                gameplayUIPanel.SetActive(false);
                gameOverWinUIPanel.SetActive(true);
                gameOverLoseUIPanel.SetActive(false);
                drawUIPanel.SetActive(false);

                break;
            case UIPanelState.GameLose:
                mainMenuUIPanel.SetActive(false);
                gameplayUIPanel.SetActive(false);
                gameOverWinUIPanel.SetActive(false);
                gameOverLoseUIPanel.SetActive(true);
                drawUIPanel.SetActive(false);

                break;
        }
    }



    public void UpdateScore(int value)
    {
        scoreText.text = "" + value;
    }

    public void UpdateLevel(int level)
    {
        mainLevelText.text = "LEVEL " + level;
        inGameLevelText.text = "LEVEL " + level;
    }

    public void EnableInfoPanel()
    {
        infoPanel.SetActive(true);
    }
  
    public void UpdateInkAmount(float val)
    {
        inkFill.fillAmount = val;
    }

    public void UpdateCountDownText(string s)
    {
        countdownText.text = s;
        countdownText.GetComponent<MMFeedbacks>().PlayFeedbacks();
    }

    public void EnableCountDownText()
    {
        if (!countdownText.gameObject.activeInHierarchy)
            countdownText.gameObject.SetActive(true);
    }
    public void DisableCountDownText()
    {
        if(countdownText.gameObject.activeInHierarchy)
        countdownText.gameObject.SetActive(false);
    }
    public void UpdateInkColor(Color c)
    {
        inkFill.color = c;
    }
    public void SpawnPointText(Vector3 point)
    {
        Instantiate(PointText, point, Quaternion.identity);
    }

    public void SpawnAwesomeText(Vector3 point, string s)
    {
        GameObject g = Instantiate(AwesomeText, new Vector3(point.x, 2, point.z), Quaternion.identity);
        g.GetComponentInChildren<TextMeshPro>().text = s;
    }

    public void UpdateCurrentWallsCounts(int val)
    {
        currentWalls.text = "x" + val;
    }
    #endregion

}




