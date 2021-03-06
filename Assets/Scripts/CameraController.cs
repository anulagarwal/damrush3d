using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;



public class CameraController : MonoBehaviour
{
    List<Ball> balls;

    [SerializeField]
    Transform limitTransform;

    [SerializeField]
    private Transform farthestBall;


    [SerializeField]
    float minimumDistance = 2;

    CinemachineVirtualCamera followCamera;

    void Start()
    {
        followCamera = GetComponent<CinemachineVirtualCamera>();
        followCamera.Follow = farthestBall;
    }

    private void LateUpdate()
    {
        if (transform.localPosition.y <= limitTransform.localPosition.y)
        {
            followCamera.Follow = null;
            return;
        }
        float yVal = 0;
        float ballTotal = 0;
        float totalBalls = 0;
        if (BallManager.Instance.balls.Count != 0)
        {
            foreach (var ball in BallManager.Instance.balls)
            {
                if (ball.destroyed)
                {
                    continue;
                }


                ballTotal += ball.transform.position.y;
                totalBalls++;
                if (followCamera.Follow == null)
                {
                    //farthestBall = ball.transform;
                    followCamera.Follow = farthestBall;
                }
                else if (farthestBall != null || (farthestBall.transform.position.y - ball.transform.position.y > minimumDistance))
                {
                    //farthestBall = ball.transform;
                    followCamera.Follow = farthestBall;
                }

            }
            yVal = ballTotal / totalBalls;
            if (yVal != 0)
            {
                farthestBall.transform.position = new Vector3(farthestBall.position.x, yVal - 3, farthestBall.position.z);
            }
        }
    }
}
