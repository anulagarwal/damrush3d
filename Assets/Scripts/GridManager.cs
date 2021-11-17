using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance = null;

    [Header("Attributes")]
    [SerializeField] int height;
    [SerializeField] int width;
    [SerializeField] float offset;
    [SerializeField] int maxWalls;
    [SerializeField] int currentWalls;

    [SerializeField] public Point selectedPoint;
    [SerializeField] public Point endPoint;

    //List<List<GameObject>> grid;
    private Point[,] grid;
    public bool isMouseDown;

    public List<Point> connectedPoints;

    [Header("Components Reference")]
    [SerializeField] GameObject point;
    [SerializeField] LineRenderer lr;
    [SerializeField] GameObject wall;



    private void Awake()
    {
        Application.targetFrameRate = 100;
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        Instance = this;
    }

    // Start is called before the first frame update


    void Start()
    {
        grid = new Point[width, height];
        GenerateGrid();
        lr.positionCount = 2;
        currentWalls = maxWalls;
        UIManager.Instance.UpdateCurrentWallsCounts(currentWalls);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (selectedPoint != null)
            {
                //Draw line renderer
                
                isMouseDown = true;
                lr.SetPosition(connectedPoints.Count, selectedPoint.transform.position);

                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    lr.SetPosition(connectedPoints.Count+1, hit.point);
                }


            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (selectedPoint != null)
            {
                DeselectGridPoint();
                lr.positionCount=0;
                lr.positionCount=2;

                connectedPoints.Clear();
            }
            isMouseDown = false;
            //Cancel line renderer
        }
    }

    public void GenerateGrid()
    {
        for(int y =0; y< height; y++)
        {
            for(int x = 0; x< width; x++)
            {
                //Spawn grid points
                Point g = Instantiate(point, new Vector2(x + (offset * x), y+(offset * y)), Quaternion.identity).GetComponent<Point>();
                g.Init(x, y);
                grid[x,y] = g;
            }
        }
    }

    public void SelectGridPoint(int x, int y)
    {
        selectedPoint = grid[x, y];
        grid[x,y].SelectPoint();
    }

    public void DeselectGridPoint(Point p)
    {
        selectedPoint = null;
        grid[p.x,p.y].DeselectPoint();
       
    }
    public void DeselectEndGridPoint(Point p)
    {
        endPoint = null;
        grid[p.x, p.y].DeselectPoint();

    }
    public void DeselectGridPoint()
    {
        selectedPoint.DeselectPoint();
        selectedPoint = null;

    }

    public void ClearGrid()
    {
        foreach(Point p in grid)
        {
            Destroy(p.gameObject);
        }
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                //Spawn grid points
                grid[x, y] = null;
            }
        }

    }
    public void ConnectPoints(Point a, Point b)
    {
        if (currentWalls > 0)
        {
            lr.SetPosition(connectedPoints.Count, selectedPoint.transform.position);
            a.AddConnectedPoint(b);
            connectedPoints.Add(a);
            lr.SetPosition(connectedPoints.Count, endPoint.transform.position);
            b.AddConnectedPoint(a);
            connectedPoints.Add(b);

            lr.positionCount = connectedPoints.Count + 2;
            //Simple y=mx+b, where b=0 and m= (yb - ya)/(xb - xa)      
            float zRot = Vector3.Angle(Vector3.up, b.transform.position - a.transform.position);
            if (b.transform.position.x - a.transform.position.x > 0.0f)
                zRot *= -1.0f;

            Vector3 midPoint = (a.transform.position + b.transform.position) * 0.5f;
            GameObject g = Instantiate(wall, midPoint, Quaternion.identity);

            g.transform.eulerAngles = new Vector3(0, 0, zRot + 90);

            a.BuildWall();
            b.BuildWall();
            currentWalls--;
            UIManager.Instance.UpdateCurrentWallsCounts(currentWalls);

            DeselectGridPoint();
            DeselectEndGridPoint(endPoint);
        }
    }
}
