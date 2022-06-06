using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointDisplay : MonoBehaviour
{
    private TextMesh _textMesh;



    private void Awake()
    {
        _textMesh = GetComponent<TextMesh>();
    }
    public void PointDisp(string text)
    {
        this._textMesh.text = text;

    }

  
    void LateUpdate()
    {
        transform.position = new Vector3(transform.position.x,transform.position.y,PlayerScript.CameraSpeed());
        Destroy(gameObject,1.2f);
    }
}
