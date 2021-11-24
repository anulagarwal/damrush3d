using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableWall : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] int health;
    // Start is called before the first frame update

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "DrawnWall")
        {
        }
    }
}
