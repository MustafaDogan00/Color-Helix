using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CylinderScript : MonoBehaviour
{
    private GameObject _helix;
    void Awake()
    {
        _helix = GameObject.FindWithTag("Helix");
    }

   
    void Update()
    {
        transform.eulerAngles = new Vector3(0, 0, _helix.gameObject.transform.eulerAngles.z % 25 );
    }
}
