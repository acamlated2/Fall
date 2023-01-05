using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    // private
    private GameObject _cameraAnchor;
    
    private float _dt;
    
    private float _prevCameraY;
    
    void Start()
    {
        _cameraAnchor = GameObject.FindGameObjectWithTag("Camera Anchor");
    }

    void Update()
    {
        _dt = Time.deltaTime;
        ManageCamera();
    }
    
    private void ManageCamera()
    {
        if (_cameraAnchor.transform.position.y < 0)
        {
            transform.position = new Vector3(transform.position.x,
                Mathf.Lerp(transform.position.y, _cameraAnchor.transform.position.y - 3, 3 * _dt));
        }
    }
}
