using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectiveHandler : MonoBehaviour
{

    [SerializeField]
    TextMeshPro ballsCollectedLabel;

    private int ballsCollected = 0;

    [SerializeField] private ParticleSystem vfx;

    [SerializeField] bool isEmpty;
    [SerializeField] float checkDelay = 6;
    [SerializeField] int health = 6;

    bool isCheckOn;
    private float checkDelayStartTime;
    void Start()
    {
        checkDelay = 6;
        if (!isEmpty)
        {
            
          //  ballsCollectedLabel.text = ballsCollected + "/" + GameManager.Instance.requiredBalls;
        }
    }

    private void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            if (!isEmpty)
            {
                ballsCollected++;
              //  ballsCollectedLabel.text = ballsCollected + "/" + GameManager.Instance.requiredBalls;
                /*if (ballsCollected >= GameManager.Instance.requiredBalls)
                {
                    ballsCollectedLabel.color = Color.green;
                }*/
                vfx.Play();
                checkDelayStartTime = Time.time;
            }
           // UIManager.Instance.SpawnText(other.transform.position);
            other.GetComponent<Ball>().destroyed = true;
           // SoundHandler.Instance.PlaySound(SoundType.Pop);
           // GameManager.Instance.AddBallToBasket(other.gameObject);
           // BucketController.Instance.ballsSpawned.Remove(other.gameObject.GetComponent<Ball>());
            other.GetComponent<Ball>().smoke.SetActive(false);
            Destroy(other.gameObject);
            if (!isCheckOn) { isCheckOn = true; }
            checkDelayStartTime = Time.time;

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ball")
        {
            Destroy(collision.gameObject);
            health--;
            GetComponentInChildren<TextMeshPro>().text = health + "";
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
