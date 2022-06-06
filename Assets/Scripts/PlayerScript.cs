using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public static PlayerScript Instance;

    private static Color _currentColor;

    private MeshRenderer _meshRenderer;

    public static float z=0;

    [HideInInspector]
    public bool display;
    private bool _move;
    private bool  _colorChange;
    public bool perfectStar;
    private float _height=.58f, _speed=3f;
    private float  _lerpValue;

    private SpriteRenderer _spriteRenderer;

    public GameObject pointDisplay;

    //private AudioSource _hitSound, _failSound, _nextLevelSound;



    private void Awake()
    {
        Instance = this;
        _meshRenderer = GetComponent<MeshRenderer>();
       /* _hitSound = GameObject.Find("HitSound").GetComponent<AudioSource>();
        _failSound = GameObject.Find("FailSound").GetComponent<AudioSource>();
        _nextLevelSound = GameObject.Find("NextLevelSound").GetComponent<AudioSource>();*/


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
        { 
            _move = true;
            GetComponent<SphereCollider>().enabled = true;      
        }

        if (_move)
        {PlayerScript.z+= _speed * .025f; }
            transform.position = new Vector3(0, _height, PlayerScript.z);

        UptadeColor();
        _spriteRenderer.color = _currentColor;
      display = false;
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
            //_nextLevelSound.Play();
        }
        if (target.gameObject.tag == "Hit")
        {
            if (perfectStar && !display)
            {
                display = true;
                GameObject pointObject = Instantiate(pointDisplay, transform.position, Quaternion.identity);
                pointObject.GetComponent<PointDisplay>().PointDisp("Perfect +" + PlayerPrefs.GetInt("Level") * 2);
            }
            else if (!perfectStar && !display)
            {
                display = true;
                GameObject pointObject = Instantiate(pointDisplay, transform.position, Quaternion.identity);
                pointObject.GetComponent<PointDisplay>().PointDisp("+" + PlayerPrefs.GetInt("Level"));
            }
           // _hitSound.Play();
            Destroy(target.transform.parent.gameObject);

        }
        if (target.gameObject.tag == "Fail")
        {
           
                gameObject.GetComponent<SphereCollider>().enabled = false;
                StartCoroutine(GameOver());
            //_failSound.Play();
                print("CARPTI");
        }


        if (target.gameObject.tag=="ColorBump")
        {
              
           _colorChange=true;
            _lerpValue = 0;
        }

        if (target.gameObject.CompareTag("Star"))
        {
            perfectStar = true;
        }
    }




    IEnumerator GameOver()
    {
       // _gameOver=true;
        _spriteRenderer.transform.position = new Vector3(0, .7f, PlayerScript.z - .05f);
        _spriteRenderer.transform.eulerAngles = new Vector3(0,0,Random.value*360);
        _spriteRenderer.enabled = true;
         
        _meshRenderer.enabled=false;
        GetComponent<SphereCollider>().enabled=false;
        _move = false;
        yield return new WaitForSeconds(1.5f);
        Camera.main.GetComponent<CameraMovement>().FlashScreen();
        // _gameOver = false;
        PlayerScript.z = 0;
        GameController.Instance.GenerateLevels();
        _spriteRenderer.enabled = false;
        _meshRenderer.enabled = false;



    }


    IEnumerator NewLevel()
    {
        Camera.main.GetComponent<CameraMovement>().enabled = false;
        yield return new WaitForSeconds(1.5f);
        PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
        _move=false;
        Camera.main.GetComponent<CameraMovement>().enabled = true;
        Camera.main.GetComponent<CameraMovement>().FlashScreen();



    }

}
