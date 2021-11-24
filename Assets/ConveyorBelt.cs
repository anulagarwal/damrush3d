using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] public float speed;
    [SerializeField] public float rotSpeed;

    [Header("Component References")]
    [SerializeField] GameObject conveyorBelt;
    [SerializeField] List<Rotator> wheels;

    // Start is called before the first frame update
    void Start()
    {
        foreach(Rotator r in wheels)
        {
            r.SetSpeed(rotSpeed);
        }       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
