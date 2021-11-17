using System.Collections.Generic;

using DG.Tweening;
using UnityEngine;


public class Ball : MonoBehaviour
{
    [SerializeField]
    public bool activated = false;
    public bool destroyed = false;
    [SerializeField]
    public bool isPipeSpawned;
    public GameObject smoke;
    private bool scaling = false;

    private void Update()
    {
        if(transform.position.y < -22)
        {
            destroyed = true;
            Destroy(gameObject);
        }
    }
    public void Shrink(float shrinkPercentage = 0.1f) {
        scaling = false;
        if (destroyed) {
            return;
        }
        transform.DOScale(transform.localScale.x - (transform.localScale.x * (shrinkPercentage / 100)), 0.2f).SetEase(Ease.InOutCubic).OnComplete(() => {
            // GetComponentInChildren<ParticleSystem>().startSize = transform.localScale.x;
            if (transform.localScale.x < 0.23f)
            {
                destroyed = true;
              //  GameManager.Instance.ReduceRemainingBalls(1);
              //  BucketController.Instance.ballsSpawned.Remove(this);
             //   GameManager.Instance.SpawnDeathParticle(transform.position);
                Destroy(gameObject);
            }
            if (gameObject == null || destroyed || !scaling) {
                return;
            }
            scaling = true;
        });
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag=="DrawnWall")
        if (GetComponent<Rigidbody2D>().gravityScale == 0)
        {
            GetComponent<Rigidbody2D>().gravityScale = 1;
        }
    }
}
