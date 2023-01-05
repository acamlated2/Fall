using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAnchorScript : MonoBehaviour
{
    // private
    private GameObject _gura;
    
    void Start()
    {
        _gura = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(0, _gura.transform.position.y);
    }
}
