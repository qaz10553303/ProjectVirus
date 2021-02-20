using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRot : MonoBehaviour
{
    GameObject CamPoint;

    // Start is called before the first frame update

    void Start()
    {
        CamPoint = GameObject.Find("CameraRotPoint");
    }

    // Update is called once per frame
    void Update()
    {
        CameraRotation();
        RotRight();
        RotLeft();
    }

    //This is for Window Debug
    void CameraRotation()
    {
        if (Input.GetKeyUp(KeyCode.E))
        {
            CamPoint.transform.Rotate(0.0f, -90.0f, 0.0f);
        }
        if (Input.GetKeyUp(KeyCode.Q))
        {
            CamPoint.transform.Rotate(0.0f, 90.0f, 0.0f);
        }
    }

    public void RotRight()
    {
        CamPoint.transform.Rotate(0.0f, -90.0f, 0.0f);
    }

    public void RotLeft()
    {
        CamPoint.transform.Rotate(0.0f, 90.0f, 0.0f);
    }
}
