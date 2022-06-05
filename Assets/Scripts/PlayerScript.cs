using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public static PlayerScript Instance;

    private static Color _currentColor;

    private MeshRenderer _meshRenderer;

    private static float z;

    private bool _move, _colorChange;
    
    private float _height=.58f, _speed=3f;
    private float  _lerpValue;

    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        Instance = this;
        _meshRenderer = GetComponent<MeshRenderer>();

    }
    private void Start()
    {
        _move = false;
        SetColor(GameController.Instance.hitColor);
        _spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        _spriteRenderer.enabled = false;
    }
    void Update()
    {

        
        if (Touch.IsPressing())
        { _move = true; }
        if (_move)
        {PlayerScript.z+= _speed * .025f; }

            transform.position = new Vector3(0, _height, PlayerScript.z);

        UptadeColor();
        _spriteRenderer.color = _currentColor;
    }


    public static float CameraSpeed()
    {
        return PlayerScript.z;
    }

    void UptadeColor() 
    {
        _meshRenderer.sharedMaterial.color = _currentColor;
        if (_colorChange)
        {
            _currentColor = Color.Lerp(_meshRenderer.material.color, ColorBump.Instance.ColorBumpGetColor(), _lerpValue);
            _lerpValue = Time.deltaTime * 8;
        }

        if (_lerpValue >= 1)
        {
            _colorChange = false;
        }

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
        if (target.gameObject.tag == "FinishLine")
        {
            StartCoroutine(NewLevel());
            StartCoroutine(GameOver());
        }
        if (target.gameObject.tag == "Hit")
        {
            
           Destroy(target.transform.parent.gameObject);

        }
        if (target.gameObject.tag == "Fail")
        {
           
            StartCoroutine(GameOver());

        }


        if (target.gameObject.tag=="ColorBump")
        {
           _colorChange=true;
            _lerpValue = 0;
          
        }
    }




    IEnumerator GameOver()
    {
        _spriteRenderer.transform.position = new Vector3(0, .7f, PlayerScript.z - .05f);
        _spriteRenderer.transform.eulerAngles = new Vector3(0,0,Random.value*360);
        _spriteRenderer.enabled = true;

        _meshRenderer.enabled=false;
        gameObject.GetComponent<SphereCollider>().enabled=false;
        _move = false;

        yield return new WaitForSeconds(1.5f);
        PlayerScript.z = 0;
        GameController.Instance.GenerateLevels();
       



    }


    IEnumerator NewLevel()
    {
        Camera.main.GetComponent<CameraMovement>().enabled = false;
        yield return new WaitForSeconds(1.5f);
        PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
        _move=false;
        Camera.main.GetComponent<CameraMovement>().enabled = true;
        


    }

}
