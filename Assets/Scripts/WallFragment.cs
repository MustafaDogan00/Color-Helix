﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallFragment : MonoBehaviour
{
    private MeshRenderer _meshRenderer;


    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }
    void Start()
    {
      
        if (this.gameObject.tag=="Hit")
        {
            if (PlayerPrefs.GetInt("Level") >= 3) 
            {
                GameObject colorBump = GameObject.FindGameObjectWithTag("ColorBump");
                if (transform.position.z > colorBump.transform.position.z)
                {
                    GameController.Instance.hitColor = ColorBump.Instance.ColorBumpGetColor();
                }
            }
           
            _meshRenderer.material.color = GameController.Instance.hitColor;
        }else
        {
            if (GameController.Instance.failColor==GameController.Instance.hitColor)
            {
                GameController.Instance.failColor = GameController.Instance.colors[Random.Range(0, GameController.Instance.colors.Length)];
            }

            _meshRenderer.material.color = GameController.Instance.failColor;

        }
        
    }

   
}
