using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] public bool isSelected;
    [SerializeField] public int x;
    [SerializeField] public int y;
    [SerializeField] List<Point> connectedPoint;
    [SerializeField] bool isBuiltOn;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Init(int xVal, int yVal)
    {
        x = xVal;
        y = yVal;
    }
    public void SelectPoint()
    {
        isSelected = true;
        GetComponent<MeshRenderer>().material.color = Color.green;
        //Show green VFX
    }

    public void DeselectPoint()
    {
        isSelected = false;
        if(!isBuiltOn)
        GetComponent<MeshRenderer>().material.color = Color.white;
        else
            GetComponent<MeshRenderer>().material.color = Color.blue;

        //Show white VFX
    }

    public void AddConnectedPoint(Point p)
    {
        connectedPoint.Add(p);
    }

    public void ClearConnectedPoints()
    {
        connectedPoint.Clear();
    }

    public void BuildWall()
    {
        GetComponent<MeshRenderer>().material.color = Color.blue;
        isBuiltOn = true;
    }

    public void CancelWall()
    {
        GetComponent<MeshRenderer>().material.color = Color.white;
        isBuiltOn = false;
    }
    private void OnMouseDown()
    {
        if (GridManager.Instance.selectedPoint == null)
        {
            GridManager.Instance.SelectGridPoint(x, y);
        }
    }

    private void OnMouseEnter()
    {
        if (GridManager.Instance.isMouseDown)
        {
            if (GridManager.Instance.selectedPoint != null && GridManager.Instance.selectedPoint != this && connectedPoint.Find(x => x == GridManager.Instance.selectedPoint) == null)
            {
                GridManager.Instance.endPoint = this;
                SelectPoint();
                GridManager.Instance.ConnectPoints(GridManager.Instance.selectedPoint, this);
                //Make line from selected point to end point
            }
            if (GridManager.Instance.selectedPoint == null)
            {
                GridManager.Instance.SelectGridPoint(x, y);
            }
        }
    }

    private void OnMouseExit()
    {
        if(GridManager.Instance.endPoint == this)
        {            
            GridManager.Instance.endPoint = null;
            DeselectPoint();
        }
    }


}
