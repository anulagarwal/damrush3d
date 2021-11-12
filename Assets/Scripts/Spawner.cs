using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class Spawner : MonoBehaviour
{

    public static Spawner Instance = null;

    float bounds = 3.5f;
   
    

    public List<Ball> ballsSpawned;

    [Header ("Attributes")]
    [SerializeField] private float pipeDownConstraint = 0f;
    [SerializeField] private float pipeUpConstraint = 0f;
    [SerializeField] private float moveSpeed = 0f;
    [SerializeField] private float launchSpeed = 0.4f;
    [SerializeField] Image borderFill;
    [SerializeField] float fallDelay = 1f;

    [Header("Component References")]

    [SerializeField] GameObject ballPrefab;
    [SerializeField] Transform dropPos;
    [SerializeField] TextMeshPro leftText;
    [SerializeField] List<Transform> spawnPositions;


    #region Private variables
    Vector3 origPos;
    private float startTime;
    private bool isFalling;
    private float oldX;
    bool isGameOn;
    bool isPipeUp;
    bool isPipeDown;
    private int ballsRemaining = 0;

    #endregion
    //public float dragMultiplier = 0.1f;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        Instance = this;
    }

    void Start()
    {
        fallDelay = 1f;
        ballsSpawned = new List<Ball>();
        ballsRemaining = GameManager.Instance.maxNumberOfBalls;
        origPos = transform.position;
        leftText.text = ballsRemaining + "";
        this.enabled = false;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && ballsRemaining > 0 && GameManager.Instance.GetCurrentGameState() == GameState.InGame)
        {

            // SpawnBall();
            oldX = Input.mousePosition.x;
            startTime = Time.time;
            if (!isFalling)
                isFalling = true;
        }

        if (Input.GetMouseButton(0))
        {
            if (!isGameOn)
            {
                oldX = Input.mousePosition.x;
                isGameOn = true;
                SpawnBall();
                isFalling = true;
                startTime = Time.time;
            }

            if (isFalling)
            {
                if (startTime + fallDelay <= Time.time)
                {
                    PipeDown();
                    isFalling = false;
                    borderFill.transform.parent.gameObject.SetActive(false);
                }
                else
                {
                    borderFill.fillAmount = (Time.time - startTime) / fallDelay;
                }
            }
            if (GameManager.Instance.GetCurrentGameState() == GameState.InGame && ballsRemaining > 0)
            {

                float x = (Input.mousePosition.x - oldX) / 2;
                transform.Translate(new Vector3(x, 0, 0) * Time.deltaTime);
                oldX = Input.mousePosition.x;

                transform.position = new Vector3(Mathf.Clamp(transform.position.x, -bounds, bounds), transform.position.y, transform.position.z);
            }

        }

        if (Input.GetMouseButtonUp(0) && ballsRemaining > 0 && GameManager.Instance.GetCurrentGameState() == GameState.InGame)
        {
            if (isFalling)
            {
                borderFill.fillAmount = 0;
            }

        }


        if (ballsRemaining > 0)
        {

            if (isPipeDown && !isPipeUp)
            {
                PipeDown();
            }

            if (isPipeUp && !isPipeDown)
            {
                PipeUp();
            }
        }

    }
    public void LaunchBalls()
    {
        if (ballsRemaining > 0)
        {
            SpawnBall();
            ReleaseBall();
            Invoke("LaunchBalls", launchSpeed);
        }
    }

    public void SpawnBall()
    {
        GameObject ballObj = Instantiate(ballPrefab, dropPos.position, Quaternion.identity) as GameObject;
        oldX = Input.mousePosition.x;
        ballObj.GetComponent<Rigidbody2D>().gravityScale = 0;
        ballObj.GetComponent<Ball>().isPipeSpawned = true;
        ballObj.transform.SetParent(transform);
        ballObj.gameObject.SetActive(true);

        ballsSpawned.Add(ballObj.GetComponent<Ball>());

        BallManager.Instance.AddBall(ballObj.GetComponent<Ball>());

    }

    public void ReleaseBall()
    {
        GetComponentInChildren<Rigidbody2D>().gravityScale = 1;
        GetComponentInChildren<Rigidbody2D>().transform.parent = null;
        ballsRemaining--;
        leftText.text = ballsRemaining + "";
      //  MMVibrationManager.Haptic(HapticTypes.MediumImpact);
     //   SoundHandler.Instance.PlaySound(SoundType.Pop);

    }

    public void PipeDown()
    {
        if (transform.position.y > origPos.y + pipeDownConstraint)
        {
            transform.Translate(-Vector3.up * Time.deltaTime * moveSpeed);
            isPipeDown = true;
            isPipeUp = false;
        }
        else
        {
            //Drop ball

            isPipeDown = false;
            isPipeUp = true;
            ReleaseBall();
            PipeUp();
        }
    }

    public void PipeUp()
    {
        if (transform.position.y < origPos.y + pipeUpConstraint)
        {
            transform.Translate(Vector3.up * Time.deltaTime * moveSpeed);
            isPipeUp = true;
            isPipeDown = false;

        }
        else
        {
            isPipeUp = false;
            isPipeDown = true;
            SpawnBall();
        }
    }
}
