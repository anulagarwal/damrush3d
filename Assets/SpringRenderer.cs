using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringRenderer : MonoBehaviour
{
    [SerializeField] LineRenderer lr;
    [SerializeField] Transform pointA;
    [SerializeField] Transform pointB;

    // Start is called before the first frame update
    void Start()
    {
        lr.positionCount = 2;  
    }

    // Update is called once per frame
    void Update()
    {
        lr.SetPosition(0, pointA.position);
        lr.SetPosition(1, pointB.position);

    }
}
