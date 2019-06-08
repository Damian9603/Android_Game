using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRes : MonoBehaviour
{
    public int xx = 1280, yy = 720;
    public Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        Screen.SetResolution(xx, yy, true);
        cam.aspect = 16f / 9f;
    }
}
