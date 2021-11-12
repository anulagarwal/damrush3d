// PlayerMovement.cs is part of 2D Physics Draw asset for Uunity 3D
// Made by Daniel C Menezes
// Asset Store URL: http://u3d.as/otD

using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    public float Speed = 0f;
    private float movex = 0f;
    //private float movey = 0f;

    // Use this for initialization
    void Start()
    {
        Speed = 5f;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.A))
            movex = -1;
        else if (Input.GetKey(KeyCode.D))
            movex = 1;
        else
            movex = 0;
    }

    void FixedUpdate()
    {
        if(this.GetComponent<Rigidbody2D>().bodyType != RigidbodyType2D.Static)
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(movex * Speed, this.GetComponent<Rigidbody2D>().velocity.y);
    }
}
