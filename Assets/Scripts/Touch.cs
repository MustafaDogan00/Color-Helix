using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Touch : MonoBehaviour,IPointerDownHandler, IPointerUpHandler
{
    private static bool _pressing;
    public static bool IsPressing()
    {
       
        return _pressing;
    }

   
  public void OnPointerDown(PointerEventData data)
   {
        _pressing = true;

   }
    public void OnPointerUp(PointerEventData data)
    {

        _pressing = false;
    }



}
