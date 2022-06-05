using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    public Color[] colors;
    [HideInInspector]
    public Color hitColor,failColor;

    public GameObject myWall;
    public GameObject finishLine;
    public GameObject colorBump;
    public GameObject walls;
    private GameObject[] _walls2;
   

    private float _wallSpawnNumber=11;
    private float _z=5;
    private float _randomRotation;

    private bool _isColor;

    void Awake()
    {
        Instance = this;
        GenerateColors();
        SpawnWalls();
       PlayerPrefs.GetInt("level",1);

    }
     
    private void Start()
    {
       // GenerateLevels();
    }

    void GenerateColors()
    {
        hitColor = colors[Random.Range(0, colors.Length)];
        failColor = colors[Random.Range(0, colors.Length)];
        while(hitColor == failColor)
            failColor = colors[Random.Range(0, colors.Length)];
        PlayerScript.SetColor(hitColor);   
    }

    public void GenerateLevels()
    {
        if (PlayerPrefs.GetInt("Level")<=1 && (PlayerPrefs.GetInt("Level")<=3))
        {
            _wallSpawnNumber = 12;
        }

        if (PlayerPrefs.GetInt("Level") <= 4 && (PlayerPrefs.GetInt("Level") <= 10))
        {
            _wallSpawnNumber = 14;
        }
        PlayerScript.Instance.enabled = true;
        PlayerScript.Instance.gameObject.GetComponent<SphereCollider>().enabled = true;

        DeleteWalls();
        _isColor = false;
       _z = 5;
        SpawnWalls();          
    }
    void DeleteWalls()
    {
        _walls2 = GameObject.FindGameObjectsWithTag("Fail");
        if (_walls2.Length>=1)
        {
            for (int i = 0; i < _walls2.Length; i++)
            {
                Destroy(_walls2[i].transform.parent.gameObject);
            }

            Destroy(GameObject.FindGameObjectWithTag("ColorBump"));
        }
    }
    void SpawnWalls()
    {
        for (int i = 0; i < _wallSpawnNumber; i++)
        {
            GameObject wall;

            if (Random.value<=0.5f && !_isColor && (PlayerPrefs.GetInt("Level") >= 3))
            {
                wall = Instantiate(colorBump, transform.position, Quaternion.identity);
                _isColor = true;
            }
          
            else 
            {
                wall = Instantiate(myWall, transform.position, Quaternion.identity);
            }
         
            wall.transform.SetParent(GameObject.FindWithTag("Helix").transform);
            wall.transform.localPosition = new Vector3(0,0,_z);
            _randomRotation = Random.Range(0, 360);
            wall.transform.localRotation = Quaternion.Euler(new Vector3(0,0,_randomRotation));
            _z += 5;
          
           
        }
        finishLine.transform.position = new Vector3(0, 0.03f, _z*2);

    }
}
