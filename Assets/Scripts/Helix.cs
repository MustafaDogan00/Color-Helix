using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helix : MonoBehaviour
{
    private bool _movable = true;

    private float _angle;
    private float _lastDeltaAngle, _lastTouchX;
   

   
    void Update()
    {
        if (_movable && Touch.IsPressing())
        {
            float mouseX=this.GetMouseX();
            _lastDeltaAngle=_lastTouchX - mouseX;
            _angle += _lastDeltaAngle * 360 * 1.7f;
            _lastTouchX = mouseX;
        } 
        else if (_lastDeltaAngle!=0)
        {
            _lastDeltaAngle -=_lastDeltaAngle*5*Time.deltaTime;
            _angle -= _angle += _lastDeltaAngle * 360 * 1.7f;

        }

        transform.eulerAngles=new Vector3(0,0,_angle);
    }



    private float GetMouseX()
    {
        return Input.mousePosition.x/(float)Screen.width;

    }
}
