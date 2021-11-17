using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CastleHandler : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] public int health;

    [Header("Component References")]
    [SerializeField] public TextMeshPro healthText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "DrawnWall")
        {
            Destroy(collision.gameObject);
        }

        if(collision.gameObject.tag == "Ball")
        {
            Destroy(collision.gameObject);
            UpdateHealth(health--);
            if (health <= 0)
            {
                //Victory condition
                GameManager.Instance.WinLevel();

                Destroy(gameObject);
            }

        }
    }

    void UpdateHealth(int val)
    {
        healthText.text = "" + val;
    }
}
