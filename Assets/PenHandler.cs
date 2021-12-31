using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenHandler : MonoBehaviour
{
    [Range(-5.0f, 5.0f)]
    public float offsetX;
    [Range(-5.0f, 5.0f)]
    public float offsetY;

     private Vector3 offset;

    private Camera myMainCamera; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        offsetX = transform.position.x - Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
        offsetY = transform.position.y - Camera.main.ScreenToWorldPoint(Input.mousePosition).y;

    }

    private void OnMouseDrag()
    {
      transform.position =  new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x + offsetX,
                                                                          Camera.main.ScreenToWorldPoint(Input.mousePosition).y + offsetY);
        GetComponentInChildren<TargetJoint2D>().target = GetComponentInChildren<TargetJoint2D>().transform.position;
    }
}
