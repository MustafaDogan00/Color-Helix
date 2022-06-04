using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorBump : MonoBehaviour
{
    private MeshRenderer _meshRenderer;
   
    private Color _color;


    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        
    }
    void Start()
    {
        transform.parent = null;
       transform.rotation = Quaternion.Euler(new Vector3(0,0,0));
        _color = GameController.Instance.colors[Random.Range(0, GameController.Instance.colors.Length)];
        _meshRenderer.material.color = _color; 

    }

   
    public Color ColorBumpGetColor()
    {
        return _color;
    }
}
