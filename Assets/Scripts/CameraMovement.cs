using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private float _cSpeed;
 
   
   
    void Update()
    {

        _cSpeed = PlayerScript.CameraSpeed()-2.95f;
        transform.position = new Vector3(0, 2.2f, _cSpeed);
    }
}
