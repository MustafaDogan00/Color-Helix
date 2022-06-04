using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public static PlayerScript Instance;

    private static Color _currentColor;

    private MeshRenderer _meshRenderer;

    private static float z;

    private bool _move;

    private Rigidbody _rb;
    
    private float _height=.58f, _speed=3f;

    private Vector3 _position;

    private void Awake()
    {
        Instance = this;
        _meshRenderer = GetComponent<MeshRenderer>();
        _position = transform.position;
       
    }

    void Update()
    {

        
        if (Touch.IsPressing())
        { _move = true; }
        if (_move)
        { PlayerScript.z += _speed * .025f; }

            transform.position = new Vector3(0, _height, PlayerScript.z);

        UptadeColor();
    }


    public static float CameraSpeed()
    {
        return PlayerScript.z;
    }

    void UptadeColor() 
    {
      _meshRenderer.sharedMaterial.color =_currentColor;
      
    }
    public static Color SetColor(Color color)
    {
        return _currentColor=color;

    }

    public static Color GetColor()
    {

        return _currentColor;
    }



    private void OnCollisionEnter(Collision target)
    {
        if (target.gameObject.tag=="FinishLine")
        {
            Debug.Log("FinishLine");
        }
        if (target.gameObject.tag == "Hit")
        {
            Debug.Log("Hit");

        }
        if (target.gameObject.tag == "Fail")
        {
            Debug.Log("Fail");
            StartCoroutine(GameOver());

        }
    }

    IEnumerator GameOver()
    {
        GameController.Instance.GenerateLevels();
        _move=false;
        PlayerScript.z = 0;
        transform.position = _position;
        yield break;

    }

}
