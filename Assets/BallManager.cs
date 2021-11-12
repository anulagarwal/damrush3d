using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    public List<Ball> balls;


    public static BallManager Instance = null;

    private void Awake()
    {
        Application.targetFrameRate = 100;
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        Instance = this;
    }

    #region public functions

    public void AddBall(Ball b)
    {
        balls.Add(b);
    }

    public void RemoveBall(Ball b)
    {
        balls.Remove(b);
    }
    public void ClearBalls()
    {
        balls.Clear();
    }
    #endregion

}
