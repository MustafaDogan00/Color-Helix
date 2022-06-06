using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private float _cSpeed;

    private Animator _animator;
    private float _time,_speed=2f;


    private void Awake()
    {
        _animator = transform.GetChild(0).GetComponent<Animator>();
    }
    void Update()
    {
        if (_time < 1)
        {
            _time += _speed*Time.deltaTime;
            _cSpeed = Mathf.Lerp(transform.position.z, -2.95f, _time);
                
        }
        else
        {
            _cSpeed = PlayerScript.CameraSpeed() - 2.95f;
        }
        transform.position = new Vector3(0, 2.2f, _cSpeed);
           
    }
    public void FlashScreen()
    {
        _animator.SetTrigger("Flash");
        _cSpeed = 0;
        _time = 0;

    }
}
