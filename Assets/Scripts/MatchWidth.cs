using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class MatchWidth : MonoBehaviour
{

    // Set this to the in-world distance between the left & right edges of your scene.
    public float sceneHeight = 10;
    public float mult = 3.09677419355f;
    Camera _camera;
    void Start()
    {
        _camera = GetComponent<Camera>();
        //3.2 width size inc = 1 height inc
    }

    // Adjust the camera's height so the desired scene width fits in view
    // even if the screen/window size changes dynamically.
    void Update()
    {
        float unitsPerPixel = sceneHeight / Screen.width;

        float desiredHalfHeight = 0.5f * unitsPerPixel * Screen.height;

        _camera.orthographicSize = desiredHalfHeight;
        _camera.transform.position = new Vector3(_camera.transform.position.x, desiredHalfHeight+ 0.6f, _camera.transform.position.z);
    }
}