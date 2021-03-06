using System;
using System.Threading.Tasks;
using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using MoreMountains.NiceVibrations;
public class BallModifier : MonoBehaviour
{
    [SerializeField]
    TextMeshPro currentValLbl;

    [SerializeField]
    GameObject ballContainer;

    [SerializeField]
    Ball ballPrefab;
    public enum MODIFIER_TYPE
    {
        ADDER,
        MULTIPLIER,
        SUBTRACTOR,
        DIVIDER
    }

    private Tween scaleUpTween;
    [SerializeField]
    float value;

    [SerializeField]
    public MODIFIER_TYPE currentModifier = MODIFIER_TYPE.ADDER;

    List<int> spawnedBalls;
    private void Start() {
        spawnedBalls = new List<int>();
        currentValLbl.text = currentModifier == MODIFIER_TYPE.ADDER ? "+" + value : "x" + value;
        DOTween.defaultAutoKill = false;
    }
    async private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball") && !spawnedBalls.Contains(collision.gameObject.GetInstanceID()) && value > 0)
        {

            if (collision.CompareTag("Ball") && value > 0)
        {

            if (scaleUpTween == null)
            {
                scaleUpTween = currentValLbl.transform.DOScale(currentValLbl.transform.localScale.x + 0.2f, 0.15f).SetEase(Ease.InOutCubic).OnComplete(() =>
                {
                    currentValLbl.transform.DOScale(currentValLbl.transform.localScale.x - 0.2f, 0.15f).SetEase(Ease.InOutCubic);
                });
                scaleUpTween.SetAutoKill(false);
                scaleUpTween.Play();
            }
            else if (scaleUpTween != null && !scaleUpTween.IsPlaying())
            {
                scaleUpTween.Restart();
            }
            if (currentModifier == MODIFIER_TYPE.ADDER)
            {
                value -= 1;
                if (value <= 0)
                {
                    Destroy(transform.parent.gameObject);
                }
                currentValLbl.text = "+" + value;
                await Task.Delay(TimeSpan.FromSeconds(0.1f));
                SpawnBall(collision);
            }
            else
            {

                for (int i = 0; i < value; i++)
                {
                    await Task.Delay(TimeSpan.FromSeconds(0.1f));
                    SpawnBall(collision);
                }
            }
            /* if (GetComponentInParent<ModifierMovement>() != null)
             {
                 GetComponentInParent<ModifierMovement>().Stop();
             }*/
        }
    }
    }
    async private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Ball")  && value > 0)
        {

            if (scaleUpTween == null) {
                scaleUpTween = currentValLbl.transform.DOScale(currentValLbl.transform.localScale.x + 0.2f, 0.15f).SetEase(Ease.InOutCubic).OnComplete(() => {
                    currentValLbl.transform.DOScale(currentValLbl.transform.localScale.x - 0.2f, 0.15f).SetEase(Ease.InOutCubic);
                });
                scaleUpTween.SetAutoKill(false);
                scaleUpTween.Play();
            } else if (scaleUpTween != null && !scaleUpTween.IsPlaying()) {
                scaleUpTween.Restart();
            }
            if (currentModifier == MODIFIER_TYPE.ADDER) {
                value -= 1;
                if (value <= 0) {
                    Destroy(transform.parent.gameObject);
                }
                currentValLbl.text = "+" + value;
                await Task.Delay(TimeSpan.FromSeconds(0.1f));
               // SpawnBall(GetComponent<BoxCollider>());
            } else {
               
                for (int i = 0; i < value; i++)
                {
                    await Task.Delay(TimeSpan.FromSeconds(0.1f));
               //     SpawnBall(other);
                }
            }
           /* if (GetComponentInParent<ModifierMovement>() != null)
            {
                GetComponentInParent<ModifierMovement>().Stop();
            }*/
        }
    }

    void SpawnBall(Collider2D colliderPos) {
        if (colliderPos != null) {
            //Ball ballObj = ObjectPool.Instance.GetPooledObject();
            Ball ballObj = null;


            if (ballObj == null) {
                ballObj = Instantiate<Ball>(ballPrefab, colliderPos.transform.position, Quaternion.identity);
            } else {
                ballObj.transform.position = colliderPos.transform.position;
            }
            //            ballObj.transform.SetParent(ballContainer.transform);
           // GameManager.Instance.AddRemainingBalls(1);
           // SoundHandler.Instance.PlaySound(SoundType.Pop);
            spawnedBalls.Add(ballObj.gameObject.GetInstanceID());
            spawnedBalls.Add(colliderPos.gameObject.GetInstanceID());
            ballObj.gameObject.SetActive(true);
            GameManager.Instance.ResetFailStart();
            SoundManager.Instance.PlaySound(SoundType.Pop);
            MMVibrationManager.Haptic(HapticTypes.MediumImpact);
            //  BucketController.Instance.ballsSpawned.Add(ballObj);
        }
    }
}
