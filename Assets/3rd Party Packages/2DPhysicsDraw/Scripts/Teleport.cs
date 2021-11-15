using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour {

    public GameObject endPos;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnMouseUp()
    {
    }

    public GameObject carrier;

    void OnTriggerEnter2D(Collider2D other)
    {
        carrier.transform.position = this.transform.position;
        other.transform.parent = carrier.transform;

        carrier.gameObject.transform.position = endPos.transform.position;
        other.transform.parent = null;
    }

}
