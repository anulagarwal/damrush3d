using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;
public class Jumper : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] float jumpForce;
    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "DrawnWall")
        {
          collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce));
            GetComponent<MMFeedbacks>().PlayFeedbacks();
            //Add Pad VFX
        }
    }
}
