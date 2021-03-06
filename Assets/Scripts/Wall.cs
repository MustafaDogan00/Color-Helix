using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public GameObject wallFragment;
    private GameObject _wall1,_wall2;
    public GameObject perfectStar;

    private float _rotatitonZ;
    private float _rotatitonZMax=180;

    private bool _smallWall;


  
    void Start()
    {
        SpawnWall();
    }


    void SpawnWall()
    {
        _wall1 = new GameObject();
        _wall2 = new GameObject();

        _wall1.name = "Wall1";
        _wall2.name = "Wall2";

        _wall1.tag = "Wall1";
        _wall2.tag = "Fail";

   
        _wall1.transform.SetParent(transform);
        _wall2.transform.SetParent(transform);


        _wall2.AddComponent<BoxCollider>();    
        _wall2.GetComponent<BoxCollider>().size = new Vector3(0.9f, 1.85f, 0.2f);
       _wall2.GetComponent<BoxCollider>().center = new Vector3(0.46f, 0, 0);

        if (Random.value <=.4f && (PlayerPrefs.GetInt("Level") >= 3))
        {
            _smallWall = true;
        }

        if (_smallWall)
        {
            _rotatitonZMax = 90;
        }
        else
        {
            _rotatitonZMax = 180;
        }
       

        for (int i = 0; i < 100; i++)
        {
            GameObject wallF = Instantiate(wallFragment, Vector3.zero, Quaternion.Euler(new Vector3(0, 0, _rotatitonZ)));
            _rotatitonZ += 3.6f;

            if (_rotatitonZ<=_rotatitonZMax)
            {
                wallF.transform.SetParent(_wall1.transform);
                wallF.gameObject.tag = "Hit";
            }
            else
            {
                wallF.transform.SetParent(_wall2.transform);
               // wallF.gameObject.tag = "Fail";
            }
        }

        _wall1.transform.localPosition = Vector3.zero;
        _wall2.transform.localPosition = Vector3.zero;

        _wall1.transform.localRotation = Quaternion.Euler(new Vector3(0,0,0));
        _wall2.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));

        if (_smallWall)
        {
            GameObject wallFragmentsChield = _wall1.transform.GetChild(14).gameObject;
            AddStar(wallFragmentsChield);
        }
        else
        {
            GameObject wallFragmentsChield = _wall1.transform.GetChild(25).gameObject;
            AddStar(wallFragmentsChield);
        }
       
    }

    void AddStar(GameObject wallFragmentsChield)
    {
        GameObject star= Instantiate(perfectStar, transform.position, Quaternion.identity);
        star.transform.SetParent(wallFragmentsChield.transform);
       star.transform.localPosition = new Vector3(.05f, .65f, -.06f);

    }
   

}
